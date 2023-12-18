import { getPanierToken, setPanierToken } from "../js/cookies.js";
import { PanierItemFromJson, displayPanier } from "../js/panier.js";


//Liste des bijoux du panier
var bijouxPanier = [];

async function fetchPanier() {
    const apiUrl = `https://elaparaibatest.fr/Panier/ObtenirPanier?token=${getPanierToken("PanierToken")}`;
    try {
        //Requête vers l'Api
        const response = await fetch(apiUrl);
        //Traduction de la requête en json
        const panierJson = await response.json();
        //On parcours le éléments du json
        for (let i = 0; i < panierJson.length; i++) {
            //Création d'un panierItem
            const panierItem = PanierItemFromJson(panierJson[i]);
            //Ajout au panier
            bijouxPanier.push(panierItem);
        }

    } catch (error) {
        console.error("Erreur de requête:", error);
    }
}

async function updatePanierCount() {
    const apiUrl = `https://elaparaibatest.fr/Panier/ObtenirPanier?token=${getPanierToken("PanierToken")}`;

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

// Fonction pour ajouter le bijou au panier
async function ajouterAuPanier(bijou) {
    //Créer un paniertoken si l'utilisateur en a pas
    var panierTokenValue = getPanierToken("PanierToken");
 
    const apiUrl = `https://elaparaibatest.fr/Panier/AjouterAuPanier?token=${panierTokenValue}`; // URL du contrôleur
    try {
        // Requête vers l'API avec la méthode POST
        const response = await fetch(apiUrl, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                NbPhotos: bijou.nbPhotos,
                Id: bijou.idBijou,
                Name: bijou.nomBijou,
                Description: bijou.descriptionBijou,
                Price: bijou.prixBijou,
                Quantity: bijou.stockBijou,
                DatePublication: bijou.datepublication,
                Type: bijou.type,
                DossierPhoto: bijou.dossierPhoto
            }) // Convertit les données du bijou en chaîne JSON
        });

        if (!response.ok) {
            // La réponse n'est pas OK, appeler setPanierToken pour obtenir un nouveau token
            panierTokenValue = await setPanierToken();
            // Mettre à jour l'URL avec le nouveau token
            const newApiUrl = `https://elaparaibatest.fr/Panier/AjouterAuPanier?token=${panierTokenValue}`;
            // Refaire la requête avec le nouveau token
            const newResponse = await fetch(newApiUrl, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    NbPhotos: bijou.nbPhotos,
                    Id: bijou.idBijou,
                    Name: bijou.nomBijou,
                    Description: bijou.descriptionBijou,
                    Price: bijou.prixBijou,
                    Quantity: bijou.stockBijou,
                    DatePublication: bijou.datepublication,
                    Type: bijou.type,
                    DossierPhoto: bijou.dossierPhoto
                })
            });

            if (!newResponse.ok) {
                throw new Error('Réponse réseau non OK même après avoir renouvelé le token.');
            }

            console.log("Requête réussie après renouvellement du token.");
        } else {
            // La réponse est OK
            const responseData = await response.text();
            console.log(responseData);
            updatePanierCount();
        }
    } catch (error) {
        console.error("Erreur de requête:", error);
    }
}

// Fonction qui permet de supprimer un bijou du panier
async function supprimerDuPanier(id) {
    var panierTokenValue = getPanierToken("PanierToken");

    const apiUrl = `https://elaparaibatest.fr/Panier/SupprimerDuPanier?token=${panierTokenValue}&id=${id}`;

    try {
        const response = await fetch(apiUrl, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json',
            },
        });

        if (!response.ok) {
            throw new Error('Réponse réseau non ok');
        }

        // Trouver l'index du bijou dans le panier
        const index = bijouxPanier.findIndex(item => item.id === id);

        // Si l'index est trouvé, supprimer le bijou du panier local
        if (index !== -1) {
            bijouxPanier[index].quantite = bijouxPanier[index].quantite - 1; // Décrémenter la quantité
            if (bijouxPanier[index].quantite <= 0) {
                bijouxPanier.splice(index, 1); // Si la quantité est inférieure ou égale à zéro, supprimer complètement le bijou du panier
            }
        }
        updatePanierCount();
        // Mettre à jour l'affichage du panier
        displayPanier(bijouxPanier);

    } catch (error) {
        console.error("Erreur de requête:", error);
    }
}

export { fetchPanier, updatePanierCount, supprimerDuPanier, ajouterAuPanier, bijouxPanier};