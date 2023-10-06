using API_SAE.Model;

namespace API_SAE.Data
{
    /// <summary>
    /// Interface des DAOS
    /// </summary>
    public interface IBijouDAO
    {
        public Bijou? getById(int id);

        public IEnumerable<Bijou> GetAllBijoux();

        public bool AddBijou(Bijou? bijou);

        public bool DeleteBijouById(int id);


    }
}
