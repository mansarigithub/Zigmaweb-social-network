using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace ZigmaWeb.Common.Messaging
{
    public static class EmailHelper
    {
        static SmtpClient smtpClient;

        static EmailHelper()
        {
            var smtpHost = ConfigurationManager.AppSettings["SmtpServerHostName"];
            var smtpPort = int.Parse(ConfigurationManager.AppSettings["SmtpServerPort"]);
            var smtpUsername = ConfigurationManager.AppSettings["SmtpUsername"];
            var smtpPassword = ConfigurationManager.AppSettings["SmtpPassword"];
            var networkCredential = new NetworkCredential(smtpUsername, smtpPassword);
            smtpClient = new SmtpClient(smtpHost, smtpPort);
            smtpClient.Credentials = networkCredential;
        }

        public static void SendMail(string from, string to, string subject, string body,string receiverDisplayName)
        {
            var fromMailAddress = new MailAddress(from, "Zigmaweb");
            var toMailAddress = new MailAddress(to, receiverDisplayName, Encoding.UTF8);

            using (var mailMessage = new MailMessage(fromMailAddress,toMailAddress)) {
                mailMessage.SubjectEncoding = mailMessage.BodyEncoding = Encoding.UTF8;
                mailMessage.IsBodyHtml = true;
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                smtpClient.Send(mailMessage);
            }
        }

        public static Task SendMailAsync(string from, string to, string subject, string body)
        {
            return Task.Factory.StartNew(() => {
                SendMail(from, to, subject, body,string.Empty);
            });
        }
    }
}