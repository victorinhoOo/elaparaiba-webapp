﻿class Bijou {
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

}



document.addEventListener("DOMContentLoaded", () => {
    // Récupérer l'ID du bijou depuis l'URL
    const queryString = window.location.search;
    const urlParams = new URLSearchParams(queryString);
    const bijouId = urlParams.get('bijouId');

    // Récupérer les éléments dans lesquels afficher les détails du bijou
    const bijouImage = document.getElementById('bijouImage');
    const bijouName = document.getElementById('bijouName');
    const bijouPrice = document.getElementById('priceValue');
    const bijouQuantite = document.getElementById('quantiteValue');
    const btnPanier = document.getElementById('btnPanier');
    const bijouCategorie = document.getElementById('bijouCategorie');
    const bijouDescription = document.getElementById('bijouDescription');



    function afficherDetailsBijou(bijou) {
        bijouImage.src = `../images/Photosdescriptif${bijou.type}/${bijou.dossierPhoto}/1.jpg`;
        bijouName.textContent = bijou.nomBijou;
        bijouPrice.textContent = bijou.prixBijou;
        bijouQuantite.textContent = bijou.stockBijou;
        bijouCategorie.textContent = `Catégorie: ${bijou.type}`;
        bijouDescription.textContent = `Description: ${bijou.descriptionBijou}`;



        const miniaturesContainer = document.getElementById('miniatures');
        for (let i = 1; i <= bijou.nbPhotos; i++) {
            const miniature = document.createElement('img');
            miniature.src = `../images/Photosdescriptif${bijou.type}/${bijou.dossierPhoto}/${i}.jpg`;
            miniature.alt = `Miniature ${i}`;
            miniature.addEventListener('click', () => changerImagePrincipale(miniature.src));
            miniaturesContainer.appendChild(miniature);
        }
    }

    /*
    // Fonction pour ajouter le bijou au panier (pas encore implémentée)
    function ajouterAuPanier(bijou) {
        // Ajoutez ici la logique pour ajouter le bijou au panier
        console.log(`Bijou ajouté au panier: ${bijou.nomBijou}`);
    }*/

    // Fonction de requête pour récupérer les détails du bijou
    async function fetchBijouDetails() {
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
                data.nbPhotos
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
});

function changerImagePrincipale(nouvelleImageSrc) {
    bijouImage.src = nouvelleImageSrc;
}
