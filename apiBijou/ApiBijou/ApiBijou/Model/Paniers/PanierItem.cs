using ApiBijou.Model.Bijoux;
using Newtonsoft.Json;

namespace ApiBijou.Model.Panier
{
    /// <summary>
    /// Représente un bijou dans panier
    /// </summary>
    public class PanierItem
    {
        private Bijou bijou;
        private int quantite;
        private int id;
        public Bijou Bijou { get => bijou; set => bijou = value; }
        public int Quantite { get => quantite; set => quantite = value; }
        public int Id { get => id; }

        /// <summary>
        /// Constructeur de PanierItem
        /// </summary>
        /// <param name="bijou">bijou</param>
        /// <param name="id">id du bijou</param>
        public PanierItem(Bijou bijou, int id)
        {
            Bijou = bijou;
            Quantite = 1;
            this.id = id;

        }
    }
}
