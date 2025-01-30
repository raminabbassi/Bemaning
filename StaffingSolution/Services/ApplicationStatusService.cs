using StaffingSolution.Data;
using System;
using System.Linq;
using StaffingSolution.Models;

namespace StaffingSolution.Services
{
    public class ApplicationStatusService
    {
        private readonly AppDbContext _context;

        public ApplicationStatusService(AppDbContext context)
        {
            _context = context;
        }

        public void MarkExpiredApplications()
        {
            var expiredApps = _context.Applications
                .Where(a => a.Status == "Pending" && a.CreatedAt < DateTime.UtcNow.AddDays(-30))
                .ToList();

            foreach (var app in expiredApps)
            {
                app.Status = "Expired";
            }

            _context.SaveChanges();
        }
    }
}
