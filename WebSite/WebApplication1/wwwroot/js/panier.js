class Bijou {
    constructor(idBijou, nomBijou, descriptionBijou, prixBijou, stockBijou, type, dossierPhoto, nbPhotos) {
      this.idBijou = idBijou;
      this.nomBijou = nomBijou;
      this.descriptionBijou = descriptionBijou;
      this.stockBijou = stockBijou;
      this.prixBijou = prixBijou;
      this.type = type;
      this.dossierPhoto = dossierPhoto;
      this.nbPhotos = nbPhotos;
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

//Représente les bijoux dans le panier
class PanierItem {
    constructor(bijou, quantite, id){
        this.bijou = bijou;
        this.quantite = quantite;
        this.id=id;
    }
}

//Liste des bijoux du panier
var bijouxPanier = [];


//Désérialise pannierItemJson
function PanierItemFromJson(json){
    //Création du panierItem a partir du json
    const panierItem = new PanierItem(
        createBijouFromJSON(json.Bijou),
        json.quantite,
        json.id   
    )
    return panierItem;    
} 

//Fonction communicante avec l'API bijou
async function fetchPanier() {
    const apiUrl = `https://localhost:7252/Panier/ObtenirPanier`;
    try {
        //Requête vers l'Api
        const response = await fetch(apiUrl);
        //Traduction de la requête en json
        const panierJson = await response.json();
        //On parcours le éléments du json
        for(let i = 0; i < data.length; i++){
            //Création d'un panierItem
            panierItem = PanierItemFromJson(panierJson[i]);
            //Ajout au panier
            bijouxPanier.push(panierItem);
        }

    } catch (error) {
        console.error("Erreur de requête:", error);
    }
}


window.onload = initialiserBijoux;


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


// Fonction d'initialisation appelée au chargement de la page
function initialiserBijoux() {
    tousLesBijoux = document.querySelectorAll(".produit");
}


function redirectToBijouPresentation(bijouId) {
    window.location.href = "bijouxpresentation.html?bijouId=" + bijouId;
}

