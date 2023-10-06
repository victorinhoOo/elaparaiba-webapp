let nombreDeBijouxAffiches = 6; // Nombre initial de bijoux affichés
const bijouxConteneur = document.getElementById("bijoux-conteneur");

function afficherPlus() {
    // Ajoutez plus de bijoux ici (par exemple, à partir d'une source de données)

    // Exemple : Ajouter 3 bijoux supplémentaires
    for (let i = 0; i < 3; i++) {
        const nouveauBijou = document.createElement("div");
        nouveauBijou.className = "produit";
        nouveauBijou.innerHTML = `
            <img src="chemin/vers/nouveau-produit.jpg" alt="Nouveau Bijou">
            <h3>Nom du nouveau bijou</h3>
            <p>Description du nouveau bijou.</p>
        `;
        bijouxConteneur.appendChild(nouveauBijou);
    }

    nombreDeBijouxAffiches += 3;

    // Cacher le bouton "Voir Plus" si tous les bijoux sont affichés
    const boutonVoirPlus = document.getElementById("voir-plus");
    if (nombreDeBijouxAffiches >= nombreTotalDeBijoux) {
        boutonVoirPlus.style.display = "none";
    }
}



window.onload = initialiserBijoux;


// Fake DAO pour les bijoux
class BijouDAO {
    constructor() {
        // Simulons une base de données de bijoux
        this.bijoux = [
            { id: 1, nom: "Boucles d'oreilles collection 'Géométrie abstraite'", description: "Description du bijou 1", prix: 69.0, categorie: "boucle-oreille", image: "../images/PhotosdescriptifBo/Bo18/20210311_161917.jpg", hoverImage: "../images/PhotosdescriptifBo/Bo18/20230908_182247.jpg" },
            { id: 2, nom: "Bracelet, Manchette", description: "Description du bijou 2", prix: 106.0, categorie: "bracelet", image: "../images/PhotosDescriptifBracelets/Bra39/20230911_105440.jpg", hoverImage: "../images/PhotosDescriptifBracelets/Bra39/20230914_164854.jpg" },
            { id: 3, nom: "Collier, collection 'Élégance'", description: "Description du bijou 3", prix: 49.0, categorie: "collier", image: "../images/PhotosDescriptifsColliers/Col27/20211125_185405.jpg", hoverImage: "../images/PhotosDescriptifsColliers/Col27/20230915_155739.jpg" },
            { id: 4, nom: "Bague, collection 'Élégance'", description: "Description du bijou 4", prix: 78.0, categorie: "bague", image: "../images/PhotosdescriptifsBagues/Bague 62/1.jpg", hoverImage: "../images/PhotosdescriptifsBagues/Bag62/20220826_153732.jpg" },
            { id: 5, nom: "Bracelet, collection 'Nature et Botanique'", description: "Description du bijou 5", prix: 52.0, categorie: "bracelet", image: "../images/PhotosDescriptifBracelets/Bra74/20230911_104855.jpg", hoverImage: "../images/PhotosDescriptifBracelets/Bra74/20230823_105248.jpg" },
            { id: 6, nom: "Boucles d'oreilles collection 'Nature et Botanique'", description: "Description du bijou 6", prix: 115.0, categorie: "boucle-oreille", image: "../images/PhotosdescriptifBo/Bo65/20230908_181612.jpg", hoverImage: "../images/PhotosdescriptifBo/Bo65/20230519_175503.jpg" },
            { id: 7, nom: "Bracelet, collection 'Entrelacs'", description: "Description du bijou 7", prix: 50.0, categorie: "bracelet", image: "../images/PhotosDescriptifBracelets/Bra72/20230911_104603.jpg", hoverImage: "../images/PhotosDescriptifBracelets/Bra72/20230823_104738.jpg" },
            { id: 8, nom: "Collier, collection 'Nature et Botanique'", description: "Description du bijou 8", prix: 70.0, categorie: "collier", image: "../images/PhotosDescriptifsColliers/Col60/20230823_102805.jpg", hoverImage: "../images/PhotosDescriptifsColliers/Col60/20230915_160038.jpg" },
            { id: 9, nom: "Collier, collection 'Nature et Botanique'", description: "Description du bijou 9", prix: 62.0, categorie: "collier", image: "../images/PhotosDescriptifsColliers/Col66/20230823_104043.jpg", hoverImage: "../images/PhotosDescriptifsColliers/Col66/20230915_160955.jpg" },
            { id: 10, nom: "Boucles d'oreilles collection 'Nature et Botanique'", description: "Description du bijou 10", prix: 85.0, categorie: "boucle-oreille", image: "../images/PhotosdescriptifBo/Bo67/20230823_111427.jpg", hoverImage: "../images/PhotosdescriptifBo/Bo67/20230823_110306.jpg" },
            { id: 11, nom: "Bague, collection 'Géométrie abstraite'", description: "Description du bijou 11", prix: 56.0, categorie: "bague", image: "../images/PhotosdescriptifsBagues/Bague 71/1.jpg", hoverImage: "../images/PhotosdescriptifsBagues/Bag71/20221209_115329.jpg" },
            { id: 12, nom: "Bague, collection 'Élégance'", description: "Description du bijou 12", prix: 56.0, categorie: "bague", image: "../images/PhotosdescriptifsBagues/Bague 78/1.jpg", hoverImage: "../images/PhotosdescriptifsBagues/Bag78/20230914_172832.jpg" },
        ];
    }

