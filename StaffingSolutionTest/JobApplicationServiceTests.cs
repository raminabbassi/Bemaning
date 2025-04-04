using Microsoft.EntityFrameworkCore;
using StaffingSolution.Data;
using StaffingSolution.Models;
using StaffingSolution.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace StaffingSolution.Tests
{
    public class JobApplicationServiceTests
    {
        private readonly AppDbContext _context;
        private readonly JobApplicationService _service;

        public JobApplicationServiceTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
               .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
               .Options;

            _context = new AppDbContext(options);
            _service = new JobApplicationService(_context);

            _context.JobApplications.AddRange(new List<JobApplication>
            {
                new JobApplication
                {
                    Id = 1,
                    UserEmail = "user1@example.com",
                    Title = "Developer",
                    Company = "Tech Corp",
                    Status = "Pending",
                    AppliedDate = System.DateTime.UtcNow.AddDays(-31)
                },
                new JobApplication
                {
                    Id = 2,
                    UserEmail = "user2@example.com",
                    Title = "Tester",
                    Company = "Quality Inc",
                    Status = "Pending",
                    AppliedDate = System.DateTime.UtcNow.AddDays(-10)
                },
                new JobApplication
                {
                    Id = 3,
                    UserEmail = "user3@example.com",
                    Title = "Designer",
                    Company = "Creative AB",
                    Status = "Approved",
                    AppliedDate = System.DateTime.UtcNow.AddDays(-40)
                }
            });

            _context.SaveChanges();
        }

        [Fact]
        public async Task GetJobsAppliedByUser_ShouldReturnJobsForSpecificUser()
        {
            var result = await _service.GetJobsAppliedByUser("user1@example.com");

            Assert.Single(result);
            Assert.Equal("Developer", result.First().Title);
            Assert.Equal("Tech Corp", result.First().Company);
        }

        [Fact]
        public async Task AddJobApplication_ShouldAddNewApplication()
        {
            var title = "Manager";
            var company = "Leadership Ltd";
            var userEmail = "user4@example.com";

            await _service.AddJobApplication(title, company, userEmail);

            var addedApplication = _context.JobApplications.FirstOrDefault(j => j.Title == "Manager");
            Assert.NotNull(addedApplication);
            Assert.Equal("Leadership Ltd", addedApplication.Company);
        }

        [Fact]
        public async Task UpdateJobApplicationStatus_ShouldUpdateStatus()
        {
            await _service.UpdateJobApplicationStatus(1, "Approved");

            var updatedApplication = _context.JobApplications.FirstOrDefault(j => j.Id == 1);
            Assert.NotNull(updatedApplication);
            Assert.Equal("Approved", updatedApplication.Status);
        }

        [Fact]
        public async Task MarkExpiredApplicationsAsync_ShouldSetOldPendingToExpired()
        {
            await _service.MarkExpiredApplicationsAsync();

            var app1 = await _context.JobApplications.FindAsync(1);
            var app2 = await _context.JobApplications.FindAsync(2);
            var app3 = await _context.JobApplications.FindAsync(3);

            Assert.Equal("Expired", app1.Status); 
            Assert.Equal("Pending", app2.Status); 
            Assert.Equal("Approved", app3.Status); 
        }
    }
}
