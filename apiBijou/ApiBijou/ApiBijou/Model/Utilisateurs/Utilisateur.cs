using Org.BouncyCastle.Asn1;

namespace ApiBijou.Model.Utilisateurs
{
    /// <summary>
    /// Représente un utilisateur
    /// </summary>
    public class Utilisateur
    {
        private string login;
        private string mdp;
        private string tokenPanier;

        public string Login
        {
            get { return login; }
            set { login = value; }
        }
        public string Mdp
        {
            get { return mdp; }
            set { mdp = value; }
        }

        public string TokenPanier
        {
            get { return tokenPanier; }
            set { tokenPanier = value; }
        }

    }
}
