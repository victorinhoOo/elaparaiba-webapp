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


//Envoi le bijou modifé au serveur
async function sendBijouModified(formData) {
    //Communication avec l'api
    let success = false;
    try {
        const response = await fetch('https://localhost:7252/Administration/ModifierBijou', {
            method: 'POST',
            body: formData,
        });
        if (response.ok) {//Modification réussi
            console.log('Réponse réussie :', response);
            success = true;
        }
    } catch (error) { //Erreur de commmunication avec le serveur
        console.error('Erreur : Communication impossible avec le serveur', error);
    } 
    return success;
}

async function delBijou(tokenPanier, idBijou){
    const requestBody = {
        TokenPanier: tokenPanier,
        IdBijou: idBijou
    };
    //Communication avec l'api
    let success = false;
    try {
        const response = await fetch('https://localhost:7252/Administration/SupprimerBijou', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(requestBody)
        });
        if (response.ok) {//Modification réussi
            console.log('Réponse réussie :', response);
            success = true;
        }
    } catch (error) { //Erreur de commmunication avec le serveur
        console.error('Erreur : Communication impossible avec le serveur', error);
    } 
    return success;

}

export { IsAdmin, ConnectAsAdmin, sendBijouModified, delBijou };