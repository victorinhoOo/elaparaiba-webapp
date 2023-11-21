namespace ApiBijou.Model.formModel
{
    public class FormulaireBijouModified
    {
        private int idBijou;
        private string name;
        private string description;
        private int quantity;
        private string type;
        private string matiere;
        private int prix;
        private DateTime datePublication;
        private List<IFormFile>? photos;

        public int IdBijou
        {
            get { return idBijou; }
            set { idBijou = value; }
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

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public string Matiere
        {
            get { return matiere; }
            set { matiere = value; }
        }

        public int Prix
        {
            get { return prix; }
            set { prix = value; }
        }

        public DateTime DatePublication
        {
            get { return datePublication; }
            set { datePublication = value; }
        }

        public List<IFormFile>? Photos
        {
            get { return photos; }
            set { photos = value; }
        }
    }
}
