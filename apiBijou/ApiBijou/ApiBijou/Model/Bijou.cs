namespace API_SAE.Model
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

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Name
        {
            get { return name;}
            set { name = value; }
        }

        public string Description
        {
            get { return description;}
            set { description = value; }
        }
        public int Price
        {
            get { return price;}
            set { price = value; }
        }
        public int Quantity
        {
            get { return quantity;}
            set { quantity = value; }
        }


        public string Datepublication
        {
            get { return datepublication; }
            set { datepublication = value; }
        }

        public String Type
        {
            get { return type; }
            set { type = value; }
        }

        public String DossierPhoto
        {
            get { return dossierPhoto; }
            set { dossierPhoto = value; }
        }

    }
}
