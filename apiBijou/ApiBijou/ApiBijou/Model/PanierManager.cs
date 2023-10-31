using System;
using API_SAE.Data;

namespace API_SAE.Model
{
    /// <summary>
    /// Classe panier manager qui communique directement avec le DAO
    /// </summary>
    public class PanierManager
    {
        /// <summary>
        /// Singleton Panier manager
        /// </summary>
        private static PanierManager instance;

        public static PanierManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PanierManager();
                }
                return instance;
            }
        }
        /// <summary>
        /// DAO utilisé par panier manager
        /// </summary>
        private IBijouDAO bijouDAO;

        /// <summary>
        /// Constructeur (privé car singleton)
        /// </summary>
        private PanierManager()
        {
            bijouDAO = BijouFakeDAO.Instance;
        }




    }
}

