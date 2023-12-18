import { updatePanierCount } from "../js/panierDAO.js";
import { setPanierToken } from "./cookies.js";
import { redirectToBijouPresentation } from "../js/bijou.js";

var id;
var controls;
var num_image = 0;

function main() {
    updatePanierCount();
    initNewsSlider();
    fetchInstagramPhotos();
    id = setInterval(nextImage, 5000);
    controls = document.querySelectorAll(".slider-control");
    for (let i = 0; i < controls.length; i++) { //Méthode événementielle directe
        controls[i].onclick = click; //onclick appellera click
       }
    document.getElementById("auto").addEventListener("click", click_cb);
    
}


function print(nbr){
    let liste = document.querySelectorAll(".slider-panel");
    let buttons = document.querySelectorAll(".slider-button");
    for (let i = 0; i < liste.length; i++) {
        liste[i].classList.remove("active");
        controls[i].classList.remove("active");
        buttons[i].classList.remove("active");
    }
    liste[nbr].classList.add("active");
    controls[nbr].classList.add("active");
    buttons[nbr].classList.add("active");
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

// Slider des nouveautées
const initNewsSlider = async () => {
    try {
        const apiUrl = `https://elaparaibatest.fr/Bijoux/GetAllBijoux`;
        const response = await fetch(apiUrl);
        const data = await response.json();

        // Triez les bijoux filtrés par date de publication (du plus récent au moins récent)
        const sortedData = data.sort((a, b) => new Date(b.datepublication) - new Date(a.datepublication));

        // Sélectionnez la div d'images
        const imageList = document.querySelector(".slider-news-wrapper .image-list");

        // Effacez les images existantes
        imageList.innerHTML = "";

        // Créez des balises img pour chaque bijou dans les 5 premières données
        sortedData.slice(0, 5).forEach(bijou => {
            const imgElement = document.createElement("img");
            imgElement.src = `https://images.elaparaibatest.fr/Photosdescriptif${bijou.type}/${bijou.dossierPhoto}/1.jpg`; // Assurez-vous que le chemin est correct
            imgElement.alt = bijou.name;
            imgElement.classList.add("image-item");
            imageList.appendChild(imgElement);
            imgElement.addEventListener("click", function () {
                redirectToBijouPresentation(bijou.id);
            });
        });

        const slideButtons = document.querySelectorAll(".slider-news-wrapper .slide-button");
        const sliderScrollbar = document.querySelector(".slider-news-container .slider-news-scrollbar");
        const scrollbarThumb = sliderScrollbar.querySelector(".scrollbar-thumb");
        const maxScrollLeft = imageList.scrollWidth - imageList.clientWidth;

        // Handle scrollbar thumb drag
        scrollbarThumb.addEventListener("mousedown", (e) => {
            const startX = e.clientX;
            const thumbPosition = scrollbarThumb.offsetLeft;

            // update thumb position on mouse move
            const handleMouseMove = (e) => {
                const deltaX = e.clientX - startX;
                const newThumbPosition = thumbPosition + deltaX;
                const maxThumbPosition = sliderScrollbar.getBoundingClientRect().width - scrollbarThumb.offsetWidth;

                const boundedPosition = Math.max(0, Math.min(maxThumbPosition, newThumbPosition));
                scrollbarThumb.style.left = `${boundedPosition}px`;

                // update image scroll position based on thumb position
                const scrollPosition = (boundedPosition / maxThumbPosition) * maxScrollLeft;
                imageList.scrollLeft = scrollPosition;
            }

            const handleMouseUp = () => {
                document.removeEventListener("mousemove", handleMouseMove);
                document.removeEventListener("mouseup", handleMouseUp);
            }

            // add event listeners for drag interactions
            document.addEventListener("mousemove", handleMouseMove);
            document.addEventListener("mouseup", handleMouseUp);
        });

        // Slide images according to the slide button clicks
        slideButtons.forEach(button => {
            button.addEventListener("click", () => {
                const direction = button.id === "prev-slide" ? -1 : 1;
                const scrollAmount = imageList.clientWidth * direction;
                imageList.scrollBy({ left: scrollAmount, behavior: "smooth" });
            });
        });

        const handleSlideButtons = () => {
            slideButtons[0].style.display = imageList.scrollLeft <= 0 ? "none" : "block";
            slideButtons[1].style.display = imageList.scrollLeft >= maxScrollLeft ? "none" : "block";
        }

        // Update scrollbar thumb position based on image scroll
        const updateScrollThumbPosition = () => {
            const scrollPosition = imageList.scrollLeft;
            const thumbPosition = (scrollPosition / maxScrollLeft) * (sliderScrollbar.clientWidth - scrollbarThumb.offsetWidth);
            scrollbarThumb.style.left = `${thumbPosition}px`;
        }

        imageList.addEventListener("scroll", () => {
            handleSlideButtons();
            updateScrollThumbPosition();
        });

    } catch (error) {
        console.error("Erreur lors de la récupération des images depuis l'API:", error);
    }
};

// Fonction qui permet de récupérer les posts instagrams et les affichers
async function fetchInstagramPhotos() {
    const response = await fetch(`https://graph.instagram.com/me/media?fields=id,caption,media_type,media_url,thumbnail_url,permalink&access_token=IGQWROMDJOVnV1aWdvOTJfZAUYtYUljZAFZAEQldzeXR5a2pKQXZA2WHRaeG1NVlpxZAEdCelR6LVk1OFhzZAXlHSHl6YmpEdzFtVktzaHp5LVVTZAzFuV1ducU1saTFKcTNjZAy15ZAllBZAk4wd0JXbl9oaUk3SDVsVV9KOEUZD&limit=5`);
    const data = await response.json();
    const items = data.data;
    const nonReels = items.filter(item => {
        return !(item.media_type === 'VIDEO' && item.media_url.includes('reel'));
    });
    const container = document.getElementById('instagram-feed');
    nonReels.forEach(item => {
        const link = document.createElement('a');  
        link.href = item.permalink;  
        link.target = '_blank';  
        
        const image = document.createElement('img');
        image.src = item.media_type === 'VIDEO' ? item.thumbnail_url : item.media_url;  // Use thumbnail_url for videos, media_url for images
        
        link.appendChild(image);  
        container.appendChild(link);  
    });
}

window.onload = main;