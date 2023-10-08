using API_SAE.Data;
using API_SAE.Model;

namespace API_SAE.Data
{
    /// <summary>
    /// Fake DAO 
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

            this.bijoux = new Dictionary<int, Bijou>
        {
            { 0, new Bijou { Id = 0, Name = "Boucles d'oreille collection Géométrie abstraite'", Description = "Description du bijou 1", Price = 69, Type="Bo", DossierPhoto="Bo18"} },
            { 1, new Bijou { Id = 1, Name = "Bracelet, Manchette", Description = "Description du bijou 2", Price = 96, Type="Bracelets", DossierPhoto="Bra39"} },
            { 2, new Bijou { Id = 2, Name = "Collier, collection 'Élégance'", Description = "Description du bijou 3", Price = 49, Type="Colliers", DossierPhoto="Col43"} },
            { 3, new Bijou { Id = 3, Name = "Bague, collection 'Élégance'", Description = "Description du bijou 4", Price = 78, Type="Bagues", DossierPhoto="Bague62"} },
            { 4, new Bijou { Id = 4, Name = "Bracelet, collection 'Nature et Botanique'", Description = "Description du bijou 5", Price = 52, Type="Bracelets", DossierPhoto="Bra73" } },
            { 5, new Bijou { Id = 5, Name = "Boucles d'oreilles collection 'Nature et Botanique'", Description = "Description du bijou 6", Price = 115, Type="Bo", DossierPhoto="Bo62" } },
            { 6, new Bijou { Id = 6, Name = "Bracelet, collection 'Entrelacs'", Description = "Description du bijou 7", Price = 50, Type="Bracelets", DossierPhoto="Bra71" } },
            { 7, new Bijou { Id = 7, Name = "Collier, collection 'Nature et Botanique'", Description = "Description du bijou 8", Price = 70, Type="Colliers", DossierPhoto="Col60" } },
            { 8, new Bijou { Id = 8, Name = "Collier, collection 'Nature et Botanique'", Description = "Description du bijou 9", Price = 62, Type="Colliers", DossierPhoto="Col66" } },
            { 9, new Bijou { Id = 9, Name = "Boucles d'oreilles collection 'Nature et Botanique'", Description = "Description du bijou 10", Price = 85, Type="Bo", DossierPhoto="Bo66" } },
            { 10, new Bijou { Id = 10, Name = "Bague, collection 'Géométrie abstraite'", Description = "Description du bijou 11", Price = 56, Type="Bagues", DossierPhoto="Bague71" } },
            { 11, new Bijou { Id = 11, Name = "Bague, collection 'Élégance'", Description = "Description du bijou 12", Price = 56, Type="Bagues", DossierPhoto="Bague64"  } }
        };

        }

        public bool AddBijou(Bijou? bijou)
        {
            bool result = false;
            if (!bijoux.ContainsKey(bijou.Id))
            {
                this.bijoux[bijoux.Count] = bijou;
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
            return this.bijoux.Values;
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
    }
}
