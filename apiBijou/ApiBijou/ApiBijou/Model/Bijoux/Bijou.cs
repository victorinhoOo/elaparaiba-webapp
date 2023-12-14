using ApiBijou.Model.formModel;

namespace ApiBijou.Model.Bijoux
{
    /// <summary>
    /// Gère les caractéristiques des bijoux
    /// </summary>
    public class Bijou
    {
        private int id;
        private string name;
        private string description;
        private int price;
        private int quantity;
        private string datepublication;
        private string type;
        private string dossierPhoto;
        private int nbPhotos;

        public int NbPhotos
        {
            get { return nbPhotos; }
            set { nbPhotos = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public int Price
        {
            get { return price; }
            set { price = value; }
        }
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }


        public string Datepublication
        {
            get { return datepublication; }
            set { datepublication = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public string DossierPhoto
        {
            get { return dossierPhoto; }
            set { dossierPhoto = value; }
        }

        public override bool Equals(object? obj)
        {
            return obj is Bijou bijou &&
                   id == bijou.id &&
                   name == bijou.name &&
                   description == bijou.description &&
                   price == bijou.price &&
                   quantity == bijou.quantity &&
                   datepublication == bijou.datepublication &&
                   type == bijou.type &&
                   dossierPhoto == bijou.dossierPhoto;
        }

        /// <summary>
        /// Modifier un bijou.
        /// </summary>
        /// <param name="formulaireBijouModified">Nouveaux attributs à hydrater</param>
        /// <returns></returns>
        /// <exception cref="Exception">Un des élèments du formulaire n'est pas au bon format</exception>
        public static Bijou ModifierBijou(Bijou bijou, FormulaireBijouModified formulaireBijouModified)
        {
            try 
            {
                bijou.Price = formulaireBijouModified.Prix;
                bijou.Quantity = formulaireBijouModified.Quantity;
                bijou.Description = formulaireBijouModified.Description;
                bijou.Type = formulaireBijouModified.Type;
                //Ajouter la matiere
                bijou.Datepublication = Convert.ToString(formulaireBijouModified.DatePublication);
                bijou.Name = formulaireBijouModified.Name;
                //Modifier le nombre de photo
                bijou.nbPhotos = formulaireBijouModified.Photos.Count();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return bijou;
        }

        /// <summary>
        /// Ajouter un nouveau bijou.
        /// </summary>
        /// <param name="formulaireBijouModified"></param>
        /// <returns></returns>
        public static Bijou NouveauBijou(FormulaireBijouModified formulaireBijouModified)
        {
            Bijou bijou = new Bijou();
            try
            {
                //On hydrate le nouveau bijou à partir des données du formulaire
                bijou.Price = formulaireBijouModified.Prix;
                bijou.Quantity = formulaireBijouModified.Quantity;
                bijou.Description = formulaireBijouModified.Description;
                bijou.Type = formulaireBijouModified.Type;
                bijou.Datepublication = Convert.ToString(formulaireBijouModified.DatePublication);
                bijou.Name = formulaireBijouModified.Name;
                bijou.NbPhotos = formulaireBijouModified.Photos.Count();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return bijou;
        }
    }
}
