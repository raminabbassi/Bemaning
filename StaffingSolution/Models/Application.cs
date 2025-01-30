namespace StaffingSolution.Models
{
    public class Application
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
