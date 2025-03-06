using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using StaffingSolution.Models;
using StaffingSolution.Repositories;
using StaffingSolution.Services;

namespace StaffingSolution.Services
{
    public class ContactService
    {
        private readonly ContactRepository _contactRepository;
        private readonly EmailService _emailService;
        private readonly IConfiguration _configuration;

        public ContactService(ContactRepository contactRepository, EmailService emailService, IConfiguration configuration)
        {
            _contactRepository = contactRepository;
            _emailService = emailService;
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

            var recipientEmail = _configuration["EmailSettings:RecipientEmail"] ?? "extendly@info.com";

            try
            {
                await _emailService.SendEmailAsync(
                    recipientEmail,
                    "Nytt kontaktmeddelande",
                    $"Namn: {name}<br>E-post: {email}<br><br>Meddelande:<br>{message}"
                );

                return "Meddelandet har skickats och sparats!";
            }
            catch (Exception ex)
            {
                Console.WriteLine($" E-postfel: {ex.Message}");
                return "Meddelandet sparades, men e-post skickades ej.";
            }
        }
    }
}
