import { updatePanierCount, ajouterAuPanier } from "../js/panierDAO.js";
import { Bijou } from "../js/bijou.js";


document.addEventListener("DOMContentLoaded", () => {
        // Récupérer l'ID du bijou depuis l'URL
        const queryString = window.location.search;
        const urlParams = new URLSearchParams(queryString);
        const bijouId = urlParams.get('bijouId');

    // Associe des constantes aux éléments contenant l'ID correspondant dans l'HTML
    const bijouImage = document.getElementById('bijouImage');
    const bijouName = document.getElementById('bijouName');
    const bijouPrice = document.getElementById('priceValue');
    const bijouQuantite = document.getElementById('quantiteValue');
    const btnPanier = document.getElementById('btnPanier');
    const bijouCategorie = document.getElementById('bijouCategorie');
    const bijouDescription = document.getElementById('bijouDescription');
    const bijouImageA = document.getElementById('aFlyPanier');


    // Affiche les différentes informations du bijou en complétant les Id de la page HTML
    function afficherDetailsBijou(bijou) {
        bijouImage.src = `https://images.elaparaibatest.fr/Photosdescriptif${bijou.type}/${bijou.dossierPhoto}/1.jpg`;
        bijouName.textContent = bijou.nomBijou;
        bijouPrice.textContent = bijou.prixBijou;
        bijouQuantite.textContent = bijou.stockBijou;
        if (bijou.stockBijou == 0) {
            const quantiteVide = "Ce bijou n'est plus disponible";
            bijouQuantite.textContent = quantiteVide;
        }
        bijouCategorie.textContent = `Catégorie: ${bijou.type}`;
        bijouDescription.textContent = `Description: ${bijou.descriptionBijou}`;
        



        // Permet d'afficher les différentes images de chaque bijou sous forme de miniature
        const miniaturesContainer = document.getElementById('miniatures');
        for (let i = 1; i <= bijou.nbPhotos; i++) {
            const miniature = document.createElement('img');
            miniature.src = `https://images.elaparaibatest.fr/Photosdescriptif${bijou.type}/${bijou.dossierPhoto}/${i}.jpg`;
            miniature.alt = `Miniature ${i}`;
            miniature.addEventListener('click', () => changerImagePrincipale(miniature.src));
            miniaturesContainer.appendChild(miniature);
        }
    }
    // Fonction de requête pour récupérer les détails du bijou
    async function fetchBijouDetails() {
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

            // Afficher les détails du bijou
            afficherDetailsBijou(bijou);

            // Ajouter un écouteur d'événement pour le bouton "Ajouter au panier"
            btnPanier.addEventListener('click', () => ajouterAuPanier(bijou));
        } catch (error) {
            console.error("Erreur de requête:", error);
        }
    }

    

    // Appeler la fonction de requête pour récupérer les détails du bijou
    fetchBijouDetails();

    updatePanierCount();
});

document.addEventListener('DOMContentLoaded', function () {
    var btnPanier = document.getElementById('btnPanier');
    var bijouImage = document.getElementById('bijouImage');
    var panier = document.getElementById('panier');

    btnPanier.addEventListener('click', function (e) {
        e.preventDefault();

        // On clone l'image
        var clonedImage = bijouImage.cloneNode(true);
        clonedImage.setAttribute('id', 'fly-to-basket');
        document.body.appendChild(clonedImage);

        // On récupère la position initiale de l'image et du panier
        var initialImagePosition = bijouImage.getBoundingClientRect();
        var initialPanierPosition = panier.getBoundingClientRect();

        // On paramètre le style de l'image
        clonedImage.style.position = 'absolute';
        clonedImage.style.top = initialImagePosition.top + 'px';
        clonedImage.style.left = initialImagePosition.left + 'px';
        clonedImage.style.width = initialImagePosition.width + 'px';
        clonedImage.style.height = initialImagePosition.height + 'px';
        clonedImage.style.transition = 'all 0.5s ease-in-out';
        clonedImage.style.objectFit = 'cover';

        // On calcule le rapport hauteur largeur
        var offset = { x: 5, y: 15 };

        // On déplace l'image jusqu'au rapport calculé
        clonedImage.style.transform = 'translate(' + offset.x + 'px, ' + offset.y + 'px)';

        // On déplace l'image avec un système de défillement 
        setTimeout(function () {
            clonedImage.style.transform = 'translate(' + (initialPanierPosition.left - initialImagePosition.left) + 'px, ' + (initialPanierPosition.top - initialImagePosition.top) + 'px)';
            clonedImage.style.width = '20px';
            clonedImage.style.height = '20px';
        }, 100);

        // L'image est supprimé à la fin de l'animation
        setTimeout(function () {
            clonedImage.parentNode.removeChild(clonedImage);
        }, 600);
    });
});
function changerImagePrincipale(nouvelleImageSrc) {
    bijouImage.src = nouvelleImageSrc;
}
