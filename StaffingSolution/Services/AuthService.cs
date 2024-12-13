using BCrypt.Net;
using StaffingSolution.Interfaces;
using StaffingSolution.Models;
using StaffingSolution.Repositories;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace StaffingSolution.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        // Event för att notifiera om statusändring
        public event Action OnChange;

        public AuthService(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        // Logga in användare och notifiera om statusändring
        public bool Login(string email, string password)
        {
            var user = _userRepository.GetByEmail(email);
            var isAuthenticated = user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);

            if (isAuthenticated)
            {
                NotifyStateChanged();
            }

            return isAuthenticated;
        }

        // Registrera ny användare
        public void Register(string email, string password)
        {
            if (_userRepository.GetByEmail(email) != null)
                throw new InvalidOperationException("E-post används redan.");

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            _userRepository.Add(new User { Email = email, PasswordHash = hashedPassword });
        }

        // Hämta e-post för inloggad användare
        public string GetCurrentUserEmail()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user?.Identity?.IsAuthenticated == true)
            {
                return user.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty;
            }
            return string.Empty;
        }

        // Kontrollera om användaren är inloggad
        public bool IsLoggedIn()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            return user?.Identity?.IsAuthenticated == true;
        }

        // Hämta information om inloggad användare
        public string GetLoggedInUser()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user?.Identity?.IsAuthenticated == true)
            {
                return user.FindFirst(ClaimTypes.Email)?.Value ?? "Okänd användare";
            }
            return "Inte inloggad";
        }

        // Notifiera om statusändring
        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
