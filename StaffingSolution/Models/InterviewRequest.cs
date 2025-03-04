using System.ComponentModel.DataAnnotations;

namespace StaffingSolution.Models
{
    public class InterviewRequest
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Namn är obligatoriskt.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "E-postadress är obligatorisk.")]
        [EmailAddress(ErrorMessage = "Ogiltig e-postadress.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Jobbtitel är obligatoriskt.")]
        public string JobTitle { get; set; }

        [Required(ErrorMessage = "Meddelande är obligatoriskt.")]
        public string Message { get; set; }
    }
}
