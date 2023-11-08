// Fonction pour créer un cookie
export function setPanierToken() {
    // Définition du temps de vie du cookie
    const value = fetchTokenPanier();
    const name = "PanierToken";
    var expires = "";
    var date = new Date();
    date.setTime(date.getTime() + (time * 7 * 24 * 60 * 60 * 1000));
    expires = "; expires=" + date.toUTCString();
    document.cookie = name + "=" + value + expires + "; path=/";
}

// Fonction pour obtenir la valeur d'un cookie
export function getPanierToken() {
    var nameEQ = "PanierToken" + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) === ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) === 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}

// Fonction pour supprimer un cookie
function eraseCookie(name) {
    document.cookie = name + '=; Max-Age=-99999999;';
}


async function fetchTokenPanier() {
    var tokenPanier = "";
    const apiUrl = `https://localhost:7252/Panier/CreerPanierToken`;
    try {
        const response = await fetch(apiUrl);

        tokenPanier = await response.text();
    }
    catch (error) {
        console.error("Erreur de requête:", error);
    }
    return tokenPanier;
}	
