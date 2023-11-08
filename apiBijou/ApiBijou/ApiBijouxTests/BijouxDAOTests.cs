using API_SAE.Controllers;
using ApiBijou.Data.Bijoux;
using ApiBijou.Model.Bijoux;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Xunit;

namespace ApiBijouxTests
{
    /// <summary>
    /// Classe de tests unitaires pour le DAO Bijoux (BijouDAO).
    /// </summary>
    public class BijouDAOTests
    {
        private readonly BijouDAO _dao;

        /// <summary>
        /// Initialise une nouvelle instance de BijouDAO pour chaque test.
        /// </summary>
        public BijouDAOTests()
        {
            _dao = new BijouDAO();
        }

        /// <summary>
        /// Vérifie la capacité du DAO à récupérer un bijou par son ID.
        /// </summary>
        [Fact]
        public void CanRetrieveBijouById()
        {
            int validId = 1;

            Bijou bijou = _dao.getById(validId);

            Assert.NotNull(bijou);
            Assert.Equal(validId, bijou.Id);
        }

        /// <summary>
        /// Vérifie que le DAO renvoie null lorsqu'un ID inconnu est passé à la méthode getById.
        /// </summary>
        [Fact]
        public void ReturnsNullForUnknownId()
        {
            int invalidId = 999;

            Bijou bijou = _dao.getById(invalidId);

            Assert.Null(bijou);
        }

        /// <summary>
        /// Vérifie la capacité du DAO à récupérer tous les bijoux.
        /// </summary>
        [Fact]
        public void CanRetrieveAllBijoux()
        {
            IEnumerable<Bijou> bijoux = _dao.GetAllBijoux();

            Assert.NotNull(bijoux);
            Assert.IsAssignableFrom<IEnumerable<Bijou>>(bijoux);
        }
    }
}
