using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using ApiBijou.Model.formModel;

namespace ApiBijou.Model.Mail
{
    public class SurMesureMail
    {
        private readonly StringBuilder body;
        private readonly IMailSender mailSender = new GoogleSmtp();

        public SurMesureMail(FormulaireSurMesureModel formulaireSurMesureModel)
        {
            body = new StringBuilder();
            GenerateBody(formulaireSurMesureModel);

            List<Attachment> attachments = GenerateAttachments(formulaireSurMesureModel.Modeles);

            mailSender.SendMail("mateobigearddasen21@gmail.com", formulaireSurMesureModel.Email, "Demande de bijoux Sur Mesure", body.ToString(), attachments);
        }

        public void GenerateBody(FormulaireSurMesureModel formulaireSurMesureModel)
        {
            body.AppendLine($"Nom du client: {formulaireSurMesureModel.Nom}");
            body.AppendLine($"Prénom du client: {formulaireSurMesureModel.Prenom}");
            body.AppendLine($"E-mail du client: {formulaireSurMesureModel.Email}");
            body.AppendLine($"Matière souhaitée: {formulaireSurMesureModel.Matiere}");
            body.AppendLine($"Type: {formulaireSurMesureModel.Type}");
            body.AppendLine($"Description: {formulaireSurMesureModel.Description}");
            body.AppendLine("Pièces jointes:");
        }

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
