using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using StaffingSolution.Models;
using StaffingSolution.Repositories;

namespace StaffingSolution.Services
{
    public class ContactService
    {
        private readonly ContactRepository _contactRepository;
        private readonly IConfiguration _configuration;

        public ContactService(ContactRepository contactRepository, IConfiguration configuration)
        {
            _contactRepository = contactRepository;
            _configuration = configuration;
        }

        public async Task<string> SaveAndSendEmailAsync(string name, string email, string message)
        {
            var contactMessage = new ContactMessage
            {
                Name = name,
                Email = email,
                Message = message
            };

            await _contactRepository.AddMessageAsync(contactMessage);

            bool emailSent = SendEmail(name, email, message);
            return emailSent ? "Meddelandet har skickats och sparats!" : "Meddelandet sparades, men e-post skickades ej.";
        }

        private bool SendEmail(string name, string senderEmail, string message)
        {
            try
            {
                var smtpServer = _configuration["EmailSettings:SmtpServer"];
                var smtpPortString = _configuration["EmailSettings:SmtpPort"];
                var smtpUsername = _configuration["EmailSettings:SmtpUsername"];
                var smtpPassword = _configuration["EmailSettings:SmtpPassword"];
                var recipientAddress = _configuration["EmailSettings:RecipientEmail"];

                if (string.IsNullOrEmpty(smtpServer) || string.IsNullOrEmpty(smtpPortString) ||
                    string.IsNullOrEmpty(smtpUsername) || string.IsNullOrEmpty(smtpPassword) ||
                    string.IsNullOrEmpty(recipientAddress))
                {
                    return false;
                }

                if (!int.TryParse(smtpPortString, out int smtpPort))
                {
                    return false;
                }

                using (var smtpClient = new SmtpClient(smtpServer, smtpPort))
                {
                    smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                    smtpClient.EnableSsl = true;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.UseDefaultCredentials = false;

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(smtpUsername), 
                        Subject = "Nytt kontaktmeddelande",
                        Body = $"Namn: {name}\nE-post: {senderEmail}\nMeddelande:\n{message}",
                        IsBodyHtml = false
                    };

                    mailMessage.To.Add(recipientAddress);
                    smtpClient.Send(mailMessage);

                    Console.WriteLine("✅ E-post skickades framgångsrikt!");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ E-postfel: {ex.Message}");
                return false;
            }
        }


    }

}

