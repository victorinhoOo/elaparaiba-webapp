//Demande à l'API les droits d'un utilisateur
async function IsAdmin(tokenPanier){
    let isAdmin = false;
    const apiUrl = `https://localhost:7252/Administration/isadmin?tokenPanier=${tokenPanier}`;
    try{
        let response = await fetch(apiUrl); 
        if (response.ok){
            isAdmin = true;
        }
        else{
            console.error("Vous n'etes pas admin")
        }
    } catch (error){
        console.error(error);
    }
    return isAdmin;
}

async function ConnectAsAdmin(formData){
    let connexionSucess = false;
    try {
        const response = await fetch('https://localhost:7252/Administration/login', {
            method: 'POST',
            body: formData // Envoyer le formulaire avec le fichier
        });
    if (response.ok){
        connexionSucess = true;
    }
    else{
        console.error("Vous n'avez pas les droits.")
    }
    } catch (error){
        console.error('Erreur : Communication impossible avec le serveur',);
    }
    return connexionSucess;
}


export {IsAdmin, ConnectAsAdmin};