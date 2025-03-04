using StaffingSolution.Data;
using StaffingSolution.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace StaffingSolution.Repositories
{
    public class InterviewRepository
    {
        private readonly AppDbContext _context;

        public InterviewRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddInterview(InterviewRequest request)
        {
            _context.InterviewRequests.Add(request);
            await _context.SaveChangesAsync();
        }

        public async Task<List<InterviewRequest>> GetAllInterviews()
        {
            return await _context.InterviewRequests.ToListAsync();
        }

    }

}
