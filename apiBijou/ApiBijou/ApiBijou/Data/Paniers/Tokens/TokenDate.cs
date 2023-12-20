namespace ApiBijou.Data.Paniers.Tokens
{
    /// <summary>
    /// Classe associant un token et une date (Créée pour le fakdeDAO).
    /// </summary>
    public class TokenDate
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
