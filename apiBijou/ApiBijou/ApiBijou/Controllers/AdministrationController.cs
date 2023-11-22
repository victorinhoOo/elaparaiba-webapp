using API_SAE.Controllers;
using ApiBijou.Model.Bijoux;
using ApiBijou.Model.formModel;
using ApiBijou.Model.Panier;
using ApiBijou.Model.Utilisateurs;
using Microsoft.AspNetCore.Mvc;

namespace ApiBijou.Controllers
{
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
        public IActionResult Login(string login, string password)
        {

            string tokenPanier  = panierManager.CreerPanierToken();

            bool isAdmin = utilisateursManager.ConnectAsAdmin(tokenPanier, login, password);
            if (isAdmin)
            {
                return Ok(new { Message = "Connexion réussie", Token = tokenPanier });
            }
            else
            {
                return Unauthorized(new { Message = "Échec de l'authentification" });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tokenPanier"></param>
        /// <returns></returns>
        [HttpGet("isadmin")]
        public IActionResult IsAdmin(string tokenPanier)
        {
            bool isAdmin = utilisateursManager.IsAdmin(tokenPanier);
            if (isAdmin)
            {
                return Ok(new { Message = "L'utilisateur est un administrateur" });
            }
            else
            {
                return Unauthorized(new { Message = "L'utilisateur n'est pas un administrateur" });
            }
        }


    }
}
