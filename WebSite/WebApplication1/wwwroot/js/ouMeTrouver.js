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

