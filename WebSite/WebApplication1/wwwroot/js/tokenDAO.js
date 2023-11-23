// Fonction qui permet de cr�er un nouveau Token
async function fetchTokenPanier() {
    var tokenPanier = "";
    const apiUrl = `https://localhost:7252/Panier/CreerPanierToken`;
    try {
        const response = await fetch(apiUrl);

        tokenPanier = await response.text();
    }
    catch (error) {
        console.error("Erreur de requete:", error);
    }
    return tokenPanier;
}	

export { fetchTokenPanier };