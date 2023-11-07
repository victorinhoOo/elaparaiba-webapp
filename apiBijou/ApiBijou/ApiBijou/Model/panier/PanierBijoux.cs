using API_SAE.Model;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiBijou.Model.Panier
{   /// <summary>
    /// Cette classe représente le pannier d'une session
    /// </summary>
    public class PanierBijoux
    {
        /// <summary>
        /// Liste des bijoux présents dans le panier
        /// </summary>
        [JsonProperty]
        private List<PanierItem> bijoux = new List<PanierItem>();

        /// <summary>
        /// Ajouter un bijou au panier
        /// </summary>
        /// <param name="bijou">bijou à ajouter</param>
        public void AddBijoux(Bijou bijou)
        {
            int position = ContientBijou(bijou);
            if (position != -1)//Il y a déja un bijou de ce type dans le panier
            {
                bijoux[position].Quantite += 1;
            }
            else//On ajoute un bijou à la suite 
            {
                bijoux.Add(new PanierItem(bijou, bijoux.Count));
            }      
        }

        /// <summary>
        /// Supprimer un bijou du panier
        /// </summary>
        /// <param name="bijou">bijou à supprimer</param>
        public void DelBijoux(Bijou bijou)
        {
            int position = ContientBijou(bijou);
            if (position != -1)
            {
                if (bijoux[position].Quantite > 1)
                {
                    bijoux[position].Quantite -= 1;
                }
                else
                {
                    bijoux.RemoveAt(position);
                }
            }
        }

        /// <summary>
        /// Obtenir les bijoux du panier
        /// </summary>
        /// <returns></returns>
        public List<PanierItem> GetBijoux()
        {
            List<PanierItem> res = new List<PanierItem>();
            foreach(PanierItem item in bijoux)
            {
                res.Add(item);
            }
            return res;
        }

        /// <summary>
        /// Calculer le coût total du panier
        /// </summary>
        public decimal Total => bijoux.Sum(b => b.Bijou.Price * b.Quantite);

        /// <summary>
        /// Renvoi la position d'un bijo dans le panier. Ou -1 si il n'est pas présent
        /// </summary>
        /// <param name="bijou">Bijou à réchercher dans le panier</param>
        /// <returns></returns>
        public int ContientBijou(Bijou bijou)
        {
            bool trouver = false;
            int index = 0;
            while (!trouver && index < bijoux.Count)
            {
                if (bijoux[index].Bijou.Equals(bijou)) //Le bijou est dans la liste
                {
                    trouver = true;
                }
                else //On passe au bijou suivant
                {
                    index++;
                }
            }
            if (!trouver)
            {
                index = -1;
            }
            return index;
        }
    }

}