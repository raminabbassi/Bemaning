using BCrypt.Net;
using StaffingSolution.Interfaces;
using StaffingSolution.Models;
using StaffingSolution.Repositories;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace StaffingSolution.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ProtectedLocalStorage _localStorage;

        public event Action OnChange;

        private bool isAuthenticated = false;
        private string loggedInEmail = string.Empty;

        public AuthService(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor, ProtectedLocalStorage localStorage)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
            _localStorage = localStorage;

        }
        private bool isAdmin = false;

        public async Task<bool> LoginAsync(string email, string password)
        {
            var user = _userRepository.GetByEmail(email);
            isAuthenticated = user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);

            if (isAuthenticated)
            {
                loggedInEmail = email;
                isAdmin = user.IsAdmin;

                await _localStorage.SetAsync("userEmail", email);

                NotifyStateChanged();
                Console.WriteLine($"Nu är du inloggad. Admin: {isAdmin}");
            }
            else
            {
                Console.WriteLine("Fel e-post eller lösenord.");
            }

            return isAuthenticated;
        }


        public bool IsAdmin()
        {
            return isAuthenticated && loggedInEmail == "ramin.abba@hotmail.com";
        }


        public void Logout()
        {
            isAuthenticated = false;
            loggedInEmail = string.Empty;
            NotifyStateChanged();
            Console.WriteLine("Du är nu utloggad.");
        }
        public void Register(string email, string password)
        {
            if (_userRepository.GetByEmail(email) != null)
            {
                throw new InvalidOperationException("E-post används redan.");
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            _userRepository.Add(new User { Email = email, PasswordHash = hashedPassword });

            var emailService = _httpContextAccessor.HttpContext.RequestServices.GetService<EmailService>();
            _ = emailService.SendEmailAsync(email, "Välkommen till Bemaning!",
                $"Hej!<br><br> Du har nu registrerat dig på vår plattform.<br><br> Mvh, Extendly");
        }
        public async Task InitializeAsync()
        {
            var result = await _localStorage.GetAsync<string>("userEmail");
            if (result.Success && !string.IsNullOrWhiteSpace(result.Value))
            {
                loggedInEmail = result.Value;
                isAuthenticated = true;

                var user = _userRepository.GetByEmail(loggedInEmail);
                isAdmin = user?.IsAdmin ?? false;

                NotifyStateChanged();
            }
        }

        public bool IsLoggedIn()
        {
            return isAuthenticated;
        }
        public string GetLoggedInUser()
        {
            return isAuthenticated ? loggedInEmail : "Inte inloggad";
        }
        public string GetCurrentUserEmail()
        {
            return isAuthenticated ? loggedInEmail : string.Empty;
        }

        public bool IsUser()
        {
            return IsLoggedIn() && !IsAdmin();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
