using System.Security.Claims;
using TSCore.API.Common.Authorization;

namespace TSCore.API.Common.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static int GetUserId(this ClaimsPrincipal user) =>
        int.TryParse(user.FindFirstValue(CustomClaimTypes.Id), out var v) ? v : default;
}