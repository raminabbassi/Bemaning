namespace StaffingSolution.Interfaces
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(string email, string password); void Logout();
        void Register(string email, string password);
        bool IsLoggedIn();
        string GetLoggedInUser();
        string GetCurrentUserEmail();
        bool IsAdmin();
        bool IsUser();
        Task InitializeAsync(); 

        event Action OnChange;
    }
}