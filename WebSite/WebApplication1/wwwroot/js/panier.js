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
        for(let i = 0; i < panierJson.length; i++){
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
function displayPannier(bijoux) {
    //Div contenant tous les bijoux du panier
    const bijouPanierConteneur = document.getElementById("cart-items");
    bijouPanierConteneur.innerHTML = "";

    bijouxPanier.forEach(bijou => {
        //Création d'un conteneur pour le bijou
        const bijouElement = document.createElement("div");
        bijouElement.classList.add("item");

        //Mise en page du bijoux

        //Image du bijou
        // const totalImages = 3;
        // const randomImageNumber = Math.floor(Math.random() * totalImages) + 1;
        // const imagePath = `../images/Photosdescriptif${bijou.type}/${bijou.dossierPhoto}/1.jpg`;
        // const imageElement = document.createElement("img");
        // imageElement.src = imagePath;
        // imageElement.alt = bijou.nomBijou;
        // bijouElement.appendChild(imageElement);

        //Nom du bijou
        const nomBijou = document.createElement("span");
        nomBijou.classList.add("item-name");
        nomBijou.textContent = bijou.nomBijou;

        //Prix
        const prixBijou = document.createElement("span");
        prixBijou.classList.add("item-price");
        prixBijou.textContent = bijou.price * bijou.quantite;

        //Quantité
        const quantiteBijou = document.createElement("span");
        quantiteBijou.classList.add("item-quantity");
        prixBijou.textContent = bijou.quantite;

        //Ajout des span à la div du bijou
        bijouElement.appendChild(prixBijou);
        bijouElement.appendChild(nomBijou);
        bijouElement.appendChild(quantiteBijou);

        //Ajout de l'item au panier
        bijouPanierConteneur.appendChild(bijouElement);
    });
}
//Fonction lancer au chargement des élèments html
document.addEventListener("DOMContentLoaded", async function () {
    fetchPanier();
    displayPannier(bijouxPanier);
});

