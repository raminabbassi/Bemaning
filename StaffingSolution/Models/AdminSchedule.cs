namespace StaffingSolution.Models
{
    public class AdminSchedule
    {
        public int Id { get; set; }
        public string AdminEmail { get; set; } 
        public DateTime StartTime { get; set; } 
        public DateTime EndTime { get; set; }   
        public bool IsBooked { get; set; } = false; 
        public string? BookedBy { get; set; }
        public virtual Booking Booking { get; set; }

    }
}
