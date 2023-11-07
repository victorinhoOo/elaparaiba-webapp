using ApiBijou.Data;

namespace ApiBijou.Model
{
    public class TokenManager
    {
        /// <summary>
        /// Dao utilisé
        /// </summary>
        private IPanierTokenDAO panierTokenDAO;

        public TokenManager()
        {
            this.panierTokenDAO = PanierTokenFakeDAO.Instance;
        }

        public string CreerPanierToken()
        {
            return panierTokenDAO.CreerPanierToken();
        }

        public int GetPanierId(string token)
        {
            return panierTokenDAO.getPanierId(token);
        }


    }
}
