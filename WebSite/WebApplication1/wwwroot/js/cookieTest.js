import { getPanierToken, setPanierToken } from "../js/cookies.js";

// Fichier qui permet de tester le fichier cookie.js avec la page test.html
function tryCookie() {

    const panierTokenValue = getPanierToken("PanierToken");

    console.log("Valeur du cookie PanierToken :", panierTokenValue);
}

window.onload = tryCookie();
