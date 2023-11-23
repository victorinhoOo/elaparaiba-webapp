import { bijoux, fetchBijou, initialiserBijoux } from "../js/bijouDAO.js";

class Bijou {
    constructor(idBijou, nomBijou, descriptionBijou, prixBijou, stockBijou, type, dossierPhoto, nbPhotos, datepublication) {
        this.idBijou = idBijou;
        this.nomBijou = nomBijou;
        this.descriptionBijou = descriptionBijou;
        this.stockBijou = stockBijou;
        this.prixBijou = prixBijou;
        this.type = type;
        this.dossierPhoto = dossierPhoto;
        this.nbPhotos = nbPhotos;
        this.datepublication = datepublication;
    }

    // Méthode pour créer un bijou à partir d'un objet JSON
    static createBijouFromJSON(bijouJSON) {
        try {
            // Créez une nouvelle instance de la classe Bijou en utilisant les propriétés de l'objet JSON
            const nouveauBijou = new Bijou(
                bijouJSON.id,
                bijouJSON.name,
                bijouJSON.description,
                bijouJSON.price,
                bijouJSON.quantity,
                bijouJSON.type,
                bijouJSON.dossierPhoto,
                bijouJSON.nbPhotos
            );

            // Retourne le bijou créé
            return nouveauBijou;
        } catch (error) {
            console.error("Erreur lors de la création du bijou:", error);
            throw error;
        }
    }
}
/*
//Fonction pour afficher plus de bijoux sur la page
function afficherPlus() {

    // Exemple : Ajouter 3 bijoux supplémentaires
    for (let i = 0; i < 3; i++) {
        const nouveauBijou = document.createElement("div");
        nouveauBijou.className = "produit";
        nouveauBijou.innerHTML = `
            <img src="chemin/vers/nouveau-produit.jpg" alt="Nouveau Bijou">
            <h3>Nom du nouveau bijou</h3>
            <p>Description du nouveau bijou.</p>
        `;
        bijouxConteneur.appendChild(nouveauBijou);
    }

    nombreDeBijouxAffiches += 3;

    // Cacher le bouton "Voir Plus" si tous les bijoux sont affichés
    const boutonVoirPlus = document.getElementById("voir-plus");
    if (nombreDeBijouxAffiches >= nombreTotalDeBijoux) {
        boutonVoirPlus.style.display = "none";
    }
} */


// Nombre initial de bijoux affichés
let bijouAffiches = 10;
///Conteneur des bijoux sur la page html
const bijouxConteneur = document.getElementById("bijoux-conteneur");

window.onload = initialiserBijoux;


function sortAndDisplayBijoux() {
    const categorieSelect = document.getElementById("categorie-select");
    const triSelect = document.getElementById("tri-select");
    const selectedCategorie = categorieSelect.value;
    const selectedTri = triSelect.value;
    //Bijoux a afficher 
    let bijouxAafficher = {};

    let bijouxKeys = Object.keys(bijoux);

    if (selectedCategorie !== "allbij") {
        ///On récupère les clées du dictionnaire


        bijouxKeys.forEach(keys => {
            if (bijoux[keys].type === selectedCategorie) { //La catégorie du bijou est celle recherchée par l'utilisateur
                bijouxAafficher[keys] = bijoux[keys];
            }
        });
    }
    else {
        bijouxKeys.forEach(keys => {

            bijouxAafficher[keys] = bijoux[keys];

        });
    }
    if (selectedTri === "prix-croissant") {
        // On récupère les valeurs du dictionnaire sous forme de tableau pour les trier 
        const bijouxArray = Object.values(bijouxAafficher);
        bijouxArray.sort((a, b) => a.prixBijou - b.prixBijou);
        // Recréez un nouvel objet avec les valeurs triées
        bijouxAafficher = {};
        //On récré le dictionnaire 
        let i = 0;
        bijouxArray.forEach(bijou => {
            bijouxAafficher[i] = bijou;
            i += 1;
        });
    }
    else if (selectedTri === "prix-decroissant") {
        const bijouxArray = Object.values(bijouxAafficher);
        bijouxArray.sort((a, b) => b.prixBijou - a.prixBijou);
        // Recréez un nouvel objet avec les valeurs triées
        bijouxAafficher = {};
        //On récré le dictionnaire 
        let i = 0;
        bijouxArray.forEach(bijou => {
            bijouxAafficher[i] = bijou;
            i += 1;
        });
    }
    //} else if (selectedTri === "nouveaute") {
    //    bijoux.sort((a, b) => new Date(b.date) - new Date(a.date));
    //}

    displayBijoux(bijouxAafficher);
}

//Fonction d'affichage des bijoux
function displayBijoux(bijoux) {

    //Conteneur des bijoux dans l'HTML
    const conteneurBijoux = document.getElementById("listeBijoux");
    conteneurBijoux.innerHTML = "";

    //Clées du dictionnaire bijoux
    let bijouxKeys = Object.keys(bijoux);

    bijouxKeys.forEach(keys => {
        //Récupération de la valeur de la clée
        let bijou = bijoux[keys];

        //Création d'un conteneur pour le bijou
        const bijouElement = document.createElement("div");
        bijouElement.classList.add("produit");

        //Mise en page du bijoux

        //Image du bijou
        const totalImages = 3;
        const randomImageNumber = Math.floor(Math.random() * totalImages) + 1;
        const imagePath = `../images/Photosdescriptif${bijou.type}/${bijou.dossierPhoto}/1.jpg`;
        const imageElement = document.createElement("img");
        imageElement.src = imagePath;
        imageElement.alt = bijou.nomBijou;
        bijouElement.appendChild(imageElement);

        //Nom du bijou
        const titreBijou = document.createElement("h3");
        titreBijou.textContent = `${bijou.nomBijou}`;
        bijouElement.appendChild(titreBijou);

        //Prix
        const prixBijou = document.createElement('p');
        prixBijou.textContent = `${bijou.prixBijou}€`;
        bijouElement.appendChild(prixBijou);

        //Description
        const descriptionElement = document.createElement("button");
        descriptionElement.textContent = "Acheter";
        descriptionElement.addEventListener("click", function () {
            redirectToBijouPresentation(bijou.idBijou);
        });
        bijouElement.appendChild(descriptionElement);

        conteneurBijoux.appendChild(bijouElement);
    });
}
//Fonction lancer au chargement des élèments html
document.addEventListener("DOMContentLoaded", async function () {
    //Ajout d'évènement pour lancer la fonction de tri
    const categorieSelect = document.getElementById("categorie-select");
    const triSelect = document.getElementById("tri-select");

    categorieSelect.addEventListener("change", sortAndDisplayBijoux);
    triSelect.addEventListener("change", sortAndDisplayBijoux);

    // Récupération de 10 bijoux
    for (let i = 0; i < bijouAffiches; i++) {
        await fetchBijou(i);
    }
    displayBijoux(bijoux)
    console.log(bijoux);

    // Récupération de la catégorie à partir du paramètre d'URL
    var urlParams = new URLSearchParams(window.location.search);
    var categorie = urlParams.get('categorie');

    // Si une catégorie est spécifiée, sélectionnez cette option dans le menu déroulant
    if (categorie) {
        categorieSelect.value = categorie;
        // Appel de la fonction pour trier et afficher les bijoux en fonction de la catégorie sélectionnée
        sortAndDisplayBijoux();
    }
});


function redirectToBijouPresentation(bijouId) {
    window.location.href = "bijouxpresentation.html?bijouId=" + bijouId;
}

export { Bijou };