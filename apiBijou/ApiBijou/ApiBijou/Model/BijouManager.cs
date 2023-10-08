using System;
using API_SAE.Data;

namespace API_SAE.Model
{
    /// <summary>
    /// Classe bijoux manager qui communique directement avec le DAO
    /// </summary>
    public class BijouManager
    {
        /// <summary>
        /// Singleton bijou manager
        /// </summary>
        private static BijouManager instance;

        public static BijouManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BijouManager();
                }
                return instance;
            }
        }
        /// <summary>
        /// DAO utilisé par bijou manager
        /// </summary>
        private IBijouDAO bijouDAO;

        /// <summary>
        /// Constructeur (privé car singleton)
        /// </summary>
        private BijouManager()
        {
            bijouDAO = BijouFakeDAO.Instance;
        }
        /// <summary>
        /// Envoi une demande GetBijouxById au DAO
        /// </summary>
        /// <param name="id">Id du bijoux à retourner</param>
        /// <returns></returns>
        public Bijou? GetBijouById(int id)
        {
            return bijouDAO.getById(id);
        }

        /// <summary>
        /// Envoi une demande GetAllBijoux au DAO
        /// </summary>
        /// <returns>IEnumerable<Bijou></returns>
        public IEnumerable<Bijou> GetAllBijoux()
        {
            return bijouDAO.GetAllBijoux();
        }

        public bool AddBijou(Bijou? user)
        {
            return bijouDAO.AddBijou(user);
        }

        public bool DeleteBijouById(int id)
        {
            return bijouDAO.DeleteBijouById(id);
        }
    }
}
