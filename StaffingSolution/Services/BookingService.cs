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
        private readonly EmailService _emailService;

        public BookingService(AppDbContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService; 
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

            await SendBookingConfirmationEmail(userEmail, schedule.StartTime, schedule.EndTime);

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

            await SendCancellationEmail(booking.UserEmail, booking.Schedule.StartTime, booking.Schedule.EndTime);

            return true;
        }

        public async Task<List<AdminSchedule>> GetAvailableTimesAsync()
        {
            return await _context.AdminSchedules
                .Where(s => !s.IsBooked)
                .OrderBy(s => s.StartTime)
                .ToListAsync();
        }

        private async Task SendBookingConfirmationEmail(string userEmail, DateTime startTime, DateTime endTime)
        {
            string subject = "Bekräftelse av bokad tid";
            string message = $"Hej, <br><br> Din bokning är bekräftad! <br> Datum & tid: {startTime:yyyy-MM-dd HH:mm} - {endTime:HH:mm} <br><br> Vänliga hälsningar, <br> Extendly";

            await _emailService.SendEmailAsync(userEmail, subject, message);
        }

        private async Task SendCancellationEmail(string userEmail, DateTime startTime, DateTime endTime)
        {
            string subject = "Bekräftelse av avbokad tid";
            string message = $"Hej, <br><br> Din bokning har blivit avbokad. <br> Tid: {startTime:yyyy-MM-dd HH:mm} - {endTime:HH:mm} <br><br> Vänliga hälsningar, <br> Extendly";

            await _emailService.SendEmailAsync(userEmail, subject, message);
        }
    }
}
