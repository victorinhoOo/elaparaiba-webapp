using ApiBijou.Model.Panier;
using ApiBijou.Model.Services;
using System;
using System.Security.Cryptography;
using System.Text;

namespace ApiBijou.Data
{
    public class PanierTokenFakeDAO : IPanierTokenDAO
    {
        /// <summary>
        /// Singleton de la classe
        /// </summary>
        private static PanierTokenFakeDAO instance;
        private Dictionary<int, PanierToken> pannierToken;
        /// <summary>
        /// Propriété d'accès au singleton
        /// </summary>
        public static PanierTokenFakeDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PanierTokenFakeDAO();
                }
                return instance;
            }
        }
        public PanierTokenFakeDAO()
        {
            MD5 md5 = MD5.Create();
            this.pannierToken = new Dictionary<int, PanierToken>
            {
                { 0, new PanierToken(Encoding.UTF8.GetString(md5.ComputeHash(Encoding.UTF8.GetBytes("0"))), new PanierBijoux()) },
                { 1, new PanierToken(Encoding.UTF8.GetString(md5.ComputeHash(Encoding.UTF8.GetBytes("1"))), new PanierBijoux()) },
                { 2, new PanierToken(Encoding.UTF8.GetString(md5.ComputeHash(Encoding.UTF8.GetBytes("2"))), new PanierBijoux()) },
                { 3, new PanierToken(Encoding.UTF8.GetString(md5.ComputeHash(Encoding.UTF8.GetBytes("3"))), new PanierBijoux()) },
                { 4, new PanierToken(Encoding.UTF8.GetString(md5.ComputeHash(Encoding.UTF8.GetBytes("4"))), new PanierBijoux()) },
                { 5, new PanierToken(Encoding.UTF8.GetString(md5.ComputeHash(Encoding.UTF8.GetBytes("5"))), new PanierBijoux()) },
                { 6, new PanierToken(Encoding.UTF8.GetString(md5.ComputeHash(Encoding.UTF8.GetBytes("6"))), new PanierBijoux()) },
                { 7, new PanierToken(Encoding.UTF8.GetString(md5.ComputeHash(Encoding.UTF8.GetBytes("7"))), new PanierBijoux()) },

            };
        }

        public string CreerPanierToken()
        {
            string token = Token.GenerateToken();
            this.pannierToken.Add(pannierToken.Count(), new PanierToken(Token.GenerateToken(), new PanierBijoux()));
            return token;
        }

        public int getPanierId(string token)
        {
            int res = -1;
            foreach (int keys in pannierToken.Keys)
            {
                if (pannierToken[keys].Token == token)
                {
                    res = keys;
                }
            }
            return res;
        }


        class PanierToken
        {
            private string token;
            private PanierBijoux panier;
            public string Token
            {
                get { return token; }
                set { token = value; }
            }
            public PanierBijoux Panier
            {
                get { return panier; }
            }

            public PanierToken(string token, PanierBijoux panier)
            {
                this.token = token;
                this.panier = panier;
            }
        }
    }
}
