using Microsoft.AspNetCore.Mvc;
using ApiBijou.Model;
using ApiBijou.Model.Services;
using API_SAE.Model;

namespace API_SAE.Controllers
{
    [ApiController]
    [Route("Panier")]
    public class PanierController : ControllerBase
    {
        private readonly PanierManager panierManager;

        public PanierController(PanierManager panierManager)
        {
            this.panierManager = panierManager;
        }

        [HttpPost("AjouterAuPanier")]
        public IActionResult AjouterAuPanier([FromBody] Bijou bijou)
        {
            if (bijou != null)
            {
                panierManager.AjouterBijouAuPanier(bijou);
                return Ok("Article ajouté au panier !");
            }
            return BadRequest("Erreur lors de l'ajout au panier.");
        }

        [HttpGet("ObtenirPanier")]
        public IActionResult ObtenirPanier()
        {
            var panier = panierManager.ObtenirPanier();
            return Ok(panier);
        }

        [HttpDelete("SupprimerDuPanier")]
        public IActionResult SupprimerDuPanier([FromBody] Bijou bijou)
        {
            if (bijou != null)
            {
                panierManager.SupprimerBijouDuPanier(bijou);
                return Ok("Article supprimé du panier !");
            }
            return BadRequest("Erreur lors de la suppression du panier.");
        }


    }
}
