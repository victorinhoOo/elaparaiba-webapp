using API_SAE.Model;
using ApiBijou.Model;

namespace ApiBijou.Data
{
    /// <summary>
    /// Interface des DAO Bijoux
    /// </summary>
    public interface IPanierDAO
    {
        /// <summary>
        /// Créer un panier
        /// </summary>
        /// <param name="bijou">panier</param>
        /// <returns>vrai si l'ajout du panier a réussi</returns>
        public bool CreatePanier(Panier? panier);

        /// <summary>
        /// Ajoute un bijou au panier
        /// </summary>
        /// <param name="bijou">bijou à ajouter</param>
        /// <returns>vrai si l'ajout a réussi</returns>
        public bool AddBijou(Bijou? bijou);

        /// <summary>
        /// Supprime un bijou du panier
        /// </summary>
        /// <param name="id">id du bijou à supprimer</param>
        /// <returns>vrai si la suppresion a réussi</returns>
        public bool DeleteBijouById(int id);


    }
}
