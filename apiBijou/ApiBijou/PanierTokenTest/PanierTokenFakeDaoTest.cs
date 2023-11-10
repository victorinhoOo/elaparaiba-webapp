using ApiBijou.Data.Paniers.Tokens;

namespace PanierTokenTest
{
    public class PanierTokenFakeDaoTest
    {
        /// <summary>
        /// Vérifie l'ID de certains tokens du fakeDAO
        /// </summary>
        [Fact]
        public void getPanierIdTest()
        {
            Assert.Equal(PanierTokenFakeDAO.Instance.GetPanierId("b70a8c45cb4366a02939b68e24ed126"),4);
        }

        /// <summary>
        /// Creer un token dans le fakeDAO et vérifie qu'il a le bon id
        /// </summary>
        [Fact]
        public void CreerPanierTokenTest()
        {
            string id = PanierTokenFakeDAO.Instance.CreerPanierToken();
            Assert.Equal(PanierTokenFakeDAO.Instance.GetPanierId(id), 8);
        }
    }
}