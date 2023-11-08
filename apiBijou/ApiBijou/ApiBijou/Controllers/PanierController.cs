using Microsoft.AspNetCore.Mvc;
using ApiBijou.Model;
using ApiBijou.Model.Services;
using ApiBijou.Model.Panier;
using System.Text.Json;
using Newtonsoft.Json;
using ApiBijou.Model.Bijoux;

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
            if (bijou != null)
            {
                panierManager.AjouterBijouAuPanier(token, bijou);
                return Ok("Article ajouté au panier !");
            }
            return BadRequest("Erreur lors de l'ajout au panier.");
        }

        /// <summary>
        /// Permet d'afficher le contenu du panier lié au token utilisateur
        /// </summary>
        /// <returns>panier utilisateur </returns>
        [HttpGet("ObtenirPanier")]
        public IActionResult ObtenirPanier(string token)
        {
            var panier = panierManager.ObtenirPanier(token);
            return Ok(panier);
        }


        /// <summary>
        /// Supprime un bijou du panier lié au token utilisateur
        /// </summary>
        /// <param name="bijou">bijou à supprimer du panier</param>
        /// <param name="token">token unique de l'utilisateur</param>
        /// <returns>réussite ou non de la requête</returns>
        [HttpDelete("SupprimerDuPanier")]
        public IActionResult SupprimerDuPanier(string token, [FromBody] Bijou bijou)
        {
            if (bijou != null)
            {
                panierManager.SupprimerBijouDuPanier(token, bijou);
                return Ok("Article supprimé du panier !");
            }
            return BadRequest("Erreur lors de la suppression du panier.");
        }

        /// <summary>
        /// Créé un token temporaire utilisateur
        /// </summary>
        /// <returns>renvoi le token créé</returns>
        [HttpGet("CreerPanierToken")]
        public IActionResult CreerPanierToken()
        {
            IActionResult result = BadRequest("Erreur lors de la génération du panier");
            string t = panierManager.CreerPanierToken();
            result = Ok(t);
            return result;
        }



    }
}
