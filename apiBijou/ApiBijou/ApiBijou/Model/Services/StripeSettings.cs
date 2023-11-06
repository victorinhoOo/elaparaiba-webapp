namespace ApiBijou.Model.Services
{
    public class StripeSettings
    {
        private string secretKey;
        private string publicKey;

        public string SecretKey { get => secretKey; set => secretKey = value; }
        public string PublicKey { get => publicKey; set => publicKey = value; }
    }
}
