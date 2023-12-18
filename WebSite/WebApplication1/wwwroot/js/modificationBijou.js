import { Bijou } from "./bijou.js";
import { getPanierToken } from "./cookies.js";
import { sendBijouModified, IsAdmin, delBijou } from "./adminDAO.js";
import { redirectToConnexion, redirectToGestion } from "./adminRedirection.js";
// Fonction de requête pour récupérer les détails du bijou
async function fetchBijouDetails(bijouId) {
    const apiUrl = `https://elaparaibatest.fr/Bijoux/GetBijouWithId?id=${bijouId}`;
    try {
        const response = await fetch(apiUrl);
        const data = await response.json();

        const bijou = new Bijou(
            data.id,
            data.name,
            data.description,
            data.price,
            data.quantity,
            data.type,
            data.dossierPhoto,
            data.nbPhotos,
            data.datepublication
        );

        return bijou;

    } catch (error) {
        console.error("Erreur de requête:", error);
    }
}

//Hydrate les attributs du bijou dans le formulaire
function displayBijouDetails(bijou) {
    document.getElementById('name').value = bijou.nomBijou;
    document.getElementById('description').value = bijou.descriptionBijou;
    document.getElementById('quantite').value = bijou.stockBijou;
    document.getElementById('type').value = bijou.type;
    document.getElementById('matiere').value = "Paladium";
    document.getElementById('prix').value = bijou.prixBijou;
    //Création de la date
    var partsDate = bijou.datepublication.split('/');
    // Récupérer les parties de la date
    var day = partsDate[0];
    var month = partsDate[1];
    // var year = partsDate[2];
    //Séparation de l'heure et de la date
    let yearHourArray = partsDate[2].split(' ');
    var year = yearHourArray[0];
    // Construire la date au format ISO (aaaa-mm-jj) 
    var isoDate = year + "-" + month + "-" + day;
    document.getElementById('datePublication').value = isoDate;

    //Gérer les images
}

document.addEventListener("DOMContentLoaded", async function () {
    if(await IsAdmin(getPanierToken("PanierToken"))){ //L'utilisateur est admin
        // Récupérer l'ID du bijou depuis l'URL
        const queryString = window.location.search;
        const urlParams = new URLSearchParams(queryString);
        const bijouId = urlParams.get('bijouId');
        
        //On modifie un bijou existant
        if (bijouId != -1) {
            await pageModificationBijou(bijouId);
        }
        
        var form = document.getElementById("bijouForm");
        //Evénement pour la modification du bijou
        form.addEventListener("submit", async function (event) {
            event.preventDefault(); // Empêche l'envoi du formulaire par défaut
            // Récupération des données du form
            const formData = new FormData(form);
            formData.append('IdBijou', bijouId);
            let userToken = getPanierToken("PanierToken");
            formData.append('UserToken', userToken);
            let popupMessage;
            if(await sendBijouModified(formData)){
                popupMessage = "Requête accomplie avec succès.";
                showPopup(popupMessage);
            }
            else{
                popupMessage = "Echec de la requête.";
                showPopup(popupMessage);
            }
        });
    }
    else{ //L'utilisateur n'est pas admin
        redirectToConnexion();
    }
});

//Affiche les paramètres pour modifier un bijou
async function pageModificationBijou(bijouId){
    var bijou = await fetchBijouDetails(bijouId);
    displayBijouDetails(bijou);
    displayDeleteBouton(bijouId, getPanierToken("PanierToken"));
    const monTitreElement = document.getElementById('mainTitle');
    monTitreElement.textContent = "Modification bijou";
    //On ne peut pas modfier le type d'un bijou.
    var selectElement = document.getElementById("type");
    selectElement.hidden = true;
}

//Affiche le pop up
function showPopup(message) {
    // Vérifier si la div existe
    var divASupprimer = document.getElementById('popup');

    if (divASupprimer) {
    // La div existe, la supprimer
    divASupprimer.remove();
    } else {
    console.log("La div n'existe pas.");
    }

    const popupDiv = document.createElement('div');
    popupDiv.id = 'popup';
  
    // Création de l'élément p
    const popupMessage = document.createElement('p');
    popupMessage.id = 'popupMessage';
    popupMessage.textContent = message;
  
    // Ajout de l'élément p à la classe
    popupMessage.classList.add('popup-message');
  
    // Création de l'élément bouton
    const popupButton = document.createElement('button');
    popupButton.id = 'popupButton';
    popupButton.textContent = 'OK';
  
    // Ajout de l'élément bouton à la classe
    popupButton.classList.add('popup-button');
    
    // Création de l'overlay
    const overlay = document.createElement('div');
    overlay.id = 'overlay';
  
    // Ajout de l'overlay à la classe
    overlay.classList.add('overlay');
  
    // Ajout des éléments créés au corps du document
    document.body.appendChild(popupDiv);
    document.body.appendChild(overlay);
    popupDiv.appendChild(popupMessage);
    popupDiv.appendChild(popupButton);
    document.getElementById('popup').style.display = 'flex';
    document.getElementById('overlay').style.display = 'block';
    document.getElementById('popupButton').addEventListener('click', function () {
      hidePopup();
      if(message == "Bijou supprimé." || message == "Requête accomplie avec succès."){ //Requête accomplie avec succès
        redirectToGestion(); //Retour à la page de gestion
      }
    });
}

//Cache le pop up 
function hidePopup() {
    // Masquer le pop-up et l'overlay
    document.getElementById('popup').style.display = 'none';
    document.getElementById('overlay').style.display = 'none';
}

//Affiche le bouton pour supprimer le bijou
function displayDeleteBouton(BijouId, PanierToken) {
    //Création de la div
    const delDiv = document.createElement('div');
    delDiv.classList.add('bouton-conteneur')
    //Id
    delDiv.id = 'bouton-conteneur';
    //Création du button
    const delButton = document.createElement('button');
    delButton.textContent = 'Supprimer bijou';
    delButton.id = 'boutonDelete';
    delButton.type = 'button';
    //Ajout du bouton dans la div
    delDiv.appendChild(delButton);
    document.body.appendChild(delDiv);
    //Ajout de l'événement
    document.getElementById('boutonDelete').addEventListener('click', async function () {
        let message;
        if(await delBijou(PanierToken, BijouId)){ //Bijou supprimé
            message = "Bijou supprimé.";       
        }
        else{
            message = "Erreur lors de la suppression."//Erreur lors de la supression
        }
        showPopup(message);
    });
}
