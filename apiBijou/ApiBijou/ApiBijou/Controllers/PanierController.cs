using Microsoft.AspNetCore.Mvc;
using ApiBijou.Model;
using ApiBijou.Model.Services;
using ApiBijou.Model.Panier;
using System.Text.Json;
using Newtonsoft.Json;
using ApiBijou.Model.Bijoux;
using ApiBijou.Model.formModel;

namespace API_SAE.Controllers
{
    /// <summary>
    /// Controlleur de l'api Panier
    /// </summary>
    [ApiController]
    [Route("Panier")]
    public class PanierController : ControllerBase
    {
        private PanierManager panierManager;

        public PanierController()
        {
            panierManager = new PanierManager();
        }



        /// <summary>
        /// Ajoute un bijou du panier lié au token utilisateur
        /// </summary>
        /// <param name="bijou">bijou à ajouter au panier</param>
        /// <param name="token">token unique de l'utilisateur</param>
        /// <returns>réussite ou non de la requête</returns>
        [HttpPost("AjouterAuPanier")]
        public IActionResult AjouterAuPanier(string token, [FromBody] Bijou bijou)
        {
            IActionResult result = BadRequest("Erreur lors de l'ajout au panier.");
            try
            {
                if (BijouManager.Instance.GetBijouById(bijou.Id).Quantity > 0) // vérifie que le stock est suffisant pour ajouter le bijou au panier
                {
                    panierManager.AjouterBijouAuPanier(token, bijou);
                    result = Ok("Article ajouté au panier !");
                }
            }
            catch (Exception ex)
            {
                result = BadRequest(ex.Message);
            }

            return result;
 
        }

        /// <summary>
        /// Permet d'afficher le contenu du panier lié au token utilisateur
        /// </summary>
        /// <returns>panier utilisateur </returns>
        [HttpGet("ObtenirPanier")]
        public IActionResult ObtenirPanier(string token)
        {
            IActionResult result;
            result = BadRequest("Erreur lors de l'obtention du panier.");
            try
            {
                var panier = panierManager.ObtenirPanier(token);
                result = Ok(panier);
            }
            catch(Exception ex)
            {
                result = BadRequest(ex.Message);
            }
            return result;

        }


        /// <summary>
        /// Supprime un bijou du panier lié au token utilisateur
        /// </summary>
        /// <param name="bijou">bijou à supprimer du panier</param>
        /// <param name="token">token unique de l'utilisateur</param>
        /// <returns>réussite ou non de la requête</returns>
        [HttpDelete("SupprimerDuPanier")]
        public IActionResult SupprimerDuPanier(string token, int id)
        {
            IActionResult result;
            result = BadRequest("Erreur lors de la suppression du panier.");
            if (id != null)
            {
                panierManager.SupprimerBijouDuPanier(token, id);
                result = Ok("Article supprimé du panier !");
            }
            return result;
            
        }

        /// <summary>
        /// Créé un token temporaire utilisateur
        /// </summary>
        /// <returns>renvoi le token créé</returns>
        [HttpGet("CreerPanierToken")]
        public IActionResult CreerPanierToken()
        {
            IActionResult result = BadRequest("Erreur lors de la génération du panier");
            try
            {
                string t = panierManager.CreerPanierToken();
                result = Ok(t);
            }
            catch(Exception ex)
            {
                result = BadRequest(ex.Message);
            }

            return result;
        }

        /// <summary>
        /// Récupére le prix du panier
        /// </summary>
        /// <returns>renvoi le token créé</returns>
        [HttpGet("CoutTotalPanier")]
        public IActionResult CoutTotalPanier(string token)
        {
            IActionResult result = BadRequest("Erreur lors de la génération du panier");
            try
            {
                double ct = panierManager.CoutTotalPanier(token);
                result = Ok(ct);
            }
            catch(Exception ex)
            {
                result = BadRequest(ex.Message);
            }
            return result;
        }

    }
}
