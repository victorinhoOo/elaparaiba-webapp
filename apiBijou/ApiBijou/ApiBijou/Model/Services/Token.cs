using System;
using System.Security.Cryptography;

namespace ApiBijou.Model.Services
{
    public class Token
    {
        /// <summary>
        /// Génére un identifiant unique pour un user
        /// </summary>
        /// <returns></returns>
        public static string GenerateToken()
        {
            // Crée un tableau de bytes pour stocker l'identifiant unique
            byte[] bytes = new byte[16];

            // Utilise RNGCryptoServiceProvider pour générer des bytes aléatoires
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(bytes);
            }

            // Convertit les bytes en un identifiant de type Guid
            Guid tokenGuid = new Guid(bytes);

            // Convertit l'identifiant Guid en une chaîne hexadécimale
            string token = tokenGuid.ToString("N");

            return token;
        }
    }
}
