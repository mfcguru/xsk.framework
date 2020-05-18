using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace X.Api.Source.Infrastructure
{
    public class EmailMessagingService : IMessagingService
    {
        private readonly AppSettings appSettings;

        public EmailMessagingService(IConfiguration configuration)
        {
            var appSettingsSection = configuration.GetSection("AppSettings");
            appSettings = appSettingsSection.Get<AppSettings>();
        }

        public void SendEmailInvitation(string receiverEmail)
        {
            try
            {
                var smtpUsername = appSettings.SmtpUsername;
                var smtpPassword = appSettings.SmtpPassword;
                var host = appSettings.SmtpHost;
                var port = appSettings.SmtpPort;
                var enableSsl = appSettings.StmpEnableSsl;

                var client = new SmtpClient(host, port)
                {
                    Credentials = new NetworkCredential(smtpUsername, smtpPassword),
                    EnableSsl = enableSsl
                };
                
                string emailTemplateFilePath = @"\EmailTemplates\MemberInvitation\MemberInvitation.html"; 
                StreamReader str = new StreamReader($"{Directory.GetCurrentDirectory()}{emailTemplateFilePath}");
                string mailText = str.ReadToEnd();
                str.Close();

                var subject = "Team Member Invitation";
                mailText = mailText.Replace("[subject]", subject);
                var username = receiverEmail.Split(new[] { "@" }, StringSplitOptions.RemoveEmptyEntries)[0];
                mailText = mailText.Replace("[username]", username);
                mailText = mailText.Replace("[registration-profile-url]", appSettings.RegistrationPageUrl);
                mailText = mailText.Replace("[contactsupport-email]", appSettings.ContactSupportEmail);
                mailText = mailText.Replace("[facebook-profile-url]", appSettings.FacebookProfileUrl);
                mailText = mailText.Replace("[twitter-profile-url]", appSettings.TwitterProfileUrl);
                mailText = mailText.Replace("[instagram-profile-url]", appSettings.InstagramProfileUrl);
                mailText = mailText.Replace("[github-profile-url]", appSettings.GithubProfileUrl);

                var senderEmail = $"noreply@{appSettings.DomainName}";
                var emailDisplayName = $"No Reply - {subject}";

                MailAddress from = new MailAddress(senderEmail,
                   emailDisplayName,
                   System.Text.Encoding.UTF8);
                MailAddress to = new MailAddress(receiverEmail);
                MailMessage mailMessage = new MailMessage(from, to);
                mailMessage.Body = mailText;
                mailMessage.IsBodyHtml = true;
                mailMessage.Body += Environment.NewLine;
                mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
                mailMessage.Subject = subject;
                mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;

                client.Send(mailMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in Send(): {0}",
                    ex.ToString());
            }
        }

        public void Send(List<string> recipients) { }
    }
}
