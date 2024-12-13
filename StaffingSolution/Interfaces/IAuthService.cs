namespace StaffingSolution.Interfaces
{
    public interface IAuthService
    {
        bool Login(string email, string password);
        void Register(string email, string password);
        string GetCurrentUserEmail();
    }
}