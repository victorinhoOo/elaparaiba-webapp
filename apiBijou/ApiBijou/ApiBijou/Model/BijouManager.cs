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

        private IBijouDAO BijouDAO => BijouFakeDAO.Instance;

        private BijouManager()
        {
        }

        public Bijou? GetBijouById(int id)
        {
            return BijouDAO.getById(id);
        }

        public IEnumerable<Bijou> GetAllBijoux()
        {
            return BijouDAO.GetAllBijoux();
        }

        public bool AddBijou(Bijou? user)
        {
            return BijouDAO.AddBijou(user);
        }

        public bool DeleteBijouById(int id)
        {
            return BijouDAO.DeleteBijouById(id);
        }
    }
}
