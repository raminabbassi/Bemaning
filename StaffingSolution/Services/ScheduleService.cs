using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StaffingSolution.Data;
using StaffingSolution.Models;

public class ScheduleService
{
    private readonly AppDbContext _context;

    public ScheduleService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<AdminSchedule>> GetAvailableTimesAsync(string adminEmail)
    {
        return await _context.AdminSchedules
            .Where(s => s.AdminEmail == adminEmail && !s.IsBooked)
            .OrderBy(s => s.StartTime)
            .ToListAsync();
    }

    public async Task AddAvailabilityAsync(string adminEmail, DateTime startTime, DateTime endTime)
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

    public async Task<bool> BookTimeAsync(int scheduleId, string userEmail)
    {
        var schedule = await _context.AdminSchedules.FindAsync(scheduleId);

        if (schedule == null || schedule.IsBooked)
            return false;

        schedule.IsBooked = true;
        schedule.BookedBy = userEmail;

        await _context.SaveChangesAsync();
        return true;
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

        schedule.IsBooked = false;
        schedule.BookedBy = null;

        await _context.SaveChangesAsync();
        return true;
    }
}
