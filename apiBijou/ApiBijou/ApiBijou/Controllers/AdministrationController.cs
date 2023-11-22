using API_SAE.Controllers;
using ApiBijou.Model.Bijoux;
using ApiBijou.Model.formModel;
using ApiBijou.Model.Panier;
using ApiBijou.Model.Utilisateurs;
using Microsoft.AspNetCore.Mvc;

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

        private PanierManager panierManager;

        public AdministrationController()
        {
            this.utilisateursManager = new UtilisateursManager();
            this.panierManager = new PanierManager();
        }

        /// <summary>
        /// Modifie un bijou
        /// </summary>
        /// <param name="formulaire"></param>
        /// <returns></returns>
        [HttpPost("ModifierBijou")]
        public IActionResult ModifierBijou([FromForm] FormulaireBijouModified formulaire)
        {
            ActionResult result = BadRequest("Erreur lors de la modification");
            try
            {
                bool bijModifier = BijouManager.Instance.ModifierBijou(formulaire);
                if (bijModifier)
                {
                    result = Ok();
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
        /// 
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
