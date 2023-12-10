using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TSCore.API.Common.Authorization;

public class ClaimRequirementsFilter : IAuthorizationFilter
{
    public const string AllowedValuesSeparator = ",";
    private readonly string _claimType;
    private readonly IEnumerable<string> _allowedValues;

    public ClaimRequirementsFilter(Claim claim)
    {
        _claimType = claim.Type;
        _allowedValues = claim.Value.Split(AllowedValuesSeparator);
    }
    
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var claim = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == _claimType);
        if (claim is not null && !_allowedValues.Contains(claim.Value))
        {
            context.Result = new ForbidResult();
        }
    }
}