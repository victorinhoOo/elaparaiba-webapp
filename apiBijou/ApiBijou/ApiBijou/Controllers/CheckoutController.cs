using ApiBijou.Model.Panier;
using ApiBijou.Model.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
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
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private PanierManager panierManager;

        public CheckoutController(IConfiguration configuration)
        {
            panierManager = new PanierManager();
            _configuration = configuration;
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
        }

        /// <summary>
        /// Permet la création d'une session Stripe Checkout pour effectuer le paiement
        /// </summary>
        /// <returns>Réussite du paiement ou non</returns>
        [HttpPost("CreateCheckoutSession")]
        public IActionResult CreateCheckoutSession(string token)
        {
            IActionResult result = null;
            var options = new SessionCreateOptions // initialise les options de la session stripe
            {
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                SuccessUrl = "https://localhost:7252/order/success?session_id={CHECKOUT_SESSION_ID}",
                CancelUrl = "https://localhost:7230/html/paiement_echoue.html",
                CustomerCreation = "always",
                ShippingOptions = new List<SessionShippingOptionOptions>
                {
                    new SessionShippingOptionOptions // Créé une méthode de livraison (collissimo)
                    {
                        ShippingRateData = new SessionShippingOptionShippingRateDataOptions
                        {
                            Type = "fixed_amount",
                            FixedAmount = new SessionShippingOptionShippingRateDataFixedAmountOptions
                            {
                                Amount = 495, // Colissimo livraison en centimes
                                Currency = "eur",
                            },
                            DisplayName = "Colissimo - Livraison à domicile entre 2-3 jours ouvrés",
                            
                            DeliveryEstimate = new SessionShippingOptionShippingRateDataDeliveryEstimateOptions
                            {
                                Minimum = new SessionShippingOptionShippingRateDataDeliveryEstimateMinimumOptions
                                {
                                    Unit = "business_day",
                                    Value = 2,
                                },
                                Maximum = new SessionShippingOptionShippingRateDataDeliveryEstimateMaximumOptions
                                {
                                    Unit = "business_day",
                                    Value = 3,
                                }
                            }
                        }
                    },

                    new SessionShippingOptionOptions // Créé une méthode de livraison (retrait en magasin)
                    {
                        ShippingRateData = new SessionShippingOptionShippingRateDataOptions
                        {
                            Type = "fixed_amount",
                            FixedAmount = new SessionShippingOptionShippingRateDataFixedAmountOptions
                            {
                                Amount = 0,
                                Currency = "eur",
                            },
                            DisplayName = "Retrait en magasin - Gratuit, 560 route du Bourg, 71500, SAINT USAGE, France",                          
                        }
                    },


                }
            };

            var panier = panierManager.ObtenirPanier(token); // récupère le panier de l'utilisateur
            foreach (var item in panier) // Pour chaque item du panier, l'ajoute dans un item stripe 
            {
                var sessionListItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.Bijou.Price * 100), // prix en centimes
                        Currency = "eur",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Bijou.Name,
                            Description = item.Bijou.Description,

                            Metadata = new Dictionary<string, string>
                            {
                                { "bijouId", item.Bijou.Id.ToString() } // Stocke l'ID du bijou dans les métadonnées
                            }
                        }
                    },
                    Quantity = item.Quantite,

                };
                options.LineItems.Add(sessionListItem); // ajoute l'item stripe aux options de la sessions
            }

            try
            {
                var service = new SessionService();
                Session session = service.Create(options); // créer la session avec les options sélectionnés

                /* Vérification du prix du panier, 
                if(session.LineItems.)*/

                result = Ok(session.Url);
            }
            catch (StripeException e)
            {
                result = BadRequest(new { error = e.StripeError.Message });
            }

            return result;
        }

    }
}
