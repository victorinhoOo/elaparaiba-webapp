using System;
using ApiBijou.Data.Bijoux;
using ApiBijou.Image;
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

        private ImageManager imageManager;

        /// <summary>
        /// Constructeur (privé car singleton)
        /// </summary>
        private BijouManager()
        {
            bijouDAO = new BijouDAO();
            this.imageManager = new ImageManager();
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

        public bool DeleteBijouById(int id)
        {
            return bijouDAO.DeleteBijouById(id);
        }

        /// <summary>
        /// Modifier un bijou
        /// </summary>
        /// <param name="formulaireBijouModified">Attributs modifiés du bijou.</param>
        /// <returns></returns>
        public bool ModifierBijou(FormulaireBijouModified formulaireBijouModified)
        {
            bool res = false;
            try
            {
                Bijou bijou = bijouDAO.getById(formulaireBijouModified.IdBijou);
                //On modifie le bijou
                bijou = Bijou.ModifierBijou(bijou, formulaireBijouModified);

                if(formulaireBijouModified.Photos != null)
                {
                    //Modification des photos
                    imageManager.ModifiePhotoBijou(formulaireBijouModified.Photos, bijou.Type, bijou.DossierPhoto);
                }
                //Chagement dans les data
                res = bijouDAO.ModifierBijou(Convert.ToInt32(formulaireBijouModified.IdBijou), bijou);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());   
            }
            return res;
        }

        /// <summary>
        /// Ajouter un nouveau bijou.
        /// </summary>
        /// <param name="formulaireBijouModified">Attributs modifiés du bijou.</param>
        /// <returns></returns>
        public bool AjouterBijou(FormulaireBijouModified formulaireBijouModified)
        {
            bool res = false;
            try
            {
                //Création d'un bijou
                Bijou bijou = Bijou.NouveauBijou(formulaireBijouModified);
                bijou.DossierPhoto = imageManager.UploadPhotoBijou(formulaireBijouModified.Photos, bijou.Type);
                res = bijouDAO.AddBijou(bijou);

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return res;
        }
    }
}
