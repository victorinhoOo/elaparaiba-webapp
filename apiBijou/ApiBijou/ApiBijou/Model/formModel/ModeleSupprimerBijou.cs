using Org.BouncyCastle.Asn1.Mozilla;

namespace ApiBijou.Model.formModel
{
    /// <summary>
    /// Modele de données pour supprimer un bijou
    /// </summary>
    public class ModeleSupprimerBijou
    {
        private string tokenPanier;
        private string idBijou;

        public string TokenPanier
        {
            get { return tokenPanier; }
            set { tokenPanier = value; }
        }

        public string IdBijou
        {
            get { return idBijou; }
            set { idBijou = value; }
        }
    }
}
