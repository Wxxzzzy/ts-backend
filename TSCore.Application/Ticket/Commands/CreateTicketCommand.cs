using MediatR;
using TSCore.Application.Common.Interfaces;
using TSCore.Domain.Enums;
using TSCore.Domain.Tables;
using TSCore.Domain.Common.Classes;

namespace TSCore.Application.Ticket.Commands;

public class CreateTicketCommand : IRequest
{
    public string TicketTitle { get; set; }
    public string ShortDescription { get; set; }
    public int? TicketStatus { get; set; }
    public int TeamId { get; set; }
    public int? AssignedToId { get; set; }
}

public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand>
{
    private readonly ITeamSyncDbContext _context;
    private readonly ITokenService _tokenService;

    public CreateTicketCommandHandler(ITeamSyncDbContext context, ITokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }
    
    public async Task Handle(CreateTicketCommand request, CancellationToken cancellationToken)
    {
        var userId = _tokenService.GetUserId();
        var ticket = new Domain.Tables.Ticket
        {
            TicketTitle = request.TicketTitle,
            ShortDescription = request.ShortDescription,
            TicketStatus = request.TicketStatus ?? (int)ETicketStatus.Open,
            TeamId = request.TeamId,
            TicketCreatorId = userId,
            AssignedToId = userId,
            CreatedAt = DateTime.Today,
            CreatedBy = "system"
        };

        if (request.AssignedToId != null)
        {
            ticket.AssignedToId = request.AssignedToId;
        }

        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync(cancellationToken);
    }
}