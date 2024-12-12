namespace StaffingSolution.Models
{
    public class Job
    {
        public string Title { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; } // Lägg till ImageUrl-egenskapen
        public string Responsibilities { get; set; } // Ansvarsområden
        public string Requirements { get; set; }     // Krav

    }

}
