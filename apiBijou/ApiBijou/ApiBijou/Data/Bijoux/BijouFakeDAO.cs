using ApiBijou.Model.Bijoux;

namespace ApiBijou.Data.Bijoux
{
    /// <summary>
    /// Fake DAO de bijou
    /// </summary>
    public class BijouFakeDAO : IBijouDAO
    {
        /// <summary>
        /// Singleton de la classe
        /// </summary>
        private static BijouFakeDAO instance;
        private Dictionary<int, Bijou> bijoux;
        /// <summary>
        /// Propriété pour  accèder au singleton
        /// </summary>
        public static BijouFakeDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BijouFakeDAO();
                }
                return instance;
            }
        }

        /// <summary>
        /// Constructeur, on définit dictionnaire de bijoux.
        /// </summary>
        private BijouFakeDAO()
        {

            bijoux = new Dictionary<int, Bijou>
        {
            { 0, new Bijou { Id = 0, Name = "Boucles d'oreille collection 'Géométrie abstraite'",Description = "Description du bijou 1", Price = 69, Quantity=2, Type="Bo", DossierPhoto="Bo18", NbPhotos=4, Datepublication="01/01/1000"} },
            { 1, new Bijou { Id = 1, Name = "Bracelet, Manchette", Description = "Description du bijou 2", Price = 96, Quantity=3, Type="Bracelets", DossierPhoto="Bra39", NbPhotos=5, Datepublication="01/01/1000"} },
            { 2, new Bijou { Id = 2, Name = "Collier, collection 'Élégance'", Description = "Description du bijou 3", Price = 49, Type="Colliers", DossierPhoto="Col43", NbPhotos=4, Datepublication="01/01/1000"} },
            { 3, new Bijou { Id = 3, Name = "Bague, collection 'Élégance'", Description = "Description du bijou 4", Price = 78, Type="Bagues", DossierPhoto="Bague62", NbPhotos=3, Datepublication = "01/01/1000"} },
            { 4, new Bijou { Id = 4, Name = "Bracelet, collection 'Nature et Botanique'", Description = "Description du bijou 5", Price = 52, Type="Bracelets", DossierPhoto="Bra73", NbPhotos=5 , Datepublication = "01/01/1000"} },
            { 5, new Bijou { Id = 5, Name = "Boucles d'oreilles collection 'Nature et Botanique'", Description = "Description du bijou 6", Price = 115, Type="Bo", DossierPhoto="Bo62", NbPhotos=4 , Datepublication = "01/01/1000"} },
            { 6, new Bijou { Id = 6, Name = "Bracelet, collection 'Entrelacs'", Description = "Description du bijou 7", Price = 50, Type="Bracelets", DossierPhoto="Bra71", NbPhotos=4 , Datepublication = "01/01/1000"} },
            { 7, new Bijou { Id = 7, Name = "Collier, collection 'Nature et Botanique'", Description = "Description du bijou 8", Price = 70, Type="Colliers", DossierPhoto="Col60", NbPhotos=9 , Datepublication = "01/01/1000"} },
            { 8, new Bijou { Id = 8, Name = "Collier, collection 'Nature et Botanique'", Description = "Description du bijou 9", Price = 62, Type="Colliers", DossierPhoto="Col66", NbPhotos=7 , Datepublication = "01/01/1000"} },
            { 9, new Bijou { Id = 9, Name = "Boucles d'oreilles collection 'Nature et Botanique'", Description = "Description du bijou 10", Price = 85, Type="Bo", DossierPhoto="Bo66", NbPhotos=5 , Datepublication = "01/01/1000"} },
            { 10, new Bijou { Id = 10, Name = "Bague, collection 'Géométrie abstraite'", Description = "Description du bijou 11", Price = 56, Type="Bagues", DossierPhoto="Bague71", NbPhotos=3 , Datepublication = "01/01/1000"} },
            { 11, new Bijou { Id = 11, Name = "Bague, collection 'Élégance'", Description = "Description du bijou 12", Price = 56, Type="Bagues", DossierPhoto="Bague64", NbPhotos=5  , Datepublication = "01/01/1000"} }
        };

        }

        public bool AddBijou(Bijou? bijou)
        {
            bool result = false;
            if (!bijoux.ContainsKey(bijou.Id))
            {
                bijoux[bijoux.Count] = bijou;
                result = true;
            }
            return result;
        }

        public bool DeleteBijouById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Bijou> GetAllBijoux()
        {
            return bijoux.Values;
        }

        public Bijou? getById(int id)
        {
            Bijou bijou = null;
            if (bijoux.ContainsKey(id))
            {
                bijou = bijoux[id];
            }
            return bijou;
        }

        public bool ModifierBijou(int idBijou, Bijou bijou)
        {
            bool res = false;
            if (bijoux.ContainsKey(idBijou))
            {
                bijoux[idBijou] = bijou;
                res = true;
            }
            return res;
        }
    }
}
