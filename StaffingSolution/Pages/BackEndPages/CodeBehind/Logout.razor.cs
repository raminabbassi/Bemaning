using Microsoft.AspNetCore.Components;
using StaffingSolution.Services;

namespace StaffingSolution.Pages.BackEndPages
{
    public partial class Logout
    {
        protected override void OnInitialized()
        {
            AuthService.Logout();
            NavigationManager.NavigateTo("/");
        }
    }
}
