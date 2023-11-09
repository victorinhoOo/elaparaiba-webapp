using System.Net.Mail;

namespace ApiBijou.Model.Mail
{
    public interface IMailSender
    {
        /// <summary>
        /// Envoi un mail
        /// </summary>
        /// <param name="senderMail">mail de l'envoyeur</param>
        /// <param name="recipientMail">mail du receveur</param>
        /// <param name="subject">subject du mail</param>
        /// <param name="body">body du mail</param>
        public void SendMail(string senderMail, string recipientMail, string subject, string body, List<Attachment> attachments);
    }
}
