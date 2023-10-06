using System.Net;
using System.Net.Mail;
using System.Text;

namespace ApiBijou.Model.SurMesure
{
    /// <summary>
    /// Cette classe a pour but d'envoyer un mail
    /// </summary>
    public class MailBuilder
    {
        /// <summary>
        /// Body du mail
        /// </summary>
        private readonly StringBuilder body;

        /// <summary>
        /// Constructeur pour faire un formulaire Sur mesure
        /// </summary>
        /// <param name="formulaireSurMesureModel"></param>
        public MailBuilder(FormulaireSurMesureModel formulaireSurMesureModel)
        {
            body = new StringBuilder();
            this.GenerateBody(formulaireSurMesureModel);
            this.SendMail();
        }

        /// <summary>
        /// Constructeur pour faire un formulaire ou me trouver
        /// </summary>
        /// <param name="FormulaireOuMeTrouver"></param>
        public MailBuilder(FormulaireOuMeTrouver formulaireOuMeTrouver)
        {
            body = new StringBuilder();
            this.GenerateBody(formulaireOuMeTrouver);
            this.SendMail();
        }

        /// <summary>
        /// Ecrit le corps
        /// </summary>
        /// <param name="formulaireSurMesureModel">Champs entré par le client</param>
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


        public void GenerateBody(FormulaireOuMeTrouver formulaireOuMeTrouver)
        {
            body.AppendLine($"Nom du client: {formulaireOuMeTrouver.Email}");
            body.AppendLine($"Prénom du client: {formulaireOuMeTrouver.Message}");
        }


        /// <summary>
        /// Envoie le mail à la cliente
        /// </summary>
        public void SendMail()
        {
            ///Configuration des destinataires 
            string senderEmail = "*******@icloud.com";
            string recipientEmail = "******bd@email.com";
            string subject = "Nouvelle demande de bijou sur mesure";
            
            ///Configuration du client smtp
            SmtpClient smtpClient = new SmtpClient("smtp.mailgun.org");
            smtpClient.Port = 587;
            smtpClient.Credentials = new NetworkCredential("postmaster@sandbox9477fb58ccf44d78983fd0bb0ce963f0.mailgun.org", "66b85866c1621ae3ec617e8ad84d0798-77316142-94ccf2c3");
            smtpClient.EnableSsl = true;

            ///Configuration du messae
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(senderEmail);
            mailMessage.To.Add(recipientEmail);
            mailMessage.Subject = subject;
            mailMessage.Body = body.ToString(); // Utilisez body.ToString() pour obtenir la chaîne de texte complète.

            smtpClient.Send(mailMessage);
        }
    }
}
