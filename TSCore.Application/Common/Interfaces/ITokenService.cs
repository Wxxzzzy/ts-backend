using TSCore.Application.Authentication;

namespace TSCore.Application.Common.Interfaces;

public interface ITokenService
{
    UserAccessDataDto? GetToken(UserAccessDataDto? user);
    int GetUserId();
}