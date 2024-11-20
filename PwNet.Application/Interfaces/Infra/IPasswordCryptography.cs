namespace PwNet.Application.Interfaces.Infra
{
    public interface IPasswordCryptography
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string passwordHash);
    }
}
