using ApiBijou.Data;

namespace PanierTokenTest
{
    public class PanierTokenFakeDaoTest
    {
        [Fact]
        public void getPanierIdTest()
        {
            Assert.Equal(PanierTokenFakeDAO.Instance.GetPanierId("b70a8c45cb4366a02939b68e24ed126"),4);
            Assert.Equal(PanierTokenFakeDAO.Instance.GetPanierId("b70a8c45cb4366a02939?68e64ed126"), -1);
        }

        [Fact]
        public void CreerPanierTokenTest()
        {
            string id = PanierTokenFakeDAO.Instance.CreerPanierToken();
            Assert.Equal(PanierTokenFakeDAO.Instance.GetPanierId(id), 8);
        }
    }
}