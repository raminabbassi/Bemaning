using Microsoft.EntityFrameworkCore;
using StaffingSolution.Data;
using StaffingSolution.Models;
using StaffingSolution.Services;
using StaffingSolutionTest.TestDoubles;


public class BookingServiceTests
{
    private AppDbContext CreateInMemoryDb()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task BookTimeAsync_BookAndSendEmail()
    {
        var context = CreateInMemoryDb();
        var emailService = new FakeEmailService();
        var service = new BookingService(context, emailService);

        var schedule = new AdminSchedule
        {
            StartTime = DateTime.Now,
            EndTime = DateTime.Now.AddHours(1),
            IsBooked = false,
            AdminEmail = "admin@example.com" 
        };


        context.AdminSchedules.Add(schedule);
        await context.SaveChangesAsync();

        var success = await service.BookTimeAsync(schedule.Id, "user@example.com");

        Assert.True(success);

        var updatedSchedule = await context.AdminSchedules.FindAsync(schedule.Id);
        var booking = await context.Bookings.FirstOrDefaultAsync();

        Assert.True(updatedSchedule.IsBooked);
        Assert.Equal("user@example.com", updatedSchedule.BookedBy);
        Assert.NotNull(booking);
        Assert.Equal("user@example.com", booking.UserEmail);
        Assert.True(emailService.EmailSent);
        Assert.Equal("user@example.com", emailService.SentTo);
    }

    [Fact]
    public async Task BookTimeAsync_ReturnFalse_Booked()
    {
        var context = CreateInMemoryDb();
        var emailService = new FakeEmailService();
        var service = new BookingService(context, emailService);

        var schedule = new AdminSchedule
        {
            StartTime = DateTime.Now,
            EndTime = DateTime.Now.AddHours(1),
            IsBooked = true,
            AdminEmail = "admin@example.com" 
        };


        context.AdminSchedules.Add(schedule);
        await context.SaveChangesAsync();

        var result = await service.BookTimeAsync(schedule.Id, "user@example.com");

        Assert.False(result);
    }

    [Fact]
    public async Task GetUserBookingsAsync_ReturnUserBookings()
    {
        var context = CreateInMemoryDb();
        var emailService = new FakeEmailService();
        var service = new BookingService(context, emailService);

        var schedule = new AdminSchedule
        {
            StartTime = DateTime.Now,
            EndTime = DateTime.Now.AddHours(1),
            AdminEmail = "admin@example.com" 
        };

        context.AdminSchedules.Add(schedule);
        await context.SaveChangesAsync();

        context.Bookings.Add(new Booking
        {
            ScheduleId = schedule.Id,
            UserEmail = "ramin@example.com",
            BookedTime = schedule.StartTime
        });

        await context.SaveChangesAsync();

        var result = await service.GetUserBookingsAsync("ramin@example.com");

        Assert.Single(result);
        Assert.Equal("ramin@example.com", result[0].UserEmail);
    }


    [Fact]
    public async Task CancelBookingAsync_UnbookAndRemoveBooking()
    {
        var context = CreateInMemoryDb();
        var emailService = new FakeEmailService();
        var service = new BookingService(context, emailService);

        var schedule = new AdminSchedule
        {
            StartTime = DateTime.Now,
            EndTime = DateTime.Now.AddHours(1),
            IsBooked = true,
            BookedBy = "ramin@example.com",
            AdminEmail = "admin@example.com"
        };


        context.AdminSchedules.Add(schedule);
        await context.SaveChangesAsync();

        var booking = new Booking
        {
            ScheduleId = schedule.Id,
            UserEmail = "ramin@example.com",
            BookedTime = schedule.StartTime
        };

        context.Bookings.Add(booking);
        await context.SaveChangesAsync();

        var success = await service.CancelBookingAsync(booking.Id);

        Assert.True(success);

        var updatedSchedule = await context.AdminSchedules.FindAsync(schedule.Id);
        Assert.False(updatedSchedule.IsBooked);
        Assert.Null(updatedSchedule.BookedBy);
        Assert.Empty(context.Bookings);
        Assert.True(emailService.EmailSent);
    }

    [Fact]
    public async Task GetAvailableTimesAsync_ReturnOnlyUnbooked()
    {
        var context = CreateInMemoryDb();
        var emailService = new FakeEmailService();
        var service = new BookingService(context, emailService);

        context.AdminSchedules.AddRange(new[]
        {
         new AdminSchedule
        {
           StartTime = DateTime.Now,
           EndTime = DateTime.Now.AddHours(1),
           IsBooked = false,
           AdminEmail = "admin1@example.com"
        },
        new AdminSchedule
        {
           StartTime = DateTime.Now,
           EndTime = DateTime.Now.AddHours(1),
           IsBooked = true,
           AdminEmail = "admin2@example.com"
        }
      });
        await context.SaveChangesAsync();

        var result = await service.GetAvailableTimesAsync();

        Assert.Single(result);
        Assert.False(result[0].IsBooked);
    }
}
