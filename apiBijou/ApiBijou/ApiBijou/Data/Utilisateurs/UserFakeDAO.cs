using System.ComponentModel;
using ApiBijou.Model.Bijoux;
using ApiBijou.Model.Utilisateurs;

namespace ApiBijou.Data.Utilisateurs
{
    public class UserFakeDAO : IUserDAO
    {
        private static UserFakeDAO instance;
        /// <summary>
        /// Instance public du singleton
        /// </summary>
        public static UserFakeDAO Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new UserFakeDAO();
                }
                return instance;
            }
        }

        //Liste des utilisateurs ayant le droit admin
        private Dictionary<int, Utilisateur> utilsateurs = new Dictionary<int, Utilisateur>
        {
            { 0, new Utilisateur { Login = "root", Mdp="0da651f3a757364a4a6ce8730990afa46fe8d62e95f26172c47ca2fde814c6f7", TokenPanier = "7adfb95552bcfee48c44d9e7d129d3d1" } }
        };

        /// <summary>
        /// Renvoi la liste des utilisateurs administrateurs
        /// </summary>
        public List<Utilisateur> Utilisateur
        {
            get
            {
                return utilsateurs.Values.ToList();
            }
        }

        public bool IsAdmin(string tokenPanier)
        {
            bool res = false;
            foreach(int idAdmin  in utilsateurs.Keys)
            {
                if (utilsateurs[idAdmin].TokenPanier == tokenPanier)
                {
                    res = true;
                }
            }
            return res;
        }

        public bool GiveAdmin(string tokenPanier)
        {
            bool res = false;
            try //Création d'un nouveau tuple ajoutant le token panier à la table utilisateur
            {
                this.utilsateurs[utilsateurs.Count()] = new Utilisateur { Login = "root", Mdp = "0da651f3a757364a4a6ce8730990afa46fe8d62e95f26172c47ca2fde814c6f7", TokenPanier = tokenPanier };
                res = true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return res;
        }

        public bool CheckLoginPwd(string login, string pwd)
        {
            bool res = false;
            if (utilsateurs[0].Login == login && utilsateurs[0].Mdp == pwd) //Le password et le mot de passe correspondent
            {
                res = true;
            }
            return res;
        }

        private UserFakeDAO() { }
    }
}
