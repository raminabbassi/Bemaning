namespace StaffingSolution.Models
{
    public class ScheduleModel
    {
        public DateTime SelectedDate { get; set; } = DateTime.Today;
        public int VisitDuration { get; set; } = 30;
        public int BreakTime { get; set; } = 10;
        public int MaxVisits { get; set; } = 5;
    }
}
