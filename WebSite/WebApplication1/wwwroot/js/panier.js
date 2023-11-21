import { getPanierToken, setPanierToken } from "../js/cookies.js";
import { fetchPanier, supprimerDuPanier, updatePanierCount, bijouxPanier } from "../js/panierDAO.js";
import { Bijou } from "../js/bijou.js";

//Représente les bijoux dans le panier
class PanierItem {
    constructor(bijou, quantite, id) {
        this.bijou = bijou;
        this.quantite = quantite;
        this.id = id;
    }
}

function PanierItemFromJson(json) {
    //Création du panierItem a partir du json
    const panierItem = new PanierItem(
        Bijou.createBijouFromJSON(json.bijou),
        json.quantite,
        json.id
    )
    return panierItem;
}

//Renvoi le cout total du panier
function CoutPanier() {
    var coutPanier = 0;
    bijouxPanier.forEach(item => {
        coutPanier += item.bijou.prixBijou * item.quantite;
    });
    return coutPanier;
}

//Fonction d'affichage des bijoux
async function displayPanier(bijoux) {
    //Créer un paniertoken si l'utilisateur en a pas
    var panierTokenValue = getPanierToken("PanierToken");
    if (panierTokenValue === "") { //Le token n'est pas définie
        panierTokenValue = await setPanierToken();
    }
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


       


        // Bouton qui permet de supprimer un bijou du panier
        const supprimerBijou = document.createElement("button");
        const favicon = document.createElement("i");
        supprimerBijou.setAttribute("class", "btn-supprimer-bijou")
        favicon.setAttribute("class", "fas fa-trash");
        supprimerBijou.appendChild(favicon);

        //Ajout des span à la div du bijou
        bijouElement.appendChild(imageElement);
        bijouElement.appendChild(nomBijou);
        bijouElement.appendChild(quantiteBijou);
        bijouElement.appendChild(prixBijou);
        bijouElement.appendChild(supprimerBijou);
        supprimerBijou.addEventListener('click', () => supprimerDuPanier(bijou.id));

        //Ajout de l'item au panier
        bijouPanierConteneur.appendChild(bijouElement);
        console.log(bijou);

    });
    //Prix total
    const prixTotal = document.getElementById("total-price");
    prixTotal.textContent = CoutPanier() + `€`;
}



// Créé une session stripe en envoyant le tokenPanier de l'utilisateur
async function createStripeCheckoutSession() {
    const apiurl = `https://localhost:7252/CreateCheckoutSession?token=${getPanierToken("PanierToken")}`;
    try {
        // Requête vers l'API
        const response = await fetch(apiurl, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },

        });

        // Vérifie si la requête a réussi
        if (!response.ok) {
            throw new Error(`Erreur HTTP! statut: ${response.status}`);
        }

        // Obtient l'URL de la session Stripe du corps de la réponse
        const url = await response.text();


        // Redirige l'utilisateur vers la page de paiement Stripe
        window.location.href = url;
    } catch (error) {
        console.error("Erreur lors de la création de la session Stripe Checkout:", error);
    }
}

// Ecouteur du bouton checkout-button, execute la fonction correspondante
document.addEventListener('DOMContentLoaded', (event) => {
    const checkoutButton = document.getElementById('checkout-button');
    if (checkoutButton) {
        checkoutButton.addEventListener('click', (event) => {
            createStripeCheckoutSession();
        });
    }
});

//Fonction lancer au chargement des élèments html
document.addEventListener("DOMContentLoaded", async function () {
    await fetchPanier();
    displayPanier(bijouxPanier);
});



// On actualise la quantité de bijoux dans le panier au chargement de la page
window.onload = updatePanierCount();

export { PanierItemFromJson, displayPanier };