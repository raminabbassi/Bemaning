﻿using System.Collections.Generic;
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
            var ApplicationModel = new JobApplication
            {
                Title = title,
                Company = company,
                UserEmail = userEmail,
                AppliedDate = DateTime.Now,
                Status = "Mottagen"
            };

            _context.JobApplications.Add(ApplicationModel);
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
        public async Task MarkExpiredApplicationsAsync()
        {
            var expiredApps = await _context.JobApplications
                .Where(a => a.Status == "Pending" && a.AppliedDate < DateTime.UtcNow.AddDays(-30))
                .ToListAsync();

            foreach (var app in expiredApps)
            {
                app.Status = "Expired";
            }

            await _context.SaveChangesAsync();
        }
    }
}
