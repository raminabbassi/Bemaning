namespace StaffingSolution.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int ScheduleId { get; set; }  
        public string UserEmail { get; set; }  
        public DateTime BookedTime { get; set; } 

        public virtual AdminSchedule Schedule { get; set; }
    }
}
