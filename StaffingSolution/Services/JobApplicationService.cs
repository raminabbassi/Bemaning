using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StaffingSolution.Data;
using StaffingSolution.Interfaces;
using StaffingSolution.Models;
using StaffingSolution.Factories; 

namespace StaffingSolution.Services
{
    public class JobApplicationService : IJobApplicationService
    {
        private readonly AppDbContext _context;

        public JobApplicationService(AppDbContext context)
        {
            _context = context;
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

        }

        public async Task UpdateJobApplicationStatus(int applicationId, string status)
        {
            var application = await _context.JobApplications.FindAsync(applicationId);
            if (application != null)
            {
                application.Status = status;
                await _context.SaveChangesAsync();

            }
        }
        public bool CanApplyForJob(string userId)
        {
            int applicationsCount = _context.Applications.Count(a => a.UserId == userId);
            return applicationsCount < 3;
        }
    }
}
