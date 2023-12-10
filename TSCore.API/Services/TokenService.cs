using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TSCore.API.Common.Authorization;
using TSCore.API.Common.Extensions;
using TSCore.Application.Authentication;
using TSCore.Application.Common.Interfaces;
using TSCore.Application.Common.Models;

namespace TSCore.API.Services;

public partial class TokenService : ITokenService
{
    private readonly AppConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private ClaimsPrincipal User => _httpContextAccessor?.HttpContext?.User;

    public const string AuthScheme = "TSAUTH";

    public TokenService(IOptions<AppConfiguration> appConfiguration, IHttpContextAccessor httpContextAccessor)
    {
        _configuration = appConfiguration.Value;
        _httpContextAccessor = httpContextAccessor;
    }

    public int GetUserId() => User?.GetUserId() ?? default;

    public UserAccessDataDto? GetToken(UserAccessDataDto? user)
    {
        if (user == null)
        {
            return null;
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration.Audience.Secret);
        var claims = new List<Claim>
        {
            new(CustomClaimTypes.Id, user.Id.ToString()),
            new(CustomClaimTypes.Username, user.Username),
            new(CustomClaimTypes.UserRoleId, user.UserRoleId.ToString())
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_configuration.Audience.TokenExpiryInMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _configuration.Audience.Issuer,
            Audience = _configuration.Audience.Aud
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        user.Token = $"{JwtBearerDefaults.AuthenticationScheme} {tokenHandler.WriteToken(token)}";
        user.Exp = tokenDescriptor.Expires;

        return user;
    }
}