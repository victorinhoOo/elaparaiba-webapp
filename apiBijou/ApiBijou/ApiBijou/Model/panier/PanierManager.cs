using API_SAE.Model;
using System.Runtime.CompilerServices;

namespace ApiBijou.Model.Panier
{
    public class PanierManager
    {
        private TokenManager tokenManager;
        /// <summary>
        /// Ajouter un bijou au panier
        /// </summary>
        /// <param name="bijou">Bijou à ajouter</param>
        public void AjouterBijouAuPanier(string token, Bijou bijou)
        {
           throw new NotImplementedException();
        }

        /// <summary>
        /// Renvoi la liste des bijoux du panier
        /// </summary>
        /// <returns> List<PanierItem></returns>
        public List<PanierItem> ObtenirPanier(string token)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Méthode pour supprimer un bijou du panier
        /// </summary>
        /// <param name="bijou"></param>
        public void SupprimerBijouDuPanier(string token, Bijou bijou)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Créer un panier token
        /// </summary>
        /// <returns></returns>
        public string CreerPanierToken()
        {
             return tokenManager.CreerPanierToken();
        }
    }
}
