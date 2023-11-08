import { getPanierToken, setPanierToken } from "../js/cookies.js";
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
///Conteneur des bijoux sur la page html



//Désérialise pannierItemJson
function PanierItemFromJson(json){
    //Création du panierItem a partir du json
    const panierItem = new PanierItem(
        Bijou.createBijouFromJSON(json.bijou),
        json.quantite,
        json.id   
    )
    return panierItem;    
} 

//Fonction communicante avec l'API bijou
async function fetchPanier() {
    const apiUrl = `https://localhost:7252/Panier/ObtenirPanier?token=${getPanierToken("PanierToken")}`;
    try {
        //Requête vers l'Api
        const response = await fetch(apiUrl);
        //Traduction de la requête en json
        const panierJson = await response.json();
        //On parcours le éléments du json
        for(let i = 0; i < panierJson.length; i++){
            //Création d'un panierItem
            const panierItem = PanierItemFromJson(panierJson[i]);
            //Ajout au panier
            bijouxPanier.push(panierItem);
        }

    } catch (error) {
        console.error("Erreur de requête:", error);
    }
}




//Fonction d'affichage des bijoux
function displayPanier(bijoux) {
    //Div contenant tous les bijoux du panier
    const bijouPanierConteneur = document.getElementById("cart-items");
    bijouPanierConteneur.innerHTML = "";

    bijoux.forEach(bijou => {
        //Création d'un conteneur pour le bijou
        const bijouElement = document.createElement("div");
        bijouElement.classList.add("item");

        //Mise en page du bijoux

        //Image du bijou
        const imagePath = `../images/PhotosDescriptif${bijou.bijou.type}/${bijou.bijou.dossierPhoto}/1.jpg`;
        const imageElement = document.createElement("img");
        imageElement.src = imagePath;
        imageElement.alt = bijou.nomBijou;
        //Nom du bijou
        const nomBijou = document.createElement("span");
        nomBijou.classList.add("item-name");
        nomBijou.textContent = bijou.bijou.nomBijou;

        //Prix
        const prixBijou = document.createElement("span");
        prixBijou.classList.add("item-price");
        prixBijou.textContent = ` ` + bijou.bijou.prixBijou + `€`;

        //Quantité
        const quantiteBijou = document.createElement("span");
        quantiteBijou.classList.add("item-quantity");
        quantiteBijou.textContent = bijou.quantite + ` x `;

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Prix total
        const prixTotal = document.getElementById("total-price");                  //Faire la méthode qui permet de calculer le prix total
        prixTotal.textContent = ``; // F
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //Ajout des span à la div du bijou
        bijouElement.appendChild(imageElement);
        bijouElement.appendChild(nomBijou);
        bijouElement.appendChild(quantiteBijou);
        bijouElement.appendChild(prixBijou);

        //Ajout de l'item au panier
        bijouPanierConteneur.appendChild(bijouElement);
        console.log(bijou);
        
    });
}

async function fetchtotalPanier(){
    const apiUrl = `https://localhost:7252/Panier/ObtenirPanier?token=${getPanierToken("PanierToken")}`;
    try {
        //Requête vers l'Api
        const response = await fetch(apiUrl);
        //Traduction de la requête en json
        const panierJson = await response.json();
        //On parcours le éléments du json
        for(let i = 0; i < panierJson.length; i++){
            //Création d'un panierItem
            const panierItem = PanierItemFromJson(panierJson[i]);
            //Ajout au panier
            bijouxPanier.push(panierItem);
        }

    } catch (error) {
        console.error("Erreur de requête:", error);
    }
}

//Fonction lancer au chargement des élèments html
document.addEventListener("DOMContentLoaded", async function () {
    console.log("Panier avant fetch :"); 
    console.log(bijouxPanier); 
    await fetchPanier();
    console.log("Panier après fetch :"); 
    console.log(bijouxPanier); 
    displayPanier(bijouxPanier);
});

