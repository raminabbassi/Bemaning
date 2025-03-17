using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StaffingSolution.Data;
using StaffingSolution.Models;

namespace StaffingSolution.Services
{
    public class BookingService
    {
        private readonly AppDbContext _context;

        public BookingService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> BookTimeAsync(int scheduleId, string userEmail)
        {
            var schedule = await _context.AdminSchedules.FindAsync(scheduleId);

            if (schedule == null || schedule.IsBooked)
                return false; 

            var booking = new Booking
            {
                ScheduleId = scheduleId,
                UserEmail = userEmail,
                BookedTime = schedule.StartTime
            };

            schedule.IsBooked = true;
            schedule.BookedBy = userEmail;

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Booking>> GetUserBookingsAsync(string userEmail)
        {
            return await _context.Bookings
                .Where(b => b.UserEmail == userEmail)
                .Include(b => b.Schedule)
                .ToListAsync();
        }

        public async Task<bool> CancelBookingAsync(int bookingId)
        {
            var booking = await _context.Bookings.Include(b => b.Schedule)
                .FirstOrDefaultAsync(b => b.Id == bookingId);

            if (booking == null)
                return false;

            booking.Schedule.IsBooked = false;
            booking.Schedule.BookedBy = null;

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<List<AdminSchedule>> GetAvailableTimesAsync()
        {
            return await _context.AdminSchedules
                .Where(s => !s.IsBooked)
                .OrderBy(s => s.StartTime)
                .ToListAsync();
        }

    }
}
