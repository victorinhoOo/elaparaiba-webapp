namespace ApiBijou.Model.Mail
{
    /// <summary>
    /// Cette classe définit le modèle du questionnaire Sur Mesure
    /// </summary>
    public class FormulaireSurMesureModel
    {
        private string _nom;
        private string _prenom;
        private string _email;
        private string _matiere;
        private string _type;
        private string _description;

       
        public string Nom
        {
            get { return _nom; }
            set { _nom = value; }
        }

        public string Prenom
        {
            get { return _prenom; }
            set { _prenom = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string Matiere
        {
            get { return _matiere; }
            set { _matiere = value; }
        }

        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
    }
}
