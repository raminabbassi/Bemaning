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

            var recipientEmail = email;
            try
            {
                Console.WriteLine($" Försöker skicka mejl till: {recipientEmail}");

                await _emailService.SendEmailAsync(
                    recipientEmail,
                    "Tack för ditt meddelande!",
                    $"Hej {name},<br><br> Vi har mottagit ditt meddelande och återkommer så snart vi kan.<br><br> Hälsningar, Extendly"
                );

                Console.WriteLine(" Mejl skickades framgångsrikt!");
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
