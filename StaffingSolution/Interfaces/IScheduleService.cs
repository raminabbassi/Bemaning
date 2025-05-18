using StaffingSolution.Models;

public interface IScheduleService
{
    Task AddAvailabilityAsync(string adminEmail, DateTime startTime, DateTime endTime);
    Task<bool> CancelBookingAsync(int scheduleId);
    Task<List<AdminSchedule>> GetAvailableTimesAsync(string adminEmail);
    Task<List<AdminSchedule>> GetBookedTimesAsync(string adminEmail);
    Task RemoveSlotAsync(int slotId);
}