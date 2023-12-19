using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TSCore.API.Common.Authorization;
using TSCore.Application.Authentication;
using TSCore.Application.Authentication.Commands;
using TSCore.Application.Authentication.Queries;
using TSCore.Application.Common.Interfaces;
using TSCore.Domain.Enums;
using TSCore.Domain.Tables;

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

    [HttpGet("{userId}/userNotifications")]
    public async Task<ActionResult<List<Notification>>> GetUserUnCheckedNotifications([FromRoute] int userId)
    {
        var query = new GetNotificationsQuery
        {
            UserId = userId
        };
        var result = await Mediator.Send(query);
        return Ok(result);
    }
}

[AuthorizeMode(ERoles.Admin)]
public class AdminController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<List<GetAllUserRecord>>> GetAllUsers()
    {
        var query = new GetAllUsersQuery();
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpPut("{username}/{block}/block-or-unblock")]
    public async Task<IActionResult> BlockOrUnblockUser([FromRoute] string username, [FromRoute] bool block)
    {
        var command = new BlockOrUnblockUserCommand
        {
            Username = username,
            IsBlocked = block
        };
        await Mediator.Send(command);
        return Ok();
    }
}