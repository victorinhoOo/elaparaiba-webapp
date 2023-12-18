using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using ApiBijou.Model.formModel;

namespace ApiBijou.Model.Mail
{
    /// <summary>
    /// Classe pour générer mail Sur Mesure
    /// </summary>
    public class SurMesureMail
    {
        private readonly StringBuilder body;
        private readonly IMailSender mailSender = new GoogleSmtp();

        /// <summary>
        /// Constructure SurMesureMail
        /// </summary>
        /// <param name="formulaireSurMesureModel">Données à insérer</param>
        public SurMesureMail(FormulaireSurMesureData formulaireSurMesureModel)
        {
            body = new StringBuilder();
            GenerateBody(formulaireSurMesureModel);

            List<Attachment> attachments = GenerateAttachments(formulaireSurMesureModel.Modeles);

            mailSender.SendMail("mateobigearddasen21@gmail.com", "elaparaiba@outlook.com", "Demande de bijoux Sur Mesure", body.ToString(), attachments);
        }
        /// <summary>
        /// Génére le body du mail sur mesure
        /// </summary>
        /// <param name="formulaireSurMesureModel"></param>
        public void GenerateBody(FormulaireSurMesureData formulaireSurMesureModel)
        {
            body.AppendLine($"Nom du client: {formulaireSurMesureModel.Nom}");
            body.AppendLine($"Prénom du client: {formulaireSurMesureModel.Prenom}");
            body.AppendLine($"E-mail du client: {formulaireSurMesureModel.Email}");
            body.AppendLine($"Matière souhaitée: {formulaireSurMesureModel.Matiere}");
            body.AppendLine($"Type: {formulaireSurMesureModel.Type}");
            body.AppendLine($"Description: {formulaireSurMesureModel.Description}");
        }

        /// <summary>
        /// Génére la liste des attachements du mail
        /// </summary>
        /// <param name="photos">Photos à envoyer</param>
        /// <returns></returns>
        public List<Attachment> GenerateAttachments(List<IFormFile> photos)
        {
            List<Attachment> attachments = new List<Attachment>();

            foreach (var file in photos)
            {
                if (file.Length > 0)
                {
                    try
                    {
                        var attachment = new Attachment(file.OpenReadStream(), file.FileName);
                        attachments.Add(attachment);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erreur lors de la création de la pièce jointe : {ex.Message}");
                        // Gérer l'erreur ici, si nécessaire
                    }
                }
            }

            return attachments;
        }
    }
}
