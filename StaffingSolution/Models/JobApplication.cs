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
        public bool IsStudying { get; set; }
        public bool CanWorkFullTime { get; set; }
        public bool HasDrivingLicense { get; set; }
        public int ExperienceYears { get; set; }
        public int SwedishSkills { get; set; }
        public bool HasPreviousExperience { get; set; }
        public bool CanWorkMorning { get; set; }
        public bool CanWorkEvening { get; set; }
        public bool CanWorkNight { get; set; }
        public string PersonalStatement { get; set; } = string.Empty;
        public byte[]? CvFile { get; set; }
    }

}
