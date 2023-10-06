using API_SAE.Controllers;
using API_SAE.Model;
using ApiBijou.Model.SurMesure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using Xunit;

namespace ApiBijouxTests
{

    /// <summary>
    /// Tests unitaires de l'API Bijoux
    /// </summary>
    public class BijouxControllerTests
    {
        private readonly BijouxController _controller;

        public BijouxControllerTests()
        {
            _controller = new BijouxController();
        }

        [Fact]
        public void NullIdCheckTest()
        {
            var result = _controller.Check(null);
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void NonExistingIdCheckTest()
        {
            int nonExistingId = 999;
            var result = _controller.Check(nonExistingId);
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetAllBijouxTest()
        {
            var okResult = _controller.GetAllBijoux();
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void GetAllBijouxItemCountTest()
        {
            var okResult = _controller.GetAllBijoux().Result as OkObjectResult;
            var items = Assert.IsAssignableFrom<ICollection<Bijou>>(okResult.Value);
            Assert.Equal(2, items.Count);
        }
    }

}
