using System;
using API_SAE.Model;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API_SAE.Data
{
    public class BijouFakeDAO : IBijouDAO
    {

        private static BijouFakeDAO instance;

        private Dictionary<int, Bijou> bijoux;

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


        private BijouFakeDAO()
        {
            this.bijoux = new Dictionary<int, Bijou>();        
            this.bijoux[0] = new Bijou(0, "Bijou1", "Ceci est un bijou", 125, 1, "20/10/2023");
            this.bijoux[1] = new Bijou(1, "Bijou2", "Ceci est un autre bijou", 80, 3, "18/04/2022");

        }
        public Bijou? getById(int id)
        {
            Bijou result = null;
            if(this.bijoux.ContainsKey(id))
            {
                result = this.bijoux[id];
            }
            return result;
        }

        public IEnumerable<Bijou> GetAllBijoux()
        {
            return this.bijoux.Values;
        }


        public bool AddBijou(Bijou? bijou)
        {
            bool res = false;
            if (bijou != null)
            {
                //Cherche le premier id libre
                int id = 0;
                while (this.bijoux.ContainsKey(id))
                {
                    id++;
                }
                //Ajoute l'élément
                bijou.Id = id;
                this.bijoux[id] = bijou;

                res = true;
            }
            return res;
        }

        public bool DeleteBijouById(int id)
        {
            bool result = false;
            if (this.bijoux.ContainsKey(id))
            {
                result = this.bijoux.Remove(id);
            }
            return result;
        }
    }
}
