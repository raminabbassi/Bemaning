using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using StaffingSolution.Models;
using StaffingSolution.Data;
using StaffingSolution.Services;

namespace StaffingSolution.Repositories
{
    public class InterviewRepository
    {
        private readonly AppDbContext _context;
        private readonly EmailService _emailService;
        private readonly ILogger<InterviewRepository> _logger;

        public InterviewRepository(AppDbContext context, EmailService emailService, ILogger<InterviewRepository> logger)
        {
            _context = context;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task AddInterview(InterviewRequest request)
        {
            try
            {
                _context.InterviewRequests.Add(request);
                await _context.SaveChangesAsync();

                _logger.LogInformation(" Intervju bokad för: {Email}", request.Email);

                // Anropa EmailService för att skicka mejl
                await _emailService.SendEmailAsync(
                    request.Email,
                    "Påminnelse: Bokat samtal",
                    $"Hej {request.FullName},<br><br> Du har bokat ett samtal angående {request.JobTitle}.<br><br> Vi hörs snart!"
                );
            }
            catch (Exception ex)
            {
                _logger.LogError("⚠️ Fel vid bokning av intervju: {Exception}", ex);
                throw;
            }
        }
    }
}
