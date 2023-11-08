
import { getPanierToken, setPanierToken } from "./cookies";

function tryCookie() {


    setPanierToken();

    const panierTokenValue = getPanierToken("PanierToken");

    console.log("Valeur du cookie PanierToken :", panierTokenValue);
}

window.onload = tryCookie();
