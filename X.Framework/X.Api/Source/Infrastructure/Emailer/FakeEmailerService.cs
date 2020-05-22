using System;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace X.Api.Source.Infrastructure.Messaging
{
	public class FakeEmailerService : IEmailerService
	{
		public async Task Send(int teamId, string recipient) 
		{
            await Task.Run(() =>
            {
                string to = recipient;
                string from = "support@xsk.com";
                string subject = "Invitation to join XSK";
                string body = string.Format("http://localhost:8080/team/{0}/confirmInvite/{1}", teamId, recipient);
                string emailDir = "c://temp/";
                string msgName = Guid.NewGuid() + ".eml";

                Console.WriteLine("Saving e-mail...");
                using (var client = new SmtpClient())
                {
                    MailMessage msg = new MailMessage(from, to, subject, body);
                    client.UseDefaultCredentials = true;
                    client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    client.PickupDirectoryLocation = emailDir;
                    client.Send(msg);
                }

                var defaultMsgPath = new DirectoryInfo(emailDir)
                    .GetFiles()
                    .OrderByDescending(f => f.LastWriteTime)
                    .First();

                var realMsgPath = Path.Combine(emailDir, msgName);
                File.Move(defaultMsgPath.FullName, realMsgPath);
            });
        }
	}
}
