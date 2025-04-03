using System.Threading.Tasks;
using StaffingSolution.Models;
using StaffingSolution.Data;
using Microsoft.EntityFrameworkCore;

namespace StaffingSolution.Repositories
{
    public class ContactRepository
    {
        private readonly AppDbContext _context;

        public ContactRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddMessageAsync(ContactMessage message)  
        {
            _context.ContactMessages.Add(message);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ContactMessage>> GetAllMessagesAsync()
        {
            return await _context.ContactMessages.ToListAsync();
        }
        public async Task MarkMessageAsReadAsync(int messageId)
        {
            var message = await _context.ContactMessages.FindAsync(messageId);
            if (message != null)
            {
                message.IsRead = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteMessageAsync(int messageId)
        {
            var message = await _context.ContactMessages.FindAsync(messageId);
            if (message != null)
            {
                _context.ContactMessages.Remove(message);
                await _context.SaveChangesAsync();
            }
        }

    }
}
