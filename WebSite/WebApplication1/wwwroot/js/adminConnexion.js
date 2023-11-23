import { IsAdmin, ConnectAsAdmin } from "./adminDAO.js";
import { getPanierToken, setPanierToken } from "./cookies.js";

function redirectToBijouModification(bijouId){
  window.location.href = "gestion.html";
}

document.addEventListener("DOMContentLoaded", async function () {
  //Créer un paniertoken si l'utilisateur en a pas
  var panierTokenValue = getPanierToken("PanierToken");
  if (panierTokenValue === "") { //Le token n'est pas définie
      panierTokenValue = await setPanierToken();
  }
  if(await IsAdmin(panierTokenValue)){ //Le token à les droits d'admin
    redirectToBijouModification(); 
  }
  else{
    const form = document.querySelector('form');
    //Ajout d'un événement pour l'envoi du form
    form.addEventListener('submit', async function (event){
      event.preventDefault();
      //Récupération des données du form
      var formData = new FormData(form);
      formData.append("TokenPanier", panierTokenValue);
      //Tentative de connexion
      if(await ConnectAsAdmin(formData)){
        redirectToBijouModification();
      }
      else{
        showPopup();
      }
    })  
  }
  // Création de la div pop-up
});

//Affiche des erreurs de connexion
function showPopup() {
  const popupDiv = document.createElement('div');
  popupDiv.id = 'popup';

  // Création de l'élément p
  const popupMessage = document.createElement('p');
  popupMessage.id = 'popupMessage';
  popupMessage.textContent = "Échec de la connexion. Vérifiez vos informations d'identification.";

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

  document.getElementById('popupButton').addEventListener('click', async function () {
    hidePopup();
  });
}

function hidePopup() {
  // Masquer le pop-up et l'overlay
  document.getElementById('popup').style.display = 'none';
  document.getElementById('overlay').style.display = 'none';
}
