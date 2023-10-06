namespace API_SAE.Model
{
    /// <summary>
    /// Classe des bijoux 
    /// </summary>
    public class Bijou
    {
        private int id;
        private string name;
        private string description;
        private int price;
        private int quantity;
        private string datepublication;

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

        public Bijou()
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.price = price;
            this.quantity = quantity;
            this.datepublication = datepublication;
        }
    }
}
