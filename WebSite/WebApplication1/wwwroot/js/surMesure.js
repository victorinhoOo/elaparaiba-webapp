import { formToJson } from "./form.js";

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

function print(nbr){
    let liste = document.querySelectorAll(".slider-panel");
    for (let i = 0; i < liste.length; i++) {
        liste[i].classList.remove("active");
        controls[i].classList.remove("active");
       }
       liste[nbr].classList.add("active");
       controls[nbr].classList.add("active");
}
///evt = paramètre du click (ou on a cliqué, ...)
function click(evt){ 
    let control = evt.target;
    let pos = [].indexOf.call(controls, control); // prend l'index de control dans controls
    num_image = pos;
    print(pos);
}

function nextImage(){
    num_image = (num_image + 1) % controls.length;
    print(num_image);
}

window.onload = main;


document.addEventListener('DOMContentLoaded', function () {
    //Selection du form
    const form = document.querySelector('form');
    //Création d'un événement quand il est submit
    form.addEventListener('submit', function (event) {
        // Empêche l'envoi du formulaire par défaut
        event.preventDefault(); 
        const formJson = formToJson(form);
        delete formJson['Modeles']
        try {
            fetch('https://localhost:7252/Bijoux/EnvoyerFormulaireSurMesure', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: formJson
            })
        }
        catch (error) {
            console.error("Erreur de requête:", error);
        }  
    });
});
