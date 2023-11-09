async function setPanierToken() {
    // Définition du temps de vie du cookie
    const value = await fetchTokenPanier();
    const name = "PanierToken";
    const oneWeekInMilliseconds = 7 * 24 * 60 * 60 * 1000; // Durée d'une semaine en millisecondes
    const expires = new Date(Date.now() + oneWeekInMilliseconds).toUTCString();

    
    document.cookie = name + "=" + value + "; expires=" + expires + "; path=/; SameSite=True";

    return value;

}

// Fonction pour obtenir la valeur d'un cookie
function getPanierToken(tokenName) {
    var res ="";
    var nameEQ = tokenName + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) === ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) === 0) res = c.substring(nameEQ.length, c.length);
    }
    return res;
}

// Fonction pour supprimer un cookie
function eraseCookie(name) {
    document.cookie = name + '=; Max-Age=-99999999;';
}


// Fonction qui permet de créer un nouveau Token
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

export{ setPanierToken, getPanierToken };