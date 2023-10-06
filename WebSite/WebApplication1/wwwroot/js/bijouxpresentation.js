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

// Supposons que vous avez déjà instancié votre FakeDAO
bijouDAO = new BijouDAO();

// Fonction pour mettre à jour la page en fonction du bijou sélectionné
function updatePage(bijouId) {
    bijou = bijouDAO.getBijouById(bijouId);

    document.getElementById('bijouTitle').innerText = bijou.nom + ' - Bijou Presentation';
    document.getElementById('bijouName').innerText = bijou.nom;
    document.getElementById('bijouImage').src = bijou.image;
    document.getElementById('bijouDescription').innerText = 'Description: ' + bijou.description;
    document.getElementById('priceValue').innerText = bijou.prix.toFixed(2);
    document.getElementById('bijouCategorie').innerText = 'Catégories: ' + bijou.categorie;
}

// Récupérez l'ID du bijou à partir de l'URL
const urlParams = new URLSearchParams(window.location.search);
const bijouId = parseInt(urlParams.get('bijouId')) || 1;

// Mettez à jour la page avec le bijou correspondant
document.addEventListener('DOMContentLoaded', function () {
    // Votre code ici
    updatePage(bijouId);
});
    

}