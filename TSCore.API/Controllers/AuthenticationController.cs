using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TSCore.Application.Authentication;
using TSCore.Application.Authentication.Commands;
using TSCore.Application.Authentication.Queries;
using TSCore.Application.Common.Interfaces;

namespace TSCore.API.Controllers;

public class AuthenticationController : BaseController
{
    private readonly ITokenService _tokenService;

    public AuthenticationController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }
    
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<UserAccessDataDto>> Login([FromBody] LoginCommand command)
    {
        var result = await Mediator.Send(command);
        var tokenData = _tokenService.GetToken(result);
        return Ok(tokenData);
    }
    
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult<UserAccessDataDto>> Register([FromBody] SignUpCommand command)
    {
        var result = await Mediator.Send(command);
        var tokenData = _tokenService.GetToken(result);
        return Ok(tokenData);
    }
    
    [HttpGet("token")]
    public async Task<ActionResult<UserAccessDataDto>> RefreshToken()
    {
        var query = new GetUserAccessDataQuery();
        var tokenData = await Mediator.Send(query);
        tokenData = _tokenService.GetToken(tokenData);
        return tokenData;
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        return await Task.FromResult(Ok());
    }
}