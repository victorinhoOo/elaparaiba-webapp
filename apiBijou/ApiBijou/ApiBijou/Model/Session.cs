using API_SAE.Model;
using ApiBijou.Model.Panier;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Http;
using MySqlX.XDevAPI;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text.Json;

namespace ApiBijou.Model
{
    public class Session
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Constructeur de Session
        /// </summary>
        /// <param name="httpContextAccessor">Context HTTP</param>
        public Session(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            // Vérifier si un panier existe déjà en session
            if (_httpContextAccessor.HttpContext.Session.GetString("panier") == null) //Aucun panier n'existe, initialiser un nouveau panier
            {
                PanierBijoux panier = new PanierBijoux();
                UpdatePanier(panier);
            }
        }

        
        

        /// <summary>
        /// Ajouter un bijou au panier
        /// </summary>
        /// <param name="bijou">bijou à ajouter</param>
        public void AddBijoux(Bijou bijou)
        {
            
            PanierBijoux panierTemp = GetPanier();
            panierTemp.AddBijoux(bijou);
            UpdatePanier(panierTemp);
        }

        /// <summary>
        /// Supprimer un bijou du panier
        /// </summary>
        /// <param name="bijou">bijou à supprimer</param>
        public void DelBijoux(Bijou bijou)
        {
            PanierBijoux panierTemp = GetPanier();
            panierTemp.DelBijoux(bijou);
            UpdatePanier(panierTemp);
        }

        /// <summary>
        /// Met a jour le panier
        /// </summary>
        /// <param name="panier">Nouveau panier a attribué à la session</param>
        private void UpdatePanier(PanierBijoux panier)
        {
            // Stocker la chaîne JSON dans la session
            _httpContextAccessor.HttpContext.Session.SetString("panier", JsonConvert.SerializeObject(panier));            
        }

        /// <summary>
        /// Renvoi le panier
        /// </summary>
        /// <returns>Renvoi le panier</returns>
        public PanierBijoux GetPanier()
        {
            return JsonConvert.DeserializeObject<PanierBijoux>(_httpContextAccessor.HttpContext.Session.GetString("panier"));
        }


    }

}
