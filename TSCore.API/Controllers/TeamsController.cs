using Microsoft.AspNetCore.Mvc;
using TSCore.API.Common.Authorization;
using TSCore.Application.Common.Interfaces;
using TSCore.Application.Team;
using TSCore.Application.Team.Commands;
using TSCore.Application.Team.Queries;
using TSCore.Domain.Enums.Authentication;

namespace TSCore.API.Controllers;

[AuthorizeMode(ERoles.Admin, ERoles.User)]
public class TeamsController : BaseController
{
    private readonly ITokenService _tokenService;
    
    public TeamsController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }
    
    [HttpGet("my-teams")]
    public async Task<ActionResult<List<GetTeamQueryDto>>> GetUserTeams()
    {
        var id = _tokenService.GetUserId();
        var query = new GetUserTeamsQuery
        {
            UserId = id
        };
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetTeamQueryDto>> GetTeam([FromRoute] int id)
    {
        var query = new GetTeamQuery
        {
            TeamId = id
        };
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> CreateTeam([FromBody] CreateTeamCommand request)
    {
        await Mediator.Send(request);
        return Ok();
    }

    [HttpPut("members")]
    public async Task<ActionResult<List<string>>> AddMemberToTeam([FromBody] AddMemberToTeamCommand request)
    {
        var result = await Mediator.Send(request);
        return Ok(result);
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTeam([FromRoute] int id)
    {
        var command = new DeleteTeamCommand
        {
            TeamId = id
        };
        await Mediator.Send(command);
        return Ok();
    }
}