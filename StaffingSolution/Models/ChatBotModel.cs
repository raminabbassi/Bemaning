using System.ComponentModel.DataAnnotations;

namespace StaffingSolution.Models
{
    public class ChatBotModel
    {
        [Required(ErrorMessage = "Fråga får inte vara tom.")]
        public string Question { get; set; }
    }

}
