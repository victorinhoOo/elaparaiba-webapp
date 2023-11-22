namespace ApiBijou.Data.Utilisateurs
{
    public interface IUserDAO
    {
        /// <summary>
        /// Renvoi true si la personne est admin
        /// </summary>
        /// <param name="tokenPanier">token panier à checker</param>
        /// <returns></returns>
        public bool IsAdmin(string tokenPanier);

        /// <summary>
        /// Donne les droits d'administration
        /// </summary>
        /// <param name="tokenPanier">id de l'utilisateur</param>
        /// <returns></returns>
        public bool GiveAdmin(string tokenPanier);

        /// <summary>
        /// Vérifie la validité du login et du password.
        /// </summary>
        /// <param name="tokenPanier">token panier du créateur de la requête</param>
        /// <param name="login"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public bool CheckLoginPwd(string login, string pwd);

    }
}
