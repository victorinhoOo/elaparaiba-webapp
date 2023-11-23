var id;
var controls;
var num_image = 0;

function main() {
    id = setInterval(nextImage, 7000);
    controls = document.querySelectorAll(".slider-control");
    for (let i = 0; i < controls.length; i++) { //Méthode événementielle directe
        controls[i].onclick = click; //onclick appellera click
    }
    document.getElementById("auto").addEventListener("click", click_cb);
}

function print(nbr) {
    let liste = document.querySelectorAll(".slider-panel");
    for (let i = 0; i < liste.length; i++) {
        liste[i].classList.remove("active");
        controls[i].classList.remove("active");
    }
    liste[nbr].classList.add("active");
    controls[nbr].classList.add("active");
}
///evt = paramètre du click (ou on a cliqué, ...)
function click(evt) {
    let control = evt.target;
    let pos = [].indexOf.call(controls, control); // prend l'index de control dans controls
    num_image = pos;
    print(pos);
}

function nextImage() {
    num_image = (num_image + 1) % controls.length;
    print(num_image);
}

window.onload = main;


document.addEventListener('DOMContentLoaded', function () {
    const form = document.querySelector('form');

    // Création d'un Event pour envoyer le formulaire une fois rempli
    form.addEventListener('submit', async function (event) {
        event.preventDefault(); // Empêche l'envoi du formulaire par défaut

        const formData = new FormData(form); // Création d'un objet FormData pour le formulaire

        try {
            const response = await fetch('https://elaparaibatest.fr/EnvoyerFormulaireSurMesure', {
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





