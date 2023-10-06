var id;
var controls;
var num_image = 0;

function main() {
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


const initNewsSlider = () => {
    const imageList = document.querySelector(".slider-news-wrapper .image-list");
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
}

async function fetchInstagramPhotos() {
    const response = await fetch(`https://graph.instagram.com/me/media?fields=id,caption,media_type,media_url,thumbnail_url,permalink&access_token=IGQWRNeHBPRDNHZAFl6WU1KTlBYX3JrRVZAyZA3dkUXduUDcwUEN2WEh1SnJDTlhsamFkQlZAQSUpIcVJpTU8xazRaUTlpS0VlZAFdjTkhGZAExKaUF1ZAVRhdTNoTjFjTFpQY3VuYWlUWjRGM2pua0xNamNNU24tV0VBUnMZD&limit=5`);
    const data = await response.json();
    const items = data.data;
    const nonReels = items.filter(item => {
        return !(item.media_type === 'VIDEO' && item.media_url.includes('reel'));  // This line assumes that Reels media URLs contain the word 'reel'
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
