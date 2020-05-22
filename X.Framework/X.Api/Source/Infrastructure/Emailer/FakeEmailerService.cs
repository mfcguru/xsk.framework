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
                    MailMessage msg = new MailMessage(from, to, subject,
                       body);
                    client.UseDefaultCredentials = true;
                    client.DeliveryMethod =
                       SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    client.PickupDirectoryLocation = emailDir;
                    try
                    {
                        client.Send(msg);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exception caught: {0}",
                           ex.ToString());
                        Console.ReadLine();
                        System.Environment.Exit(-1);
                    }
                }

                var defaultMsgPath = new
                   DirectoryInfo(emailDir).GetFiles()
                      .OrderByDescending(f => f.LastWriteTime)
                      .First();
                var realMsgPath = Path.Combine(emailDir, msgName);
                try
                {
                    File.Move(defaultMsgPath.FullName, realMsgPath);
                    Console.WriteLine("Message saved.");
                }
                catch (System.IO.IOException)
                {
                    Console.WriteLine("File already exists. Overwrite it ? Y / N");    

                    var test = Console.ReadLine();
                    if (test == "y" || test == "Y")
                    {
                        Console.WriteLine("Overwriting existing file...");
                        File.Delete(realMsgPath);
                        File.Move(defaultMsgPath.FullName, realMsgPath);
                        Console.WriteLine("Message saved.");
                    }
                    else
                    {
                        Console.WriteLine("Exiting Program without saving file.");
                    }
                }
                Console.WriteLine("Press any key to exit.");
                Console.ReadLine();
            });
        }
	}
}
