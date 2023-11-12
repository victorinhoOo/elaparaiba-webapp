import { Bijou } from "./bijoupresentation.js";

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
        const imagePath = `../../images/Photosdescriptif${bijou.type}/${bijou.dossierPhoto}/1.jpg`;
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


        //Ajout des sous div dans la div du bijou
        bijouElement.appendChild(imgBijouDiv);
        bijouElement.appendChild(titreBijouDiv);
        bijouElement.appendChild(prixBijouDiv);
        bijouElement.appendChild(quantiteBijouDiv);
        bijouElement.appendChild(boutonDiv);

        conteneurBijoux.appendChild(bijouElement);

    })
}

document.addEventListener("DOMContentLoaded", async function () {
    await fetchAllBijou();
    displayBijou();


});