namespace ApiBijou.Model.Services
{
    /// <summary>
    /// Classe liée aux paramètres de Stripe, 
    /// </summary>
    public class StripeSettings
    {
        private string secretKey;
        private string publicKey;


        public string SecretKey { get => secretKey; set => secretKey = value; }
        public string PublicKey { get => publicKey; set => publicKey = value; }
    }
}
