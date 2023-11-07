using API_SAE.Model;

namespace ApiBijou.Model.Panier
{
    public class PanierManager
    {
        /// <summary>
        /// Panier manager 
        /// </summary>
        private readonly Session session;

        public PanierManager(Session session)
        {
            this.session = session;
        }

        /// <summary>
        /// Ajouter un bijou au panier
        /// </summary>
        /// <param name="bijou">Bijou à ajouter</param>
        public void AjouterBijouAuPanier(Bijou bijou)
        {
            session.AddBijoux(bijou);
        }

        /// <summary>
        /// Renvoi la liste des bijoux du panier
        /// </summary>
        /// <returns> List<PanierItem></returns>
        public List<PanierItem> ObtenirPanier()
        {
            return session.GetPanier().GetBijoux();
        }

        /// <summary>
        /// Méthode pour supprimer un bijou du panier
        /// </summary>
        /// <param name="bijou"></param>
        public void SupprimerBijouDuPanier(Bijou bijou)
        {
            session.DelBijoux(bijou);
        }
    }
}
