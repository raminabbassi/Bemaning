namespace StaffingSolution.Models
{
    public class JobApplication
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public string Status { get; set; }
        public DateTime AppliedDate { get; set; }
        public string UserEmail { get; set; } 
    }

}
