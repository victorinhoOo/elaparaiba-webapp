using Xunit;
using Moq;
using Stripe;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using Stripe.BillingPortal;
using ApiBijou.Model.Panier;
using API_SAE.Controllers;
using ApiBijou.Data.Bijoux;
using ApiBijou.Model.Bijoux;
using ApiBijou.Controllers;
using Microsoft.Extensions.Configuration;
using ApiBijou.Data.Paniers.Tokens;

namespace CheckoutControllerTests
{
    /// <summary>
    /// Tests unitaires du paiement 
    /// </summary>
    public class TestsPaiement
    {
        /// <summary>
        /// Créé une session stripe liée à un panier et vérifie qu'un url est renvoyé
        /// </summary>
        [Fact]
        public void CreationSession_RetourneUrl()
        {
            // Préparation
            var panierManager = new PanierManager(); 
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(c => c["Stripe:SecretKey"]).Returns("sk_test_51O3zmTLbt6jKzUZ5fpLPPq8gH9PwnKsgXWqwJselQBkZ2N8oXm5AuJpXsI9hgvshwSpJ9trlubsu1QxF0jDSQ1Pn00wbIKK8j6");
            var controller = new CheckoutController(configurationMock.Object);

            // Ajoute un article à un panier du fakeDAO
            string t = panierManager.CreerPanierToken();

            panierManager.AjouterBijouAuPanier(t, BijouFakeDAO.Instance.getById(1));

            // Créé la session correspondante à ce panier
            var resultat = controller.CreateCheckoutSession(t);


            // Vérifie qu'un url est renvoyé
            var okResultat = Assert.IsType<OkObjectResult>(resultat);
            Assert.NotNull(okResultat.Value);
            Assert.IsType<string>(okResultat.Value);
        }

    }
}
