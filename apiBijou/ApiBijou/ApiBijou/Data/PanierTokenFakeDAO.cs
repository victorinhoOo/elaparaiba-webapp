﻿using ApiBijou.Model.Panier;
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
        private Dictionary<int, TokenDate> pannierToken;
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

        private PanierTokenFakeDAO()
        {
            
            this.pannierToken = new Dictionary<int, TokenDate>
        {
            { 0, new TokenDate(Token.GenerateToken(), new DateTime()) },
            { 1, new TokenDate(Token.GenerateToken(), new DateTime()) },
            { 2, new TokenDate(Token.GenerateToken(), new DateTime()) },
            { 3, new TokenDate(Token.GenerateToken(), new DateTime()) },
            { 4, new TokenDate("b70a8c45cb4366a02939b68e24ed126", new DateTime()) }, //On, attribue une variable arbitraire pour nos test
            { 5, new TokenDate(Token.GenerateToken(), new DateTime()) },
            { 6, new TokenDate(Token.GenerateToken(), new DateTime()) },
            { 7, new TokenDate(Token.GenerateToken(), new DateTime()) }
        };
        }



        public string CreerPanierToken()
        {
            string token = Token.GenerateToken();
            this.pannierToken.Add(pannierToken.Count(), new TokenDate(token, new DateTime()));
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


        class TokenDate
        {
            private string token;
            private DateTime date;
            public string Token
            {
                get { return token; }
                set { token = value; }
            }
            public DateTime Panier
            {
                get { return date; }
            }

            public TokenDate(string token, DateTime date)
            {
                this.token = token;
                this.date = date;
            }
        }
    }
}
