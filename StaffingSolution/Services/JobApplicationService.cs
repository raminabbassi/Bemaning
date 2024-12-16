using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StaffingSolution.Data;
using StaffingSolution.Interfaces;
using StaffingSolution.Models;
using StaffingSolution.Factories; 
using StaffingSolution.Observers;

namespace StaffingSolution.Services
{
    public class JobApplicationService : IJobApplicationService
    {
        private readonly AppDbContext _context;
        private readonly Notifier _notifier; 

        public JobApplicationService(AppDbContext context, Notifier notifier)
        {
            _context = context;
            _notifier = notifier;
        }

        public async Task<List<JobApplication>> GetJobsAppliedByUser(string userEmail)
        {
            return await _context.JobApplications
                .Where(j => j.UserEmail == userEmail)
                .ToListAsync();
        }

        public async Task AddJobApplication(string title, string company, string userEmail)
        {
            var newApplication = JobApplicationFactory.Create(title, company, userEmail);

            _context.JobApplications.Add(newApplication);
            await _context.SaveChangesAsync();

            _notifier.Notify($"New job application for {title} at {company} was created by {userEmail}.");
        }

        public async Task UpdateJobApplicationStatus(int applicationId, string status)
        {
            var application = await _context.JobApplications.FindAsync(applicationId);
            if (application != null)
            {
                application.Status = status;
                await _context.SaveChangesAsync();

                _notifier.Notify($"Job application with ID {applicationId} status updated to '{status}'.");
            }
        }
    }
}
