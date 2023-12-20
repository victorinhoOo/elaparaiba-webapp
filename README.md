# Elaparaïba by Team Utopia

[![forthebadge](https://forthebadge.com/images/badges/built-with-love.svg)](https://forthebadge.com)
[![forthebadge](https://forthebadge.com/images/badges/for-you.svg)](https://forthebadge.com)


Le site web Elaparaïba est une plateforme de vente de bijoux artisanaux en ligne. Elle permet l'achat de bijoux ainsi que la commande de bijoux sur mesure.  

Il est possible de consulter l'actualité de la marque et de visualiser les nouveaux bijoux.  

L'administrateur peut également modifier les bijoux, en ajouter de nouveaux et les supprimer depuis une page d'administration.  


## Fabriqué avec

**Front-end** : HTML/CSS/JS

**Back-end** : ASP.NET Core 


## Comment faire tourner l'application Elaparaïba en local ?  

  

**OPTION 1 : Avec une base de données MYSQL**

1) Cloner le github dans un dossier spécifique.
   
2) Créer une base de données mysql sur votre PC (MySQL Workbench est recommandé) avec les identifiants suivants :  
   nom d'utilisateur : root  
   mot de passe : rootroot  

3) Exécuter ce script SQL dans la base de données : https://pastebin.com/vcg35HAe  
   
4) Exécuter l'API sur Visual Studio : C2_Elaparaiba_SAE3\apiBijou\ApiBijou\ApiBijou.sln  
   Exécuter le front-end sur Visual Studio  : C2_Elaparaiba_SAE3\website\Elaparaiba.sln  
   
5) Si l'API plante, cliquez simplement sur "continuer".  

  

**OPTION 2 : Avec les FAKEDAO**

1) Cloner le github dans un dossier spécifique  

2) Dans l'API sur Visual Studio : C2_Elaparaiba_SAE3\apiBijou\ApiBijou\ApiBijou.sln remplacer les lignes suivantes :  
   
   - Dans TokenManager.cs : remplacer le new panierTokenDAO() par new panierTokenFakeDAO() dans le constructeur.  
   - Dans Utilisateurs/UtilisateurManager.cs : remplacer le new UserDAO()  par new UserFakeDAO()  
   - Dans BijouManager.cs : remplacer le new BijouDAO() par new BijouFakeDAO() dans le constructeur  

4) Exécutez l'API et le front-end sur Visual Studio  

5) Si l'API plante avec l'exception "Aucun panier associé avec ce token" il faut supprimer à la main l'ancien cookie :  
   
   SUR FIREFOX : Appuyer sur F12 --> stockage --> clic droit sur le cookie PanierToken --> supprimer  
   SUR CHROME : Appuyer sur F12 --> application --> cookies --> clic droit sur le cookie PanierToken --> supprimer  

Bravo ! vous pouvez désormais naviguer dans l'application et tester les différentes fonctionnalités !  

  

**Pour se connecter à la page d'administration :**  
  
https://localhost:7230/html/administration/gestion.html  
Nom d'utilisateur : leaparaiba  
Mot de passe : J1293zp30*  

  
:warning: Si vous tentez d'ajouter un bijou depuis le réseau de l'IUT, cela ne fonctionnera pas car le transfert d'images par FTP est bloqué par le réseau de l'IUT  

  

## Captures d'écrans
![accueil 1](https://github.com/dept-info-iut-dijon/C2_Elaparaiba_SAE3/assets/116215966/c75a755a-b336-4e00-b63e-3690332ace28)

![Les_bijoux](https://github.com/dept-info-iut-dijon/C2_Elaparaiba_SAE3/assets/116215966/57f2dda9-981b-4a95-a5d7-2cb13002149f)

![sue mesure](https://github.com/dept-info-iut-dijon/C2_Elaparaiba_SAE3/assets/116215966/16e7492e-bd1a-412a-bc62-12b1aa762891)

![Savoir-faire](https://github.com/dept-info-iut-dijon/C2_Elaparaiba_SAE3/assets/116215966/887eef95-fa67-4d9b-a29d-52a6bdecd7b3)

![ou me trouver](https://github.com/dept-info-iut-dijon/C2_Elaparaiba_SAE3/assets/116215966/bbaa648f-1f93-4bfc-94dc-6c79e84cabf9)

![adminLogin](https://github.com/dept-info-iut-dijon/C2_Elaparaiba_SAE3/assets/115616225/6c038b63-667d-4ca3-b71c-9b1c80a11a28)

![admin](https://github.com/dept-info-iut-dijon/C2_Elaparaiba_SAE3/assets/115616225/81066519-f76b-4990-8f0e-c26e7d301a03)


  

## Auteurs

* **Matéo Bigeard** _alias_ [@Biigeard](https://github.com/Mbigeard06)
* **Victor Duboz** _alias_ [@VictorinhoOo](https://github.com/victorinhoOo)
* **Martin Simon** _alias_ [@Elven](https://github.com/ms292435)
* **Ezai Comtois** _alias_ [@ezmaaan](https://github.com/tpiut212)


