using StaffingSolution.Services;
using System.ComponentModel.DataAnnotations;

namespace StaffingSolution.Pages.BackEndPages
{
    public partial class Register
    {
        private RegisterModel registerModel = new RegisterModel();
        private string successMessage;
        private string errorMessage;

        private async Task RegisterUser()
        {
            successMessage = string.Empty;
            errorMessage = string.Empty;

            if (registerModel.Password != registerModel.ConfirmPassword)
            {
                errorMessage = "Lösenorden matchar inte!";
                return;
            }

            try
            {
                AuthService.Register(registerModel.Email, registerModel.Password);
                successMessage = "Registreringen lyckades!";
            }
            catch (Exception ex)
            {
                errorMessage = $"Ett fel uppstod: {ex.Message}";
            }
        }

        public class RegisterModel
        {
            [Required(ErrorMessage = "E-post är obligatoriskt.")]
            [EmailAddress(ErrorMessage = "Ange en giltig e-postadress.")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Lösenord är obligatoriskt.")]
            [MinLength(6, ErrorMessage = "Lösenordet måste innehålla minst 6 tecken.")]
            public string Password { get; set; }

            [Required(ErrorMessage = "Bekräfta lösenordet.")]
            public string ConfirmPassword { get; set; }
        }
    }
}
