using System;
using ApiBijou.Data.Bijoux;
using ApiBijou.Model.formModel;

namespace ApiBijou.Model.Bijoux
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

        /// <summary>
        /// Diminue le stock d'un bijou spécifié par ID
        /// </summary>
        /// <param name="id">ID du bijou</param>
        /// <returns>Vrai si la mise à jour a réussi, sinon faux</returns>
        public bool DecreaseStockById(int id, int quantite)
        {
            return bijouDAO.DecreaseStock(id, quantite);
        }


        public bool AddBijou(Bijou? user)
        {
            return bijouDAO.AddBijou(user);
        }

        public bool DeleteBijouById(int id)
        {
            return bijouDAO.DeleteBijouById(id);
        }

        public bool modifierBijou(FormulaireBijouModified formulaireBijouModified)
        {
            bool res = false;
            try
            {
                Bijou bijou = bijouDAO.getById(Convert.ToInt32(formulaireBijouModified.IdBijou));
                //On modifie le bijou
                bijou = Bijou.ModifierBijou(bijou, formulaireBijouModified);
                //Chagement dans les data
                res = bijouDAO.ModifierBijou(Convert.ToInt32(formulaireBijouModified.IdBijou), bijou);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());   
            }
            return res;
        }
    }
}
