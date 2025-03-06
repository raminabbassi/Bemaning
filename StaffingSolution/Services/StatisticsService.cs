using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StaffingSolution.Data;

namespace StaffingSolution.Services
{
    public class StatisticsService
    {
        private readonly AppDbContext _context;

        public StatisticsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetTotalJobApplicationsAsync()
        {
            return await _context.JobApplications.CountAsync();
        }

        public async Task<int> GetTotalUsersAsync()
        {
            return await _context.Users.CountAsync();
        }

        public async Task<int> GetTotalMessagesAsync()
        {
            return await _context.ContactMessages.CountAsync();
        }
    }
}
