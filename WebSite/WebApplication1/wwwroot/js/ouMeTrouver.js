import { updatePanierCount } from "../js/commun.js";

function ValidationSubmit()
{
    return validateForm();
}

function isValidEmail(email) {
    // Expression régulière pour vérifier le format de l'e-mail
    var regex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    return regex.test(email);
}

function validateForm() {
    var email = document.getElementById("email").value;
    if (isValidEmail(email)) {
        return true
    } else {
        return false
    }
}

window.onload = updatePanierCount();

document.addEventListener('DOMContentLoaded', function () {
    const form = document.querySelector('form');
    //Création d'un Event pour envoyer le formulaire une fois rempli
    form.addEventListener('submit',async function (event) {
        event.preventDefault(); // Empêche l'envoi du formulaire par défaut

        const formData = new FormData(form); // Création d'un objet FormData pour le formulaire

        try {
            const response = await fetch('https://elaparaibatest.fr/Bijoux/EnvoyerFormulaireOuMeTrouver', {
                method: 'POST',
                body: formData // Envoyer le formulaire avec le fichier
            });
            if (!response.ok) {
                throw new Error('Échec de la requête. Statut ' + response.status);
            }
            showPopup();
            console.log('Réponse réussie :', response);
            // Ajoutez ici le code que vous souhaitez exécuter en cas de succès
            //Erreur de communication avec le serveur
        } catch (error) {
            console.error('Erreur : Communication impossible avec le serveur',);
        }
    });

    // Ajout d'un gestionnaire d'événements pour fermer le pop-up
    const closePopupBtn = document.getElementById('closePopupBtn');
    closePopupBtn.addEventListener('click', function () {
        closePopup('.popup');
        form.reset();
    });
});

// Fonction pour afficher le pop-up
function showPopup() {
    var popup = document.getElementById("popup");
    popup.style.display = "block";
}

// Fonction pour fermer le pop-up
function closePopup() {
    var popup = document.getElementById("popup");
    popup.style.display = "none";
}

