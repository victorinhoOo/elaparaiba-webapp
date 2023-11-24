using ApiBijou.Data.Paniers.Tokens;
using ApiBijou.Data.Utilisateurs;
using System.Security.Cryptography;
using System.Text;

namespace ApiBijou.Model.Utilisateurs
{

    /// <summary>
    /// UtilisateursManager.
    /// Il n y a qu'un admin mais il peut avoir différents tokenPanier (1 par session).
    /// Il y a donc que un seul login et mdp.
    /// </summary>
    public class UtilisateursManager
    {
        
        private IUserDAO userDAO = UserFakeDAO.Instance;
        
        /// <summary>
        /// Hash le password
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - retourne un tableau de bytes
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convertir le tableau de bytes en une chaîne de caractères hexadécimaux
                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        /// <summary>
        /// Vérifie la véracité du login et du mdp.
        /// </summary>
        /// <param name="login">login</param>
        /// <param name="pwd">mdp</param>
        /// <returns></returns>
        public bool CheckLoginPwd(string login, string pwd)
        {
            return userDAO.CheckLoginPwd(login, HashPassword(pwd));
        }
        
        /// <summary>
        /// Donne les droits d'administrateusr à un utilisateur.
        /// </summary>
        /// <param name="tokenPanier">token associé à la connexion</param>
        /// <returns></returns>
        public bool GiveAdmin(string tokenPanier)
        {
            return userDAO.GiveAdmin(tokenPanier);
        }
        /// <summary>
        /// Vérifie si un utilisateur possède les droits d'administration.
        /// </summary>
        /// <param name="tokenPanier"></param>
        /// <returns></returns>
        public bool IsAdmin(string tokenPanier)
        {
            return userDAO.IsAdmin(tokenPanier);
        }

        /// <summary>
        /// Effectue une connexion en tant qu'administrateur.
        /// </summary>
        /// <param name="tokenPanier"></param>
        /// <param name="login"></param>
        /// <param name="pwd"></param>
        /// <returns>Renvoi true si la connexion à été validée et false si elle a été refusée.</returns>
        public bool ConnectAsAdmin(string tokenPanier, string login, string pwd)
        {
            bool res = false;
            if (IsAdmin(tokenPanier)) //L'admin existe déja
            {
                res = true;
            }
            else//L'admin existe pas
            {
                bool authentification = CheckLoginPwd(login, pwd);
                if (authentification) //Authentification validée
                {
                    GiveAdmin(tokenPanier);
                    res = true;
                }
            }
            return res;
        }
    }
}
