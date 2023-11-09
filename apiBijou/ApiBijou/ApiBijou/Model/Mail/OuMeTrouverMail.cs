using System.Net.Mail;
using System.Text;

namespace ApiBijou.Model.Mail
{
    /// <summary>
    /// Classe pour générer mail ou me trouver
    /// </summary>
    public class OuMeTrouverMail
    {
        private readonly StringBuilder body;
        private readonly IMailSender mailSender = new GoogleSmtp();

        /// <summary>
        /// Constructure SurMesureMail
        /// </summary>
        /// <param name="formulaireOuMeTrouverData">Données à insérer</param>
        public OuMeTrouverMail(FormulaireOuMeTrouverData formulaireOuMeTrouverData)
        {
            body = new StringBuilder();
            GenerateBody(formulaireOuMeTrouverData);
            mailSender.SendMail("mateobigearddasen21@gmail.com", formulaireOuMeTrouverData.Email, "Question d'un client", body.ToString(), new List<Attachment>());
        }
        /// <summary>
        /// Génére le body du mail sur mesure
        /// </summary>
        /// <param name="formulaireOuMeTrouverData">Formulaire Ou me trouver Data</param>
        public void GenerateBody(FormulaireOuMeTrouverData formulaireOuMeTrouverData)
        {
            body.AppendLine($"Email du client: {formulaireOuMeTrouverData.Email}");
            body.AppendLine($"Question du client: {formulaireOuMeTrouverData.Message}");            
        }

    }
}
