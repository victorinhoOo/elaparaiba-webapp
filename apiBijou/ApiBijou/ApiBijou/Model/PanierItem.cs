using API_SAE.Model;

namespace ApiBijou.Model
{
    /// <summary>
    /// Représente un bijou du panier
    /// </summary>
    public class PannierItem
    {
        private Bijou bijou;

        private int quantite;

        private int id;


        public Bijou Bijou { get => bijou; set => bijou = value; }
        public int Quantite { get => quantite; set => quantite = value; }
        public int Id { get => id; }

        public PannierItem(Bijou bijou, int id)
        {
            Bijou = bijou;
            Quantite = 1;
            id = id;

        }
    }
}
