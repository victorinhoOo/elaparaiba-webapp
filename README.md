# Elaparaïba by Team Utopia

[🇬🇧 English](README.md) | [🇫🇷 Français](README.fr.md)

[![forthebadge](https://forthebadge.com/images/badges/built-with-love.svg)](https://forthebadge.com)  [![forthebadge](https://forthebadge.com/images/badges/for-you.svg)](https://forthebadge.com)

The **Elaparaïba** website is an online platform for purchasing handcrafted jewelry. It allows users to buy ready-made jewelry and order custom-made pieces.  

Users can also explore the brand's latest news and view new jewelry collections.  

Administrators have access to a management page where they can modify, add, or delete jewelry.  

---

## Built with

**Front-end**: HTML/CSS/JS  
**Back-end**: ASP.NET Core / MySQL  

---

## Screenshots
![Home Page](https://github.com/dept-info-iut-dijon/C2_Elaparaiba_SAE3/assets/116215966/c75a755a-b336-4e00-b63e-3690332ace28)  
![Jewelry Page](https://github.com/dept-info-iut-dijon/C2_Elaparaiba_SAE3/assets/116215966/57f2dda9-981b-4a95-a5d7-2cb13002149f)  
![Custom Orders](https://github.com/dept-info-iut-dijon/C2_Elaparaiba_SAE3/assets/116215966/16e7492e-bd1a-412a-bc62-12b1aa762891)  
![Craftsmanship](https://github.com/dept-info-iut-dijon/C2_Elaparaiba_SAE3/assets/116215966/887eef95-fa67-4d9b-a29d-52a6bdecd7b3)  
![Location](https://github.com/dept-info-iut-dijon/C2_Elaparaiba_SAE3/assets/116215966/bbaa648f-1f93-4bfc-94dc-6c79e84cabf9)  
![Admin Login](https://github.com/dept-info-iut-dijon/C2_Elaparaiba_SAE3/assets/115616225/6c038b63-667d-4ca3-b71c-9b1c80a11a28)  
![Admin Panel](https://github.com/dept-info-iut-dijon/C2_Elaparaiba_SAE3/assets/115616225/81066519-f76b-4990-8f0e-c26e7d301a03)  


---

## How to run the Elaparaïba application locally?

### **OPTION 1: Using a MySQL database**

1. Clone the GitHub repository into a specific folder.  

2. Create a MySQL database on your PC (MySQL Workbench is recommended) with the following credentials:  
   - Username: `root`  
   - Password: `rootroot`  

3. Execute this SQL script in the database: [SQL Script](https://pastebin.com/vcg35HAe)  

4. Run the API in Visual Studio:  
   `C2_Elaparaiba_SAE3\apiBijou\ApiBijou\ApiBijou.sln`  
   Run the front-end in Visual Studio:  
   `C2_Elaparaiba_SAE3\website\Elaparaiba.sln`  

5. If the API crashes, simply click "continue."  

---

### **OPTION 2: Using FakeDAO**

1. Clone the GitHub repository into a specific folder.  

2. In the API project, open:  
   `C2_Elaparaiba_SAE3\apiBijou\ApiBijou\ApiBijou.sln`.  

3. Replace the following lines of code:  
   - In `TokenManager.cs`, replace `new panierTokenDAO()` with `new panierTokenFakeDAO()` in the constructor.  
   - In `Utilisateurs/UtilisateurManager.cs`, replace `new UserDAO()` with `new UserFakeDAO()`.  
   - In `BijouManager.cs`, replace `new BijouDAO()` with `new BijouFakeDAO()` in the constructor.  

4. Run both the API and the front-end in Visual Studio.  

5. If the API crashes with the error "No cart associated with this token," you must manually delete the old cookie:  
   - **On Firefox**: Press `F12` → `Storage` → Right-click on the `PanierToken` cookie → Delete.  
   - **On Chrome**: Press `F12` → `Application` → `Cookies` → Right-click on the `PanierToken` cookie → Delete.  

Congratulations! You can now navigate the application and test its features!  

---

### **Administrator Login**  
To access the administration page, use the following credentials:  
URL: [https://localhost:7230/html/administration/gestion.html](https://localhost:7230/html/administration/gestion.html)  
- Username: `leaparaiba`  
- Password: `J1293zp30*`  

:warning: **Note**: Adding jewelry images will not work on the IUT network due to FTP restrictions.

---

## Presentation Video
[Feature Demonstration Video](https://drive.google.com/file/d/1nqg6WLO75eyOlFkWkcwtiT05NiZ6xTuO/view)  

---

## Authors

- **Matéo Bigeard** _alias_ [@Biigeard](https://github.com/Mbigeard06)  
- **Victor Duboz** _alias_ [@VictorinhoOo](https://github.com/victorinhoOo)  
- **Martin Simon** _alias_ [@Elven](https://github.com/ms292435)  
- **Ezai Comtois** _alias_ [@ezmaaan](https://github.com/tpiut212)  
