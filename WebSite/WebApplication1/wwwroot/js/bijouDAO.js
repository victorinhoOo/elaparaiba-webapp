import { Bijou } from "../js/bijou.js";

///Liste des bijoux
var bijoux = {};


//Fonction communicante avec l'API bijou
async function fetchBijou(i) {
    const apiUrl = `https://elaparaibatest.fr/Bijoux/GetBijouWithId?id=${i}`;
    try {
        //Requête vers l'Api
        const response = await fetch(apiUrl);
        //Traduction de la requête en json
        const data = await response.json();

        //Création du nouveau bijou
        const nouveauBijou = new Bijou(
            data.id,
            data.name,
            data.description,
            data.price,
            data.quantity,
            data.type,
            data.dossierPhoto
        );

        //Ajout du bijou à la liste 
        bijoux[Object.keys(bijoux).length] = nouveauBijou;

    } catch (error) {
        console.error("Erreur de requête:", error);
    }
}

//Permet 
function initialiserBijoux() {
    tousLesBijoux = document.querySelectorAll(".produit");
}


export { Bijou, fetchBijou, initialiserBijoux, bijoux };
