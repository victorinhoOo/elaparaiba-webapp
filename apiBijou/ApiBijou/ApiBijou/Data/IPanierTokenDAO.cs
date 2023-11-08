﻿namespace ApiBijou.Data
{
    public interface IPanierTokenDAO
    {
        /// <summary>
        /// Créer un nouveau Panier Token et le renvoi au client
        /// </summary>
        public string CreerPanierToken();
        /// <summary>
        /// Renvoi l'id du Pannier associé au token
        /// </summary>
        /// <param name="token">Token associé au pannier</param>
        /// <returns></returns>
        public int GetPanierId(string token);

    }
}
