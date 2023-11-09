import { getPanierToken, setPanierToken } from "../js/cookies.js";


document.addEventListener('DOMContentLoaded', function () {
    // Sélectionnez le bouton du menu burger et le menu principal
    const burgerMenu = document.querySelector('.burger-menu');
    const mainNav = document.querySelector('.mainNav');

    // Ajoutez un écouteur d'événements au bouton du menu burger
    burgerMenu.addEventListener('click', function () {
        // Ajoutez ou supprimez la classe 'active' du menu principal
        mainNav.classList.toggle('active');
    });
});

var bijouxPanier = [];

/*
async function updatePanierCount() {
    const panierCountElement = document.getElementById('panierCount'); // L'élément HTML où afficher le nombre d'articles

    try {
        const apiUrl = `https://localhost:7252/Panier/ObtenirPanier?token=${getPanierToken("PanierToken")}`;
        // Requête vers l'API pour obtenir les informations du panier
        const response = await fetch(apiUrl);
        const panierJson = await response.json();

        // Calculer le nombre total d'articles dans le panier
        const nombreArticles = panierJson.reduce((total, panierItem) => total + panierItem.quantite, 0);

        // Mettre à jour l'affichage
        panierCountElement.textContent = nombreArticles.toString();
    } catch (error) {
        console.error("Erreur lors de la mise à jour du panier:", error);
    }
}
*/

async function updatePanierCount() {
    const apiUrl = `https://localhost:7252/Panier/ObtenirPanier?token=${getPanierToken("PanierToken")}`;

    // Récupère l'élément HTML représentant le nombre d'articles dans le panier
    const panierCountElement = document.getElementById('panierCount'); // L'élément HTML où afficher le nombre d'articles

    try {
        // Requête vers l'API pour obtenir les informations du panier
        const response = await fetch(apiUrl);
        const panierJson = await response.json();

        // Calculer le nombre total d'articles dans le panier
        const nombreArticles = panierJson.reduce((total, panierItem) => total + panierItem.quantite, 0);

        // Mettre à jour l'affichage seulement si le nombre d'articles est supérieur à zéro
        if (nombreArticles > 0) {
            panierCountElement.textContent = nombreArticles.toString();
            panierCountElement.style.display = 'block'; // Afficher l'élément
        } else {
            panierCountElement.style.display = 'none'; // Masquer l'élément si le nombre d'articles est zéro
        }
    } catch (error) {
        console.error("Erreur lors de la mise à jour du panier:", error);
    }
}

window.onload = updatePanierCount();
export { updatePanierCount };