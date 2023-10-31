using API_SAE.Model;

namespace ApiBijou.Model
{
    /// <summary>
    /// Représente un bijou du panier
    /// </summary>
    public class BijouInPanier
    {
        private Bijou bijou;

        private int quantite;

        private int id;


        public Bijou Bijou { get => bijou; set => bijou = value; }
        public int Quantite { get => quantite; set => quantite = value; }
        public int Id { get => id; set => id = value; }
    }
}
