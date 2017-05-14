using MailKit.Net.Smtp;
using MimeKit;
using System.IO;
using System.Threading.Tasks;

namespace MiA_projekt.Services
{
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            #if DEBUG
            if (!Directory.Exists(@"C:\emails"))
                Directory.CreateDirectory(@"C:\emails");
            File.WriteAllText(@"C:\emails\" + email + ".txt", message);
            return;
            #endif

            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Apartments", "noreply762912@gmail.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("plain") { Text = message };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587);
                await client.AuthenticateAsync("noreply762912@gmail.com", "grzesio12345");
                await client.SendAsync(emailMessage).ConfigureAwait(false);
                await client.DisconnectAsync(true).ConfigureAwait(false);
            }
        }

        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
}
