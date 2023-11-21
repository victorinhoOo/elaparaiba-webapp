import { Bijou } from "./bijoupresentation.js";

// Fonction de requête pour récupérer les détails du bijou
async function fetchBijouDetails(bijouId) {
    const apiUrl = `https://localhost:7252/Bijoux/GetBijouWithId?id=${bijouId}`;
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

//Envoi le bijou modifé au serveur
async function sendBijouModified(form, bijouId) {
    // Récupération des données du form
    const formData = new FormData(form);
    formData.append('IdBijou', bijouId);
    //Communication avec l'api
    var success = False;
    try {
        const response = await fetch('https://localhost:7252/Administration/ModifierBijou', {
            method: 'POST',
            body: formData,
        });
        if (response.ok) {//Modification réussi
            console.log('Réponse réussie :', response);
            success = False;
        } else {//Erreur coté serveur
            console.error('Réponse en échec :', response.status, response.statusText);
            console.error(await response.text()); // Affichez le corps de la réponse dans la console
        }
        showPopup(success);
    } catch (error) {
        console.error('Erreur : Communication impossible avec le serveur', error);
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
    var year = partsDate[2];
    // Construire la date au format ISO (aaaa-mm-jj) 
    var isoDate = year + "-" + month + "-" + day;
    document.getElementById('datePublication').value = isoDate;

    //Gérer les images
}

document.addEventListener("DOMContentLoaded", async function () {
    // Récupérer l'ID du bijou depuis l'URL
    const queryString = window.location.search;
    const urlParams = new URLSearchParams(queryString);
    const bijouId = urlParams.get('bijouId');
    
    //On modifie un bijou existant
    if (bijouId != -1) {
        var bijou = await fetchBijouDetails(bijouId);
        displayBijouDetails(bijou);
    }
    var form = document.getElementById("bijouForm")
    form.addEventListener("submit", async function (event) {
        event.preventDefault(); // Empêche l'envoi du formulaire par défaut
        if(bijouId != -1){ //On modifie un bijoue existant
            sendBijouModified(form, bijouId);
        }
    });

});

// Fonction pour afficher le pop-up
function showPopup() {
    var popup = document.getElementById("popup");
    
    popup.style.display = "block";
}

// Fonction pour fermer le pop-up
function closePopup() {
    var popup = document.getElementById("popup");
    popup.style.display = "none";
}
