using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StaffingSolution.Data;
using StaffingSolution.Models;
using StaffingSolution.Services;

public class ScheduleService : IScheduleService
{
    private readonly AppDbContext _context;
    private readonly EmailService _emailService;

    public ScheduleService(AppDbContext context, EmailService emailService)
    {
        _context = context;
        _emailService = emailService;
    }

    public async Task<List<AdminSchedule>> GetAvailableTimesAsync(string adminEmail)
    {
        return await _context.AdminSchedules
            .Where(s => s.AdminEmail == adminEmail && !s.IsBooked)
            .OrderBy(s => s.StartTime)
            .ToListAsync();
    }

    public async Task AddAvailabilityAsync(string adminEmail, DateTime startTime, DateTime endTime)//skapa lediga tider 
    {
        var schedule = new AdminSchedule
        {
            AdminEmail = adminEmail,
            StartTime = startTime,
            EndTime = endTime,
            IsBooked = false
        };

        _context.AdminSchedules.Add(schedule);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveSlotAsync(int slotId)//tar bort tid från schemat 
    {
        var slot = await _context.AdminSchedules.FindAsync(slotId);

        if (slot != null)
        {
            _context.AdminSchedules.Remove(slot);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<AdminSchedule>> GetBookedTimesAsync(string adminEmail)
    {
        return await _context.AdminSchedules
            .Where(s => s.AdminEmail == adminEmail && s.IsBooked)
            .OrderBy(s => s.StartTime)
            .ToListAsync();
    }

    public async Task<bool> CancelBookingAsync(int scheduleId)
    {
        var schedule = await _context.AdminSchedules.FindAsync(scheduleId);

        if (schedule == null || !schedule.IsBooked)
            return false;

        string? userEmail = schedule.BookedBy;
        DateTime startTime = schedule.StartTime;
        DateTime endTime = schedule.EndTime;

        schedule.IsBooked = false;
        schedule.BookedBy = null;

        await _context.SaveChangesAsync();

        if (!string.IsNullOrEmpty(userEmail))
        {
            await SendCancellationEmail(userEmail, startTime, endTime);
        }

        return true;
    }

    private async Task SendCancellationEmail(string userEmail, DateTime startTime, DateTime endTime)
    {
        string subject = "Din bokade tid har avbokats";
        string message = $"Hej,\n\nDin bokade tid mellan {startTime:yyyy-MM-dd HH:mm} och {endTime:HH:mm} har avbokats av administratören.\n\nVänliga hälsningar,\nExtendly";

        await _emailService.SendEmailAsync(userEmail, subject, message);
    }
}
