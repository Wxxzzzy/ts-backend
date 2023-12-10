namespace TSCore.Application.Common.Interfaces;

public interface IAuthService
{
    bool Verify(string password, string hash, byte[] salt = null);
    string CreateHash(string password, byte[] salt);
}