using Microsoft.AspNetCore.Mvc;
using TSCore.API.Common.Authorization;
using TSCore.Application.Ticket;
using TSCore.Application.Ticket.Commands;
using TSCore.Application.Ticket.Queries;
using TSCore.Domain.Enums;

namespace TSCore.API.Controllers;

[AuthorizeMode(ERoles.Admin, ERoles.User)]
public class TicketsController : BaseController
{
    [HttpGet("{id}/team-tickets")]
    public async Task<ActionResult<List<GetTicketQueryDto>>> GetTicketsByTeam([FromRoute] int id)
    {
        var query = new GetTicketsByTeamQuery
        {
            TeamId = id
        };
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{userId}/{teamId}/user-tickets")]
    public async Task<ActionResult<List<GetTicketQueryDto>>> GetUserTickets([FromRoute] int userId,
        [FromRoute] int teamId)
    {
        var query = new GetUserTicketsQuery
        {
            TeamId = teamId,
            UserId = userId
        };
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetTicketQueryDto>> GetTicketInfo([FromRoute] int id)
    {
        var query = new GetTicketInfoQuery
        {
            TicketId = id
        };
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> CreateTicket([FromBody] CreateTicketCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }

    [HttpPut]
    public async Task<ActionResult> UpdateTicket([FromBody] UpdateTicketCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTicket(int id)
    {
        var command = new DeleteTicketCommand
        {
            Id = id
        };

        await Mediator.Send(command);
        return Ok();
    }
}