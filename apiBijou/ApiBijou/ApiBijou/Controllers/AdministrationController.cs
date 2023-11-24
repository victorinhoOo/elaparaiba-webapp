using API_SAE.Controllers;
using ApiBijou.Model.Bijoux;
using ApiBijou.Model.formModel;
using ApiBijou.Model.Panier;
using ApiBijou.Model.Utilisateurs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace ApiBijou.Controllers
{
    /// <summary>
    /// Controller de la page administration
    /// </summary>
    [ApiController]
    [Route("Administration")]
    public class AdministrationController : ControllerBase
    {
        private UtilisateursManager utilisateursManager;

        public AdministrationController()
        {
            this.utilisateursManager = new UtilisateursManager();
        }

        /// <summary>
        /// Modifie ou crée un nouveau bijou
        /// </summary>
        /// <param name="formulaire">attributs du nouveau bijou</param>
        /// <returns></returns>
        [HttpPost("ModifierBijou")]
        public IActionResult ModifierBijou([FromForm] FormulaireBijouModified formulaire)
        {
            ActionResult result = BadRequest("Erreur lors de la modification");
            bool res = false;
            try
            {
                if (utilisateursManager.IsAdmin(formulaire.UserToken)) //L'utilisateur est admin
                {
                    if(formulaire.IdBijou == -1)//Création d'un nouveau bijou
                    {
                        res = BijouManager.Instance.AjouterBijou(formulaire);
                    }
                    else //Modification d'un bijou existant
                    {
                        res = BijouManager.Instance.ModifierBijou(formulaire);
                    }
                    if (res) //Bijou crée ou modifié avec succès
                    {
                        result = Ok("Bijou modifié");
                        //Modification

                    }
                }
                else //L'utilisateur n'est pas admin
                {
                    result = Unauthorized(new { Message = "L'utilisateur n'est pas un administrateur" });
                }
            }
            catch (Exception ex)
            {
                result = BadRequest(ex);
            }
            return result;
        }

        /// <summary>
        /// Suppression d'un bijou.
        /// </summary>
        /// <param name="modeleSupprimerBijou">Mode de données transmis par le client</param>
        /// <returns></returns>
        [HttpPost("SupprimerBijou")]
        public IActionResult SupprimerBijou([FromBody] ModeleSupprimerBijou modeleSupprimerBijou)
        {
            IActionResult result = Unauthorized(new { Message = "L'utilisateur n'est pas un administrateur" });
            try
            {
                if (utilisateursManager.IsAdmin(modeleSupprimerBijou.TokenPanier)) //Vérification des droits de l'utilisateur
                {
                    bool suppression = BijouManager.Instance.DeleteBijouById(Convert.ToInt32(modeleSupprimerBijou.IdBijou));
                    if (suppression)
                    {
                        result = Ok("Bijou supprimé");
                    }
                    else
                    {
                        result = BadRequest("Erreur lors de la suppression");
                    }
                }
            }
            catch (Exception ex)
            {
                result = BadRequest(ex);
            }
            return result;        
        }

        /// <summary>
        /// Gère la connexion de l'administrateur
        /// </summary>
        /// <param name="login">nom d'utilisateur</param>
        /// <param name="password">mot de passe</param>
        /// <returns>Ok si la connexion a réussi</returns>
        [HttpPost("login")]
        public IActionResult Login([FromForm] Utilisateur utilisateur)
        {
            IActionResult result = Unauthorized(new { Message = "L'utilisateur n'est pas un administrateur" });
            if (utilisateursManager.ConnectAsAdmin(utilisateur.TokenPanier, utilisateur.Login, utilisateur.Mdp))
            {
                result = Ok(new { Message = "L'utilisateur est connecté en tant qu'administrateur" });
            }
            return result;
        }

        /// <summary>
        /// Vérifie si un utilisateur est admin.
        /// </summary>
        /// <param name="tokenPanier"></param>
        /// <returns></returns>
        [HttpGet("isAdmin")]
        public IActionResult IsAdmin(string tokenPanier)
        {
            IActionResult result = Unauthorized(new { Message = "L'utilisateur n'est pas un administrateur" });
            bool isAdmin = utilisateursManager.IsAdmin(tokenPanier);
            if (isAdmin)
            {
                result = Ok(new { Message = "L'utilisateur est un administrateur" });
            }
            return result;
        }

        
    }
}
