using MediatR;
using Microsoft.AspNetCore.Mvc;
using TSCore.Application.Authentication;
using TSCore.Application.Authentication.Commands;

namespace TSCore.API.Controllers.Authentication;

public class AuthenticationController : BaseController
{
    public readonly IMediator _mediator;
    
    public AuthenticationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<string> SingIn(string login, string password)
    {
        var command = new LoginCommand
        {
            Username = login,
            Password = password
        };

        var result = await _mediator.Send(command);
        return result;
    }

    [HttpPost("signUp")]
    public async Task<string> SignUp([FromBody] SignUpDto request)
    {
        var command = new SignUpCommand(request.Login, request.Password, request.Email);
        var result = await _mediator.Send(command);
        return result;
    }
}