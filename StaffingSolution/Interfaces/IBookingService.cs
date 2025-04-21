using StaffingSolution.Models;

namespace StaffingSolution.Interfaces
{
    public interface IBookingService
    {
        Task<bool> BookTimeAsync(int scheduleId, string userEmail);
        Task<bool> CancelBookingAsync(int bookingId);
        Task<List<AdminSchedule>> GetAvailableTimesAsync();
        Task<List<Booking>> GetUserBookingsAsync(string userEmail);
    }
}