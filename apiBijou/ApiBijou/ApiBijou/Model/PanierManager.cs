using ApiBijou.Model;

namespace API_SAE.Model
{
    public class PanierManager
    {
        private readonly Panier _panier;

        public PanierManager(Panier panier)
        {
            _panier = panier;
        }

        // Méthode pour ajouter un bijou au panier
        public void AjouterBijouAuPanier(Bijou bijou)
        {
            _panier.AddBijoux(bijou);
        }

        // Méthode pour obtenir le panier
        public List<PannierItem> ObtenirPanier()
        {
            return _panier.GetPanier();
        }

        // Méthode pour supprimer un bijou du panier
        public void SupprimerBijouDuPanier(Bijou bijou)
        {
            _panier.DelBijoux(bijou);
        }
    }
}
