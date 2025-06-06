﻿using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using StaffingSolution.Interfaces;

namespace StaffingSolution.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _smtpServer = "smtp.gmail.com";
        private readonly int _smtpPort = 587;
        private readonly string _smtpUsername = "raminabbassi88@gmail.com";
        private readonly string _smtpPassword = "ttad iixt sudt emfk";
        private readonly ILogger<EmailService> _logger;

        public EmailService(ILogger<EmailService> logger)
        {
            _logger = logger;
        }

        public virtual async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                _logger.LogWarning(" Försök att skicka mejl utan e-postadress.");
                throw new ArgumentException("E-postadress får inte vara tom.", nameof(email));
            }

            _logger.LogInformation(" Förbereder att skicka mejl till: {Email}", email);

            try
            {
                using (var client = new SmtpClient(_smtpServer, _smtpPort))
                {
                    client.Credentials = new NetworkCredential(_smtpUsername, _smtpPassword);
                    client.EnableSsl = true;

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(_smtpUsername),
                        Subject = subject,
                        Body = htmlMessage,
                        IsBodyHtml = true
                    };

                    mailMessage.To.Add(email);

                    await client.SendMailAsync(mailMessage);
                    _logger.LogInformation(" Mejlet har skickats till: {Email}", email);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(" Fel vid skickning av mejl till: {Email}. Felmeddelande: {Exception}", email, ex);
                throw;
            }
        }

    }
}
