using Microsoft.AspNetCore.Components;
using StaffingSolution.Services;
using System.ComponentModel.DataAnnotations;

namespace StaffingSolution.Pages.BackEndPages
{
    public partial class Login
    {
        private LoginModel loginModel = new();
        private string ErrorMessage;

        private async Task LoginUser()
        {
            ErrorMessage = string.Empty;

            var isLoggedIn = await AuthService.LoginAsync(loginModel.Email, loginModel.Password);
            if (isLoggedIn)
            {
                Console.WriteLine("Inloggning lyckades!");

                NavigationManager.NavigateTo("/");
            }
            else
            {
                ErrorMessage = "Fel e-postadress eller lösenord.";
            }
        }

        public class LoginModel
        {
            [Required(ErrorMessage = "E-post är obligatoriskt.")]
            [EmailAddress(ErrorMessage = "Ange en giltig e-postadress.")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Lösenord är obligatoriskt.")]
            public string Password { get; set; }
        }
    }
}
