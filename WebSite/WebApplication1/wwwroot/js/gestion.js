import { Bijou } from "./bijoupresentation.js";
import { IsAdmin } from "./adminDAO.js";
import { redirectToConnexion } from "./adminRedirection.js";
import { getPanierToken } from "./cookies.js";

//Liste des bijoux 
var bijoux = [];

//Fonction communicante avec l'API bijou
async function fetchAllBijou() {
    const apiUrl = `https://localhost:7252/Bijoux/GetAllBijoux`;
    try {
        //Requête vers l'Api
        const response = await fetch(apiUrl);
        //Traduction de la requête en json
        const data = await response.json();
        for (let i = 0; i < data.length; i++) {
            const element = data[i];
            const nouveauBijou = new Bijou(
                element.id,
                element.name,
                element.description,
                element.price,
                element.quantity,
                element.type,
                element.dossierPhoto,
                element.nbPhotos,
                element.datepublication
            );
            bijoux.push(nouveauBijou);
        }
    } catch (error) {
        console.error("Erreur de requête:", error);
    }
}

//Affiche les bijoux à modifier
function displayBijou() {
    //Conteneur des bijoux dans l'HTML
    const conteneurBijoux = document.getElementById("listBijoux");
    conteneurBijoux.innerHTML = "";
    bijoux.forEach(bijou => {

        //Création d'un conteneur pour le bijou
        const bijouElement = document.createElement("div");
        bijouElement.classList.add("bijou");

        //Image bijou
        const imagePath = `https://images.elaparaibatest.fr/images/PhotosDescriptif${bijou.type}/${bijou.dossierPhoto}/1.jpg`;
        const imgBijouDiv = document.createElement("div");
        imgBijouDiv.classList.add("photoBijou");
        const imageBijou = document.createElement("img");
        imageBijou.src = imagePath;
        imageBijou.alt = bijou.nomBijou;
        imgBijouDiv.appendChild(imageBijou);

        //Nom du bijou
        const titreBijouDiv = document.createElement("div");
        titreBijouDiv.classList.add("nomBijou")
        const titreBijou = document.createElement("p");
        titreBijou.textContent = `${bijou.nomBijou}`;
        titreBijouDiv.appendChild(titreBijou);

        //Prix bijou
        const prixBijouDiv = document.createElement("div");
        prixBijouDiv.classList.add("prixBijou");
        const prixBijou = document.createElement("p");
        prixBijou.textContent = `Prix : ${bijou.prixBijou}€`;
        prixBijouDiv.appendChild(prixBijou);

        //Quantite bijou
        const quantiteBijouDiv = document.createElement("div");
        quantiteBijouDiv.classList.add("quantiteBijou");
        const quantiteBijou = document.createElement("p");
        quantiteBijou.textContent = `Stock : ${bijou.stockBijou}`;
        quantiteBijouDiv.appendChild(quantiteBijou);

        //Boutton
        const boutonDiv = document.createElement("div");
        boutonDiv.classList.add("boutonModifier"); // Utilisez "boutton" au lieu de "boutonModifier"
        const bouton = document.createElement("button");
        bouton.textContent = "Modifier";
        bouton.classList.add("boutton");
        boutonDiv.appendChild(bouton);
        

        //Création d'un événement pour rédiriger vers la page de modification du bijou
        bouton.addEventListener("click", function(){
            redirectToBijouModification(bijou.idBijou);
        })


        //Ajout des sous div dans la div du bijou
        bijouElement.appendChild(imgBijouDiv);
        bijouElement.appendChild(titreBijouDiv);
        bijouElement.appendChild(prixBijouDiv);
        bijouElement.appendChild(quantiteBijouDiv);
        bijouElement.appendChild(boutonDiv);

        conteneurBijoux.appendChild(bijouElement);

    })
    document.getElementById("nouveauBijou");
}

document.addEventListener("DOMContentLoaded", async function () {
    if(await IsAdmin(getPanierToken("PanierToken"))){
    const nouveauBijouButton = document.getElementById('nouveauBijouButton');
    nouveauBijouButton.addEventListener('click', function() {
        redirectToBijouModification("-1");
    });
    await fetchAllBijou();
    displayBijou();
    }
    else{
        redirectToConnexion();
    }
});

function redirectToBijouModification(bijouId){
    window.location.href = "modificationBijou.html?bijouId=" + bijouId;
}