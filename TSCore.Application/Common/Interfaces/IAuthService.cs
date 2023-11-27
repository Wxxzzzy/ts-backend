namespace TSCore.Application.Common.Interfaces;

public interface IAuthService
{
    bool Verify(string password, string hash);
    string CreateHash(string password);
}