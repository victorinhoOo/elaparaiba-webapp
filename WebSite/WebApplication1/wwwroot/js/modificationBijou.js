import { Bijou } from "./bijoupresentation.js";

// Fonction de requête pour récupérer les détails du bijou
async function fetchBijouDetails(bijouId) {
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
            data.nbPhotos,
            data.datepublication
        );

        return bijou;
        
    } catch (error) {
        console.error("Erreur de requête:", error);
    }
}


function displayBijouDetails(bijou){
    document.getElementById('nom').value = bijou.nomBijou;
    document.getElementById('description').value = bijou.descriptionBijou;
    document.getElementById('quantite').value = bijou.stockBijou;
    document.getElementById('type').value = bijou.type;
    document.getElementById('matiere').value = bijou.matiere;
    document.getElementById('prix').value = bijou.prixBijou;
    document.getElementById('datePublication').value = bijou.datepublication;
}

document.addEventListener("DOMContentLoaded", async function () {
     // Récupérer l'ID du bijou depuis l'URL
     const queryString = window.location.search;
     const urlParams = new URLSearchParams(queryString);
     const bijouId = urlParams.get('bijouId');
    //On modifie un bijou existant
    if(bijouId != -1){
        const bijou =  await fetchBijouDetails(bijouId);
        displayBijouDetails(bijou);
    }
    
});
