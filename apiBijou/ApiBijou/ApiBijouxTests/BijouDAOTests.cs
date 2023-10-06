using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiBijouxTests
{
    using System.Collections.Generic;
    using Xunit;
    using MySql.Data.MySqlClient;
    using API_SAE.Model;
    using API_SAE.Data;

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
}
