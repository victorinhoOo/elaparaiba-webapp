namespace ApiBijou.Model.formModel
{
    /// <summary>
    /// Structure de données du formulaire SurMesure
    /// </summary>
    public class FormulaireSurMesureData
    {
        private string _nom;
        private string _prenom;
        private string _email;
        private string _matiere;
        private string _type;
        private string _description;
        private List<IFormFile> _modeles;


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

        public List<IFormFile> Modeles
        {
            get { return _modeles; }
            set { _modeles = value; }
        }

        public FormulaireSurMesureData(string nom, string prenom, string email, string matiere, string type, string description, List<IFormFile> modeles)
        {
            Nom = nom;
            Prenom = prenom;
            Email = email;
            Matiere = matiere;
            Type = type;
            Description = description;
            Modeles = modeles;
        }
        /// <summary>
        /// Constructeur utilisé par le binder
        /// </summary>
        public FormulaireSurMesureData() { }
    }

}
