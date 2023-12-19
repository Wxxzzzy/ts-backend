using Microsoft.AspNetCore.Mvc;
using TSCore.API.Common.Authorization;
using TSCore.API.Common.Models;
using TSCore.API.Interfaces;
using TSCore.Application.Common.Interfaces;
using TSCore.Application.Common.Models;
using TSCore.Application.Team;
using TSCore.Application.Team.Commands;
using TSCore.Application.Team.Queries;
using TSCore.Domain.Enums;

namespace TSCore.API.Controllers;

[AuthorizeMode(ERoles.Admin, ERoles.User)]
public class TeamsController : BaseController
{
    private readonly ITokenService _tokenService;
    private readonly IInvitesService _invitesService;
    
    public TeamsController(ITokenService tokenService, IInvitesService invitesService)
    {
        _tokenService = tokenService;
        _invitesService = invitesService;
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

    [HttpGet("{teamId}/members")]
    public async Task<ActionResult<List<KeyValuesBase>>> GetTeamMembersKeyValues([FromRoute] int teamId)
    {
        var query = new GetTeamMembersQuery
        {
            TeamId = teamId
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

    [HttpPost("invite")]
    public async Task<IActionResult> Invite([FromBody] Invite invitation)
    {
        await _invitesService.CreateNotification(invitation.UserId, invitation.TeamId, invitation.Message);
        return Ok();
    }

    [HttpDelete("invite/{inviteId}")]
    public async Task<IActionResult> AcceptInvitation([FromRoute] int inviteId)
    {
        var command = new AcceptInviteCommand
        {
            InviteId = inviteId
        };
        await Mediator.Send(command);
        return Ok();
    }

    [HttpDelete("invite/{inviteId}/decline")]
    public async Task<IActionResult> Decline([FromRoute] int inviteId)
    {
        var command = new AcceptInviteCommand
        {
            InviteId = inviteId,
            Decline = true
        };
        await Mediator.Send(command);
        return Ok();
    }

    [HttpGet("{teamId}/not-in-team")]
    public async Task<ActionResult<List<KeyValuesBase>>> GetNotIncludedUsers([FromRoute] int teamId)
    {
        var query = new GetUsersQuery
        {
            TeamId = teamId
        };
        var result = await Mediator.Send(query);
        return Ok(result);
    }
}