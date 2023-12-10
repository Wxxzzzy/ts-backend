using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using TSCore.Domain.Enums.Authentication;

namespace TSCore.API.Common.Authorization;

public class AuthorizeModeAttribute : TypeFilterAttribute
{
    public AuthorizeModeAttribute(params ERoles[] allowedRoles) : base(typeof(ClaimRequirementsFilter))
    {
        var roles = string.Join(ClaimRequirementsFilter.AllowedValuesSeparator, allowedRoles.Select(x => (int)x));
        Arguments = new object[] { new Claim(CustomClaimTypes.UserRoleId, roles) };
    }
}