import { getPanierToken, setPanierToken } from "../js/cookies.js";
function tryCookie() {


    setPanierToken();

    const panierTokenValue = getPanierToken("PanierToken");

    console.log("Valeur du cookie PanierToken :", decodeURIComponent(panierTokenValue));
}

window.onload = tryCookie();
