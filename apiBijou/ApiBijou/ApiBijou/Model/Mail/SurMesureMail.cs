using System.Net;
using System.Net.Mail;
using System.Text;
using ApiBijou.Model.formModel;

namespace ApiBijou.Model.Mail
{
    /// <summary>
    /// Cette classe a pour but d'envoyer un mail
    /// </summary>
    public class SurMesureMail
    {
        /// <summary>
        /// Body du mail
        /// </summary>
        private readonly StringBuilder body;

        private readonly IMailSender mailSender = new GoogleSmtp();

        /// <summary>
        /// Constructeur pour créer un e-mail à partir d'un modèle de formulaire "Où me trouver".
        /// </summary>
        /// <param name="formulaireOuMeTrouver">Le modèle de formulaire "Où me trouver".</param>
        /// <returns>Une instance de la classe <see cref="SurMesureMail"/>.</returns>
        public SurMesureMail(FormulaireSurMesureModel formulaireSurMesureModel)
        {
            body = new StringBuilder();
            GenerateBody(formulaireSurMesureModel);
            mailSender.SendMail("mateobigearddasen21@gmail.com", formulaireSurMesureModel.Email, "Demande de bijoux Sur Mesure", body.ToString());
            Console.WriteLine(body.ToString());
        }

        /// <summary>
        /// Constructeur pour faire un formulaire ou me trouver
        /// </summary>
        //public MailBuilder(FormulaireOuMeTrouver formulaireOuMeTrouver)
        //{
        //    body = new StringBuilder();
        //    GenerateBody(formulaireOuMeTrouver);
        //    SendMail();
        //}

        /// <summary>
        /// Génère le corps de l'e-mail à partir des données du formulaire "Où me trouver".
        /// </summary>
        /// <param name="formulaireOuMeTrouver">Les champs saisis par le client.</param>
        /// <returns>Le corps de l'e-mail sous forme de chaîne de caractères.</returns>
        public void GenerateBody(FormulaireSurMesureModel formulaireSurMesureModel)
        {
            body.AppendLine($"Nom du client: {formulaireSurMesureModel.Nom}");
            body.AppendLine($"Prénom du client: {formulaireSurMesureModel.Prenom}");
            body.AppendLine($"E-mail du client: {formulaireSurMesureModel.Email}");
            body.AppendLine($"Matière souhaité: {formulaireSurMesureModel.Matiere}");
            body.AppendLine($"Type: {formulaireSurMesureModel.Type}");
            body.AppendLine($"Description: {formulaireSurMesureModel.Description}");
            body.AppendLine("Pièces jointes:");
        }


        /// <summary>
        /// Génère le corps de l'e-mail à partir des données du formulaire "Où me trouver".
        /// </summary>
        /// <param name="formulaireOuMeTrouver">Les champs saisis par le client.</param>
        /// <returns>Le corps de l'e-mail sous forme de chaîne de caractères.</returns>
        //public void GenerateBody(FormulaireOuMeTrouver formulaireOuMeTrouver)
        //{
        //    body.AppendLine($"Nom du client: {formulaireOuMeTrouver.Email}");
        //    body.AppendLine($"Prénom du client: {formulaireOuMeTrouver.Message}");
        //}


        
    }
}
