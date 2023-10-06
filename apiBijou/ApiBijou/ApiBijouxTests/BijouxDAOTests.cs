using API_SAE.Controllers;
using API_SAE.Data;
using API_SAE.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Xunit;

namespace ApiBijouxTests
{
    /// <summary>
    /// Tests unitaires du DAO Bijoux
    /// </summary>
    public class BijouDAOTests
    {
        private readonly BijouDAO _dao;

        public BijouDAOTests()
        {
            _dao = new BijouDAO();
        }

        [Fact]
        public void CanRetrieveBijouById()
        {
            int validId = 1;
            Bijou bijou = _dao.getById(validId);
            Assert.NotNull(bijou);
            Assert.Equal(validId, bijou.Id);
        }

        [Fact]
        public void ReturnsNullForUnknownId()
        {
            int invalidId = 999;
            Bijou bijou = _dao.getById(invalidId);
            Assert.Null(bijou);
        }

        [Fact]
        public void CanRetrieveAllBijoux()
        {
            Bijou bijoux = (Bijou)_dao.GetAllBijoux();
            Assert.NotNull(bijoux);
            Assert.IsAssignableFrom<IEnumerable<Bijou>>(bijoux);
        }
    }
}
