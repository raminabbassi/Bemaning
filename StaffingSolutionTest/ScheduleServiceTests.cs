using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StaffingSolution.Data;
using StaffingSolution.Models;
using StaffingSolution.Services;
using Xunit;
using System.Collections.Generic;
using StaffingSolutionTest.TestDoubles;


public class ScheduleServiceTests
{
    private AppDbContext CreateInMemoryDb()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) 
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task AddAvailability_ShouldSaveToDb()
    {
        var context = CreateInMemoryDb();
        var emailService = new FakeEmailService();
        var service = new ScheduleService(context, emailService);

        await service.AddAvailabilityAsync("admin@example.com", DateTime.Now, DateTime.Now.AddHours(1));

        var slot = await context.AdminSchedules.FirstOrDefaultAsync();
        Assert.NotNull(slot);
        Assert.Equal("admin@example.com", slot.AdminEmail);
        Assert.False(slot.IsBooked);
    }

    [Fact]
    public async Task GetAvailableTimes_ShouldReturnCorrectSlots()
    {
        var context = CreateInMemoryDb();
        var emailService = new FakeEmailService();
        var service = new ScheduleService(context, emailService);

        context.AdminSchedules.AddRange(new List<AdminSchedule>
        {
            new AdminSchedule { AdminEmail = "admin@example.com", IsBooked = false, StartTime = DateTime.Now },
            new AdminSchedule { AdminEmail = "admin@example.com", IsBooked = true, StartTime = DateTime.Now },
            new AdminSchedule { AdminEmail = "other@example.com", IsBooked = false, StartTime = DateTime.Now },
        });

        await context.SaveChangesAsync();

        var result = await service.GetAvailableTimesAsync("admin@example.com");

        Assert.Single(result);
    }

    [Fact]
    public async Task CancelBooking_ShouldMarkAsUnbookedAndSendEmail()
    {
        var context = CreateInMemoryDb();
        var emailService = new FakeEmailService();
        var service = new ScheduleService(context, emailService);

        var booking = new AdminSchedule
        {
            AdminEmail = "admin@example.com",
            StartTime = DateTime.Now,
            EndTime = DateTime.Now.AddHours(1),
            IsBooked = true,
            BookedBy = "user@example.com"
        };

        context.AdminSchedules.Add(booking);
        await context.SaveChangesAsync();

        var success = await service.CancelBookingAsync(booking.Id);

        var updated = await context.AdminSchedules.FindAsync(booking.Id);
        Assert.True(success);
        Assert.False(updated.IsBooked);
        Assert.Null(updated.BookedBy);
        Assert.True(emailService.EmailSent);
        Assert.Equal("user@example.com", emailService.SentTo);
    }
}
