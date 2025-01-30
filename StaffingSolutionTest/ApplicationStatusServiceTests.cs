using Microsoft.EntityFrameworkCore;
using StaffingSolution.Data;
using StaffingSolution.Services;
using StaffingSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffingSolutionTest
{
    public class ApplicationStatusServiceTests
    {
        private readonly AppDbContext _context;
        private readonly ApplicationStatusService _service;

        public ApplicationStatusServiceTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
               .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
               .Options;

            _context = new AppDbContext(options);
            _service = new ApplicationStatusService(_context);

            _context.Applications.AddRange(new List<Application>
            {
                new Application
                {
                    Id = 1,
                    UserId = "user1",
                    Status = "Pending",
                    CreatedAt = DateTime.UtcNow.AddDays(-31) 
                },
                new Application
                {
                    Id = 2,
                    UserId = "user2",
                    Status = "Pending",
                    CreatedAt = DateTime.UtcNow.AddDays(-29) 
                },
                new Application
                {
                    Id = 3,
                    UserId = "user3",
                    Status = "Approved",
                    CreatedAt = DateTime.UtcNow.AddDays(-40) 
                }
            });
            _context.SaveChanges();
        }

        [Fact]
        public void MarkExpiredApplications_ShouldUpdateOldPendingApplications()
        {
            _service.MarkExpiredApplications();

            var expiredApp = _context.Applications.FirstOrDefault(a => a.Id == 1);
            var pendingApp = _context.Applications.FirstOrDefault(a => a.Id == 2);
            var approvedApp = _context.Applications.FirstOrDefault(a => a.Id == 3);

            Assert.NotNull(expiredApp);
            Assert.Equal("Expired", expiredApp.Status);

            Assert.NotNull(pendingApp);
            Assert.Equal("Pending", pendingApp.Status);

            Assert.NotNull(approvedApp);
            Assert.Equal("Approved", approvedApp.Status);
        }
    }
}
