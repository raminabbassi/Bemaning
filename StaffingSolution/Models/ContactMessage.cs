using System.ComponentModel.DataAnnotations;

namespace StaffingSolution.Models
{
    public class ContactMessage
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Message { get; set; }
        public bool IsRead { get; set; } = false;
        public bool Selected { get; set; } = false;


    }
}
