document.addEventListener('DOMContentLoaded', function () {
    // Sélectionnez le bouton du menu burger et le menu principal
    const burgerMenu = document.querySelector('.burger-menu');
    const mainNav = document.querySelector('.mainNav');

    // Ajoutez un écouteur d'événements au bouton du menu burger
    burgerMenu.addEventListener('click', function () {
        // Ajoutez ou supprimez la classe 'active' du menu principal
        mainNav.classList.toggle('active');
    });
});

window.onload = main;