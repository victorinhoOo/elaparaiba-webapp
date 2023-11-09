using System;
using System.Net;
using System.Net.Mail;
using System.Collections.Generic;

namespace ApiBijou.Model.Mail
{
    public class GoogleSmtp : IMailSender
    {
        public void SendMail(string senderMail, string recipientMail, string subject, string body, List<Attachment> attachments)
        {
            try
            {
                MailMessage mail = new MailMessage(senderMail, recipientMail, subject, body);
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential(senderMail, "gczr ylng xxkg bjtv"),
                    EnableSsl = true
                };

                foreach (Attachment attachment in attachments)
                {
                    mail.Attachments.Add(attachment);
                }

                client.Send(mail);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
