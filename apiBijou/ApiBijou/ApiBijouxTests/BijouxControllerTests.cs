using API_SAE.Controllers;
using ApiBijou.Model.Bijoux;
using Microsoft.AspNetCore.Mvc;

namespace ApiBijouxTests
{
    /// <summary>
    /// Classe de tests unitaires pour le contrôleur de l'API Bijoux (BijouxController).
    /// </summary>
    public class BijouxControllerTests
    {
        private readonly BijouxController _controller;

        /// <summary>
        /// Initialise une nouvelle instance du contrôleur BijouxController pour chaque test.
        /// </summary>
        public BijouxControllerTests()
        {
            _controller = new BijouxController();
        }

        /// <summary>
        /// Vérifie que le contrôleur renvoie une réponse BadRequestObjectResult en cas de vérification d'un ID nul.
        /// </summary>
        [Fact]
        public void NullIdCheckTest()
        {
            var result = _controller.Check(null);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        /// <summary>
        /// Vérifie que le contrôleur renvoie une réponse NotFoundResult en cas de vérification d'un ID inexistant.
        /// </summary>
        [Fact]
        public void NonExistingIdCheckTest()
        {
            int nonExistingId = 999;

            var result = _controller.Check(nonExistingId);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        /// <summary>
        /// Vérifie que le contrôleur renvoie une réponse OkObjectResult lors de la récupération de tous les bijoux.
        /// </summary>
        [Fact]
        public void GetAllBijouxTest()
        {
            var okResult = _controller.GetAllBijoux();

            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        /// <summary>
        /// Vérifie que le contrôleur renvoie une réponse OkObjectResult contenant une collection de bijoux avec le bon nombre d'éléments.
        /// </summary>
        [Fact]
        public void GetAllBijouxItemCountTest()
        {
            var okResult = _controller.GetAllBijoux().Result as OkObjectResult;
            var items = Assert.IsAssignableFrom<ICollection<Bijou>>(okResult.Value);

            Assert.Equal(12, items.Count);
        }
    }
}
