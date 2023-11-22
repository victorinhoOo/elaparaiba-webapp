using API_SAE.Controllers;
using ApiBijou.Model.Bijoux;
using ApiBijou.Model.Panier;
using ApiBijou.Model.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;
using Stripe.Issuing;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ApiBijou.Controllers
{
    /// <summary>
    /// Controlleur pour le paiement via Stripe
    /// </summary>
    [Route("webhook")]
    [ApiController]
    public class WebhookController : Controller
    {

        private readonly IConfiguration _configuration;

        public WebhookController(IConfiguration configuration)
        {
            _configuration = configuration;
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
        }


        // This is your Stripe CLI webhook secret for testing your endpoint locally.
        const string endpointSecret = "whsec_22ec48c2f970e18178e697af2cc612b2f13dec95b5c96f2396e0ad0a586bc01b";

        /// <summary>
        /// Gestionnaire d'évenements stripe
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Index()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            try
            {
                var stripeEvent = EventUtility.ConstructEvent(
                    json,
                    Request.Headers["Stripe-Signature"],
                    endpointSecret
                );

                if (stripeEvent.Type == Events.CheckoutSessionCompleted)
                {
                    var session = stripeEvent.Data.Object as Stripe.Checkout.Session;
                    var options = new SessionGetOptions
                    {
                        Expand = new List<string> { "line_items.data.price.product" } 
                    };

                    var service = new SessionService();
                    Session sessionWithLineItems = service.Get(session.Id, options);
                    StripeList<LineItem> lineItems = sessionWithLineItems.LineItems;

                    FulfillOrder(lineItems);
                }

                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest();
            }
        }


        /// <summary>
        /// Met à jour la base de données lorsqu'une commande est validée par Stripe en diminuant le stock des bijoux achetés en fonction de la quantité achetée
        /// </summary>
        /// <param name="lineItems">Bijoux achetés</param>
        private void FulfillOrder(StripeList<LineItem> lineItems)
        {
            foreach (var lineItem in lineItems.Data)
            {
                var metadata = lineItem.Price.Product.Metadata; // Assuming metadata is attached to the product

                if (metadata != null && metadata.TryGetValue("bijouId", out var bijouIdStr) && int.TryParse(bijouIdStr, out int bijouId))
                {
                    int quantite = Convert.ToInt32(lineItem.Quantity);
                    bool success = BijouManager.Instance.DecreaseStockById(bijouId, quantite);
                    if (!success)
                    {
                        throw new InvalidOperationException($"Échec de la diminution du stock pour le bijou ID {bijouId}");
                    }
                }
                else
                {
                    throw new InvalidOperationException("ID de bijou invalide ou manquant dans les métadonnées Stripe.");
                }
            }
        }





        /// <summary>
        /// Affiche un récapitulatif de commande en récupérant les informations de la session Stripe
        /// </summary>
        /// <param name="session_id">id de la session stripe</param>
        /// <returns>Page de récapitulatif</returns>
        [HttpGet("/order/success")]
        public ActionResult OrderSuccess([FromQuery] string session_id)
        {

            var sessionService = new SessionService();
            Session session;
            try
            {
                session = sessionService.Get(session_id, new SessionGetOptions { Expand = new List<string> { "line_items" } });
            }
            catch (StripeException e)
            {
                return BadRequest("Unable to retrieve session from Stripe. " + e.Message);
            }

            if (session == null || string.IsNullOrEmpty(session.CustomerDetails?.Name))
            {
                return BadRequest("Invalid session or customer details.");
            }

            var htmlContent = new StringBuilder(@"
                <html>
                <head>
                    <meta charset='UTF-8'>
                    <style>
                        body {
                            display: unset;
                            font-family: Arial;
                        }
                        .order-summary {
                            width: 1000px;
                            background-color: #f9f9f9;
                            padding: 20px;
                            border: 1px solid #e1e1e1;
                            border-radius: 5px;
                            margin: auto;
                            display: flex;
                            flex-direction: column;
                        }
                        .order-summary h1 {
                            color: #03889F;
                            border-bottom: 2px solid #03889F;
                            padding-bottom: 10px;
                            font-family: 'Caveat';
                            font-size: 35px;
                            text-align: center;
                        }
                        .item {
                            display: grid;
                            margin-bottom: 10px;
                            grid-template-columns: 60% 20% 20% auto auto;
                            align-items: center;
                        }
                        .item img {
                            width: 150px;
                            margin-right: 10px;
                            height: 200px;
                            object-fit: cover;
                        }
                        .item-name {
                            color: #333;
                            flex-grow: 1;
                        }
                        .item-quantity {
                            color: black;
                            font-weight: bold;
                        }
                        .item-price {
                            color: #000;
                            font-weight: bold;
                        }

                        h3 {
                            margin-bottom: 50px;
                            text-align: center;
                            margin-top: 20px;
                        }

                        .continuer-btn {
                            margin: auto;
                            text-align: center;
                            background-color: #03889F;
                            color: #fff;
                            padding: 10px 20px;
                            border: 1px solid #fff;
                            border-radius: 5px;
                            text-decoration: none;
                            margin-top: 30px; 
                            width: 140px; 
                            display: flex;
                            justify-content: center;
                            align-items: center;
                        }

                        .continuer-btn:hover {
                            cursor: pointer;
                            background-color: white;
                            color: #03889F;
                            text-decoration: none; /* Enlève le soulignement au survol */
                            border: 1px solid #03889F;
                        }
                        .total{
                            text-align: right;
                            margin-top: 20px;
                            font-size: 13px;
                        }
                    </style>
                </head>
                <body>
                    <div class='order-summary'>
                        <h1>Merci pour votre commande " + session.CustomerDetails.Name + @" !</h1>");

            htmlContent.Append("<h3>Un récapitulatif vous a été envoyé à l'adresse mail suivante : " + session.CustomerDetails.Email + @"</h3>");

            double? total = 0;


            if (session.LineItems != null && session.LineItems.Data != null) // On affiche les différents bijoux
            {
                foreach (var item in session.LineItems.Data)
                {
                    double? price = item.Price.UnitAmount / 100.0;
                    double? itemTotal = price * item.Quantity;
                    total += itemTotal; // Pour l'affichage du total
                    htmlContent.Append(@"
            <div class='item'>
                <p class='item-name'>" + item.Description + @"</p>
                <p class='item-quantity'>Quantité: " + item.Quantity + @"</p>
                <p class='item-price'>Prix: " + (item.Price.UnitAmount / 100.0) + " " + item.Price.Currency.ToUpper() + @"</p>
            </div>");
                }
            }
            else
            {
                htmlContent.Append("<p>Impossible de récupérer les informations de la commande.</p>");
            }

            htmlContent.Append(@"
            <div class='total'>
                <h2>Total : " + total + @" " + session.Currency.ToUpper() + @"</h2>
            </div></div>");
            htmlContent.Append("<a href='https://localhost:7230' class=\"continuer-btn\">Revenir à l'accueil</a>");

            htmlContent.Append("</body></html>");
            return Content(htmlContent.ToString(), "text/html; charset=utf-8");
        }

    }
}