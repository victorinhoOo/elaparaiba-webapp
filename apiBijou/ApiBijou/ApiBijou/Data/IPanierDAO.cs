using API_SAE.Model;
using ApiBijou.Model.Panier;

namespace ApiBijou.Data
{
    public interface IPanierDAO
    {
        /// <summary>
        /// Ajoute bijou au panier
        /// </summary>
        /// <param name="idPanier">id du panier</param>
        /// <param name="bijou">bijou à ajouter</param>
        public void AjouterBijouAuPanier(int idPanier, Bijou bijou);
        /// <summary>
        /// Supprimer bijou du panier
        /// </summary>
        /// <param name="idPanier">id du panier</param>
        /// <param name="bijou">bijou à supprimer</param>
        public void SupprimerBijouDuPanier(int idPanier, Bijou bijou);
        /// <summary>
        /// Renvoi le contenu du panier
        /// </summary>
        /// <param name="idPanier">id du panier</param>
        /// <returns>List de PanierItem</returns>
        public List<PanierItem> ObtenirPanier(int idPanier);
        /// <summary>
        /// Créer un panier
        /// </summary>
        /// <param name="idPanier">Id du panier a créer</param>
        public void CreerPanier(int idPanier);
    }
}
