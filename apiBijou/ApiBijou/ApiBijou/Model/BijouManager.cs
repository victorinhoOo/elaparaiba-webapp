using System;
using API_SAE.Data;

namespace API_SAE.Model
{
    public class BijouManager
    {
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

        private IBijouDAO bijouDAO;

        private BijouManager()
        {
            bijouDAO=new BijouDAO();
        }

        public Bijou? GetBijouById(int id)
        {
            return bijouDAO.getById(id);
        }

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
