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
        public IActionResult AjouterAuPanier()
        {
            IActionResult result = BadRequest("Problème lors de l'ajout");
            string token = Request.Headers["token"];
            var id = Request.Headers["id"];
            Bijou bijou = BijouManager.Instance.GetBijouById(Convert.ToInt32(id));  
            if (bijou != null && token != null)
            {
                panierManager.AjouterBijouAuPanier(token, bijou);
                result = Ok("Article ajouté au panier !");
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
        public IActionResult SupprimerDuPanier(string token, int id)
        {
            if (id != null)
            {
                panierManager.SupprimerBijouDuPanier(token, id);
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

        /// <summary>
        /// Récupére le prix du panier
        /// </summary>
        /// <returns>renvoi le token créé</returns>
        [HttpGet("CoutTotalPanier")]
        public IActionResult CoutTotalPanier(string token)
        {
            IActionResult result = BadRequest("Erreur lors de la génération du panier");
            double ct = panierManager.CoutTotalPanier(token);
            result = Ok(ct);
            return result;
        }

    }
}
