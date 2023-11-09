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
    form.addEventListener('submit', function (event) {
        event.preventDefault(); // Empêche l'envoi du formulaire par défaut

        const formData = new FormData(form); // Création d'un objet FormData pour le formulaire

        

        fetch('https://localhost:7252/Bijoux/EnvoyerFormulaireOuMeTrouver', {
            method: 'POST',
            body: formData // Envoyer le formulaire avec le fichier
        })
            .catch(error => {
                console.error('Erreur:', error);
            });
    });
});

