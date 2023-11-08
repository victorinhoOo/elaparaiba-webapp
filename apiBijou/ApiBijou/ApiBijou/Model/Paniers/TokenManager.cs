using ApiBijou.Data.Paniers.Tokens;

namespace ApiBijou.Model.Paniers
{
    public class TokenManager
    {
        /// <summary>
        /// Dao utilisé
        /// </summary>
        private IPanierTokenDAO panierTokenDAO;

        public TokenManager()
        {
            panierTokenDAO = PanierTokenFakeDAO.Instance;
        }

        /// <summary>
        /// Apelle le DAO pour créer un token
        /// </summary>
        /// <returns>token créé</returns>
        public string CreerPanierToken()
        {
            return panierTokenDAO.CreerPanierToken();
        }

        /// <summary>
        /// Appelle le DAO pour récupérer l'id correspondant à un token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public int GetPanierId(string token)
        {
            return panierTokenDAO.GetPanierId(token);
        }


    }
}