    // Récupérer tous les bijoux
    getAllBijoux() {
        return this.bijoux;
    }

    // Récupérer un bijou par son ID
    getBijouById(id) {
        return this.bijoux.find(bijou => bijou.id === id);
    }

    // Récupérer les bijoux par catégorie
    getBijouxByCategorie(categorie) {
        return this.bijoux.filter(bijou => bijou.categorie === categorie);
    }

    // Ajouter un nouveau bijou
    addBijou(nom, description, prix, categorie, image) {
        const newBijou = {
            id: this.bijoux.length + 1, // Simulons l'attribution automatique d'un nouvel ID
            nom: nom,
            description: description,
            prix: prix,
            categorie: categorie,
            image: image,
        };

        this.bijoux.push(newBijou);
        return newBijou;
    }

    // Mettre à jour un bijou
    updateBijou(id, nom, description, prix, categorie, image) {
        const bijouIndex = this.bijoux.findIndex(bijou => bijou.id === id);

        if (bijouIndex !== -1) {
            this.bijoux[bijouIndex] = {
                id: id,
                nom: nom,
                description: description,
                prix: prix,
                categorie: categorie,
                image: image,
                hoverImage: hoverImage,
            };
            return true; // La mise à jour a réussi
        }

        return false; // Le bijou avec l'ID spécifié n'a pas été trouvé
    }

    // Supprimer un bijou par son ID
    deleteBijou(id) {
        const initialLength = this.bijoux.length;
        this.bijoux = this.bijoux.filter(bijou => bijou.id !== id);
        return this.bijoux.length !== initialLength; // Retourne true si un bijou a été supprimé
    }

    triBijouxParPrix(ordre) {
        if (ordre === "prix-croissant") {
            this.bijoux.sort((a, b) => a.prix - b.prix);
        } else if (ordre === "prix-decroissant") {
            this.bijoux.sort((a, b) => b.prix - a.prix);
        }
    }

    geHoverImagebyId(id) {
        return this.bijoux.hoverImage.find(bijou => bijou.id === id)
    }
}
let bijouDAO;

document.addEventListener('DOMContentLoaded', function () {
    try {
        // Supposons que vous avez déjà la classe BijouDAO définie ici
        bijouDAO = new BijouDAO();

        // Récupérer tous les bijoux
        const tousLesBijoux = bijouDAO.getAllBijoux();

        // Sélectionner l'élément HTML où vous souhaitez afficher les bijoux
        const listeBijouxElement = document.getElementById('listeBijoux');

        if (!listeBijouxElement) {
            throw new Error("L'élément avec l'ID 'listeBijoux' n'a pas été trouvé.");
        }

        // Générer des règles CSS dynamiques pour chaque bijou
        const styleElement = document.createElement('style');
        document.head.appendChild(styleElement);

        // Parcourir tous les bijoux et les ajouter à l'élément HTML
        tousLesBijoux.forEach(bijou => {
            const classeBijou = `${bijou.id}`;
            // Créer un élément pour chaque bijou
            const bijouElement = document.createElement('div');
            bijouElement.classList.add('produit');

            // Ajouter le contenu du bijou à l'élément
            bijouElement.innerHTML = `
                <img class="${classeBijou}" src="${bijou.image}" alt="${bijou.nom}">
                <h3>${bijou.nom}</h3>
                <p>${bijou.prix}€</p> <!-- Ajouter le prix du bijou -->
                <button onclick="redirectToBijouPresentation(${classeBijou})">Acheter</button>
            `;

            // Ajouter l'élément du bijou à la liste des bijoux
            listeBijouxElement.appendChild(bijouElement);
        });
    } catch (error) {
        console.error("Une erreur s'est produite :", error.message);
    }
});

// Fonction d'initialisation appelée au chargement de la page
function initialiserBijoux() {
    tousLesBijoux = document.querySelectorAll('.produit');
}

