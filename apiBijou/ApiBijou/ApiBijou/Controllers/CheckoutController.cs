using ApiBijou.Model.Panier;
using ApiBijou.Model.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;
using Stripe.Issuing;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiBijou.Controllers
{
    /// <summary>
    /// Controlleur pour le paiement via Stripe
    /// </summary>
    public class CheckoutController : ControllerBase
    {
        private readonly StripeSettings _stripeSettings;

        private PanierManager panierManager;

        private string token;

        public CheckoutController(IOptions<StripeSettings> stripeSettings, string token)
        {
            this.token = token;
            panierManager = new PanierManager();
            _stripeSettings = stripeSettings.Value;
            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;
        }

        /// <summary>
        /// Permet la création d'une session Stripe Checkout pour effectuer le paiement
        /// </summary>
        /// <returns>Réussite du paiement ou non</returns>
        [HttpPost("CreateCheckoutSession")]
        public async Task<IActionResult> CreateCheckoutSessionAsync()
        {
            var panier = panierManager.ObtenirPanier(token); // récupère le panier de l'utilisateur
            var lineItems = new List<SessionLineItemOptions>();
            foreach (var item in panier) // Pour chaque item du panier, l'ajoute dans un item stripe
            {
                lineItems.Add(new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.Bijou.Price * 100), // convertit le prix en centimes
                        Currency = "eur",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Bijou.Name,
                            Description = item.Bijou.Description,
                        },
                    },
                    Quantity = item.Quantite,
                });
            }
            var options = new SessionCreateOptions // modifie les options de la session stripe
            {
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = "https://votre_site.com/success",
                CancelUrl = "https://votre_site.com/cancel",
            };


            try
            {
                var service = new SessionService();
                Session session = await service.CreateAsync(options);

                Response.Headers.Add("Location", session.Url);
                return new StatusCodeResult(303);
            }
            catch (StripeException e)
            {
                return BadRequest(new { error = e.StripeError.Message });
            }
        }
    }
}
