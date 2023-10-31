using API_SAE.Model;

namespace ApiBijou.Model
{
    /// <summary>
    /// Représente un panier
    /// </summary>
    public class Panier
    {
        private int id;

        private List<BijouInPanier> bijoux = new List<BijouInPanier>();

        public List<BijouInPanier> Bijoux { get => bijoux; set => bijoux = value; }
        public int Id { get => id; set => id = value; }


        /// <summary>
        /// Prix total du panier à partir de chaque bijou dans le panier
        /// </summary>
        public decimal Total => bijoux.Sum(b => b.Bijou.Price * b.Quantite);

    }

}
