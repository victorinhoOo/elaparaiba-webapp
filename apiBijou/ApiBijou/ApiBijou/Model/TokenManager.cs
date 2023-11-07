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
            this.panierTokenDAO = new PanierTokenFakeDAO();
        }


    }
}