function trierBijoux() {
    const categorieSelect = document.getElementById("categorie-select");
    const triSelect = document.getElementById("tri-select");
    const selectedCategorie = categorieSelect.value;
    const selectedTri = triSelect.value;

    // Obtenez les bijoux en fonction de la catégorie sélectionnée
    let bijouxAffiches;
    if (selectedCategorie === "allbij") {
        bijouxAffiches = bijouDAO.getAllBijoux();
    } else {
        bijouxAffiches = bijouDAO.getBijouxByCategorie(selectedCategorie);
    }

    // Triez les bijoux en fonction de l'option de tri sélectionnée
    if (selectedTri) {
        bijouDAO.triBijouxParPrix(selectedTri);
    }

    // Obtenez la liste HTML où vous allez afficher les bijoux
    const listeBijouxElement = document.getElementById("listeBijoux");

    // Effacez le contenu actuel de la liste
    listeBijouxElement.innerHTML = "";

    // Affichez les bijoux triés et filtrés
    bijouxAffiches.forEach(bijou => {
        const classeBijou = `${bijou.id}`;
        // Créer un élément pour chaque bijou
        const bijouElement = document.createElement('div');
        bijouElement.classList.add('produit');

        // Ajouter le contenu du bijou à l'élément
        bijouElement.innerHTML = `
            <img src="${bijou.image}" alt="${bijou.nom}">
            <h3>${bijou.nom}</h3>
            <p>${bijou.prix}€</p>
            <button onclick="redirectToBijouPresentation(${classeBijou})">Acheter</button>
        `;

        // Ajouter l'élément du bijou à la liste des bijoux
        listeBijouxElement.appendChild(bijouElement);
    });
}

function redirectToBijouPresentation(bijouId) {
    window.location.href = 'bijouxpresentation.html?bijouId=' + bijouId;
}





/* const bijouxConteneur = document.getElementById("bijoux-conteneur");

window.onload = initialiserBijoux;

let bijouxList = [];

class Bijou {
    constructor(idBijou, nomBijou, matiereBijou, descriptionBijou, stockBijou, prixBijou) {
        this.idBijou = idBijou;
        this.nomBijou = nomBijou;
        this.matiereBijou = matiereBijou;
        this.descriptionBijou = descriptionBijou;
        this.stockBijou = stockBijou;
        this.prixBijou = prixBijou;
    }
}

function sortAndDisplayBijoux() {
    const categorieSelect = document.getElementById("categorie-select");
    const triSelect = document.getElementById("tri-select");
    const selectedCategorie = categorieSelect.value;
    const selectedTri = triSelect.value;

    let filteredBijoux = [bijouxList];

    if (selectedCategorie !== "allbij") {
        filteredBijoux = filteredBijoux.filter(bijou => bijou.categorie === selectedCategorie);
    }

    if (selectedTri === "prix-croissant") {
        filteredBijoux.sort((a, b) => a.price - b.price);
    } else if (selectedTri === "prix-decroissant") {
        filteredBijoux.sort((a, b) => b.price - a.price);
    } else if (selectedTri === "nouveaute") {
        filteredBijoux.sort((a, b) => new Date(b.date) - new Date(a.date));
    }

    displayBijoux(filteredBijoux);
}

document.addEventListener("DOMContentLoaded", async function () {

    async function fetchBijou(i) {
        const apiUrl = `https://localhost:7252/Bijoux/GetBijouWithId?id=${i}`;
        try {
            const response = await fetch(apiUrl);
            const data = await response.json();

            bijouxList.push(data);
            displayBijoux(bijouxList);
        } catch (error) {
            console.error("Erreur de requête:", error);
        }
    }

    function displayBijoux(bijoux) {
        const resultElement = document.getElementById("listeBijoux");
        resultElement.innerHTML = "";

        bijoux.forEach(data => {
            const bijouElement = document.createElement("div");
            bijouElement.classList.add("produit");

            const bijouName = data.name;
            const totalImages = 3;
            const randomImageNumber = Math.floor(Math.random() * totalImages) + 1;
            const imagePath = `../images/PhotosdescriptifsBagues/${bijouName}/${randomImageNumber}.jpg`;

            const imageElement = document.createElement("img");
            imageElement.src = imagePath;
            imageElement.alt = bijouName;
            bijouElement.appendChild(imageElement);

            const titreBijou = document.createElement("h3");
            titreBijou.textContent = `${data.name}`;
            bijouElement.appendChild(titreBijou);

            const prixBijou = document.createElement('p');
            prixBijou.textContent = `${data.price}€`;
            bijouElement.appendChild(prixBijou);

            const descriptionElement = document.createElement("button");
            descriptionElement.textContent = "Acheter";
            descriptionElement.addEventListener("click", function () {
                redirectToBijouPresentation(data.id);
            });
            bijouElement.appendChild(descriptionElement);

            resultElement.appendChild(bijouElement);
        });
    }

    for (let i = 1; i <= 10; i++) {
        await fetchBijou(i);
    }
    console.log(bijouxList);
});


// Fonction d'initialisation appelée au chargement de la page
function initialiserBijoux() {
  tousLesBijoux = document.querySelectorAll(".produit");
}


function redirectToBijouPresentation(bijouId) {
  window.location.href = "bijouxpresentation.html?bijouId=" + bijouId;
}
 */