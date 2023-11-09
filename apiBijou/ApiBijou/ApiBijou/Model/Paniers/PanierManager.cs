using ApiBijou.Data;
using ApiBijou.Data.Paniers;
using ApiBijou.Model.Bijoux;
using ApiBijou.Model.Paniers;
using System.Runtime.CompilerServices;

namespace ApiBijou.Model.Panier
{
    public class PanierManager
    {
        private TokenManager tokenManager = new TokenManager();
        private IPanierDAO panierDAO = new PanierDAO();
        /// <summary>
        /// Ajouter un bijou au panier
        /// </summary>
        /// <param name="bijou">Bijou à ajouter</param>
        public void AjouterBijouAuPanier(string token, Bijou bijou)
        {
            panierDAO.AjouterBijouAuPanier(tokenManager.GetPanierId(token), bijou);
        }

        /// <summary>
        /// Renvoi la liste des bijoux du panier
        /// </summary>
        /// <returns> List<PanierItem></returns>
        public List<PanierItem> ObtenirPanier(string token)
        {
            return panierDAO.ObtenirPanier(tokenManager.GetPanierId(token));
        }

        /// <summary>
        /// Méthode pour supprimer un bijou du panier
        /// </summary>
        /// <param name="bijou"></param>
        public void SupprimerBijouDuPanier(string token, Bijou bijou)
        {
            panierDAO.SupprimerBijouDuPanier(tokenManager.GetPanierId(token), bijou);
        }

        /// <summary>
        /// Créer un token associé au panier
        /// </summary>
        /// <returns>token </returns>
        public string CreerPanierToken()
        {
             //Création d'un panier token
             string panierToken = tokenManager.CreerPanierToken();
             int id = tokenManager.GetPanierId(panierToken);
             //Créer le panier associé
             CreerPanier(id);
             return panierToken;
        }
        /// <summary>
        /// Créer un nouveau panier
        /// </summary>
        /// <param name="id">id du panier à créer</param>
        public void CreerPanier(int id)
        {
            panierDAO.CreerPanier(id);
        }

        public double CoutTotalPanier(string token)
        {
            return panierDAO.CoutTotalPanier(tokenManager.GetPanierId(token));
        }
    }
}
