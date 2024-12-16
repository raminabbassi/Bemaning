using StaffingSolution.Models;
using System;

namespace StaffingSolution.Factories
{
    public static class JobApplicationFactory
    {
        public static JobApplication Create(string title, string company, string userEmail, string status = "Pending")
        {
            return new JobApplication
            {
                Title = title,
                Company = company,
                UserEmail = userEmail,
                Status = status,
                AppliedDate = DateTime.UtcNow
            };
        }
    }
}
