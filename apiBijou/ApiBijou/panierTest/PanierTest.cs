//using API_SAE.Data;
//using API_SAE.Model;
//using ApiBijou.Model;

//namespace panierTest
//{
//    public class PanierTest
//    {
//        [Fact]
//        public void ajouterBijou()
//        {
//            Panier panier = new Panier();
//            panier.addBijoux(BijouFakeDAO.Instance.getById(1));
//            Assert.Equal<int>(0, panier.contientBijou(BijouFakeDAO.Instance.getById(1)));
//        }


//        /// <summary>
//        /// Test sur la quantité du bijou dans la liste après l'ajout du même bijou
//        /// </summary>
//        [Fact]
//        public void ajouterMemeBijou()
//        {
//            Panier panier = new Panier();
//            panier.addBijoux(BijouFakeDAO.Instance.getById(1));
//            panier.addBijoux(BijouFakeDAO.Instance.getById(1));
//            Assert.Equal<int>(2, panier.Bijoux[0].Quantite);
//        }

//        [Fact]
//        public void contenirBijou()
//        {
//            Panier panier = new Panier();
//            Assert.Equal<int>(-1, panier.contientBijou(BijouFakeDAO.Instance.getById(1)));
//            panier.addBijoux(BijouFakeDAO.Instance.getById(1));
//            panier.addBijoux(BijouFakeDAO.Instance.getById(2));
//            panier.addBijoux(BijouFakeDAO.Instance.getById(3));
//            Assert.Equal<int>(2,panier.contientBijou(BijouFakeDAO.Instance.getById(3)));

//        }
//    }
//}