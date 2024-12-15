namespace StaffingSolution.Interfaces
{
    public interface IAuthService
    {
        bool Login(string email, string password);
        void Logout();
        void Register(string email, string password);
        bool IsLoggedIn();
        string GetLoggedInUser();
        string GetCurrentUserEmail();
        event Action OnChange;
    }
}