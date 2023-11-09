namespace ApiBijou.Model.Mail
{
    /// <summary>
    /// Structure de données du formulaire OuMeTrouver
    /// </summary>
    public class FormulaireOuMeTrouverData
    {
        private string _email;
        private string _message;

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }
    }
}
