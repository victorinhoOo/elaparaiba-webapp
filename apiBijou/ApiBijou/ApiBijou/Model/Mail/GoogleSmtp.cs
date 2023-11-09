using System.Net.Mail;
using System.Net;

namespace ApiBijou.Model.Mail
{
    public class GoogleSmtp : IMailSender
    {
        public void SendMail(string senderMail, string recipientMail, string subject, string body)
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential(senderMail, "gczr ylng xxkg bjtv"),
                    EnableSsl = true
                };
                client.Send(senderMail, recipientMail, subject, body);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
        }
    }
}
