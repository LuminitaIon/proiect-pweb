using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MailSender
{
    public class MailProcessor
    {
        private MailAddress _mailAddress { get; set; }
        private SmtpClient _smtp { get; set; }

        public MailProcessor(string smtpUsername, string smtpPassword, string smtpClient, int smtpPort)
        {
            _mailAddress = new MailAddress(smtpUsername, "GoSaveMe");

            _smtp = new SmtpClient
            {
                Host = smtpClient,
                Port = smtpPort,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(smtpUsername, smtpPassword)
            };
        }

        private void SendEmail(string toEmail, string subject, string body)
        {
            using var message = new MailMessage(_mailAddress, new MailAddress(toEmail, toEmail));
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            _smtp.Send(message);
        }

        public void SendSignUpEmail(string toEmail)
        {
            string body = "<p>Welcome to GoSaveMe!</p>";
            body += "<p>Get your news in real time from war-affected countries</p>";

            SendEmail(toEmail, "Welcome to GoSaveMe!", body);
        }

        public void SendProfileUpdatedEmail(string toEmail)
        {
            string body = "<p>Your profile has been updated!</p>";
            body += "<p>Visit the website now for seeing the latest news</p>";

            SendEmail(toEmail, "Profile updated!", body);
        }

        public void SendNewsPostedEmail(string toEmail)
        {
            string body = "<p>Your news has been posted!</p>";
            body += "<p>Thank you for sharing your knowledge with others!</p>";

            SendEmail(toEmail, "News posted!", body);
        }

        public void SendNewsWaitingForApprovalEmail(string toEmail)
        {
            string body = "<p>Your news has been queued for approval!</p>";
            body += "<p>As soon as it will be accepted, everyone will know about it.</p>";

            SendEmail(toEmail, "News submitted for approval!", body);
        }
    }
}
