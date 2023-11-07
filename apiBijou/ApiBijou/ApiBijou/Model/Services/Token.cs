using System.Security.Cryptography;
using System.Text;

namespace ApiBijou.Model.Services
{
    public class Token
    {
        public static string GenerateToken()
        {
            // Créez une instance de la classe Random
            Random random = new Random();
            //Génére un nombre aléatoire entre 1 et 30000
            int nombreAleatoire = random.Next(1, 30001);
            MD5 md5 = MD5.Create();
            string token = Encoding.UTF8.GetString(md5.ComputeHash(Encoding.UTF8.GetBytes(Convert.ToString(nombreAleatoire))));
            return token;
        }
    }
}
