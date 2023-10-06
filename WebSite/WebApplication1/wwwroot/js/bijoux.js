const bijouxConteneur = document.getElementById("bijoux-conteneur");

window.onload = initialiserBijoux;

class Bijou {
  constructor(
    idBijou,
    nomBijou,
    matiereBijou,
    descriptionBijou,
    stockBijou,
    prixBijou
  ) {
    this.idBijou = idBijou;
    this.nomBijou = nomBijou;
    this.matiereBijou = matiereBijou;
    this.descriptionBijou = descriptionBijou;
    this.stockBijou = stockBijou;
    this.prixBijou = prixBijou;
  }
}

document.addEventListener("DOMContentLoaded", async function () {
  // Notez le "async" ici
  async function fetchBijou(i) {
    const apiUrl = `https://localhost:7252/Bijoux/GetBijouWithId?id=${i}`;
    try {
      const response = await fetch(apiUrl);
      const data = await response.json();

      const bij = new Bijou(data); // Si vous avez l'intention d'utiliser cette instance, faites-le
      const resultElement = document.getElementById("listeBijoux");

      const bijouElement = document.createElement("div");
      bijouElement.classList.add("produit");
        
      const bijouName = data.name; // Supposons que c'est "Bague 61" par exemple

      const totalImages = 3; // Remplacez par le nombre total d'images si vous le connaissez
      const randomImageNumber = Math.floor(Math.random() * totalImages) + 1; // Génère un nombre entre 1 et totalImages

        const imagePath = `../images/PhotosdescriptifsBagues//${bijouName}/${randomImageNumber}.jpg`; // Construisez le chemin de l'image

      const imageElement = document.createElement("img");
      imageElement.src = imagePath;
      imageElement.alt = bijouName; // Ajoutez un texte alternatif pour l'accessibilité

      bijouElement.appendChild(imageElement); // Ajoutez l'élément img à bijouElement

      const titreBijou = document.createElement("h3");
      titreBijou.textContent = `${data.name}`;
      bijouElement.appendChild(titreBijou); // Ajoutez titreBijou à bijouElement


        const prixBijou = document.createElement('p');
      prixBijou.textContent = `${data.price}€`;
        bijouElement.appendChild(prixBijou);
      const descriptionElement = document.createElement("button");
      descriptionElement.textContent = "Acheter";
      descriptionElement.addEventListener("click", function () {
        redirectToBijouPresentation(data.id);
      });
      bijouElement.appendChild(descriptionElement); // Ajoutez descriptionElement à bijouElement

      resultElement.appendChild(bijouElement); // Ajoutez bijouElement à resultElement
    } catch (error) {
      console.error("Erreur de requête:", error);
      // Peut-être ajouter une notification à l'utilisateur ici
    }
  }

  for (let i = 1; i <= 10; i++) {
    await fetchBijou(i); // Notez le "await" ici
  }
});

// Parcourir tous les bijoux et les ajouter à l'élément HTML
tousLesBijoux.forEach((bijoux) => {
  // Créer un élément pour chaque bijou
  const bijouElement = document.createElement("div");
  bijouElement.classList.add("produit");

  // Ajouter le contenu du bijou à l'élément
  c;

  // Ajouter l'élément du bijou à la liste des bijoux
  listeBijouxElement.appendChild(bijouElement);
});

// Fonction d'initialisation appelée au chargement de la page
function initialiserBijoux() {
  tousLesBijoux = document.querySelectorAll(".produit");
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
  bijouxAffiches.forEach((bijou) => {
    const classeBijou = `${bijou.id}`;
    // Créer un élément pour chaque bijou
    const bijouElement = document.createElement("div");
    bijouElement.classList.add("produit");

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
  window.location.href = "bijouxpresentation.html?bijouId=" + bijouId;
}
