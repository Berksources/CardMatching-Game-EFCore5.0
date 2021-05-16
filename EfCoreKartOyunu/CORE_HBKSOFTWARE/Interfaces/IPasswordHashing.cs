namespace CORE_HBKSOFTWARE.Interfaces
{
    public interface IPasswordHashing
    {
        string PasswordHash(string password);
        bool PasswordCheck(string dbPassword, string enteredPassword);
    }
}