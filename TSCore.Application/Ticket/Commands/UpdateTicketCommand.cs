using MediatR;
using Microsoft.EntityFrameworkCore;
using TSCore.Application.Common.Exceptions;
using TSCore.Application.Common.Interfaces;

namespace TSCore.Application.Ticket.Commands;

public class UpdateTicketCommand : IRequest
{
    public int Id { get; set; }
    public string TicketTitle { get; set; }
    public string ShortDescription { get; set; }
    public int TicketStatus { get; set; }
    public int TeamId { get; set; }
    public int TicketCreatorId { get; set; }
    public int? AssignedToId { get; set; }
}

public class UpdateTicketCommandHandler : IRequestHandler<UpdateTicketCommand>
{
    private readonly ITeamSyncDbContext _context;

    public UpdateTicketCommandHandler(ITeamSyncDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(UpdateTicketCommand request, CancellationToken cancellationToken)
    {
        var ticket = await _context.Tickets
            .Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        
        NotFoundException.ThrowIfNull(ticket);

        ticket.TicketTitle = request.TicketTitle;
        ticket.ShortDescription = request.ShortDescription;
        ticket.TicketStatus = request.TicketStatus;
        ticket.TicketCreatorId = request.TicketCreatorId;
        ticket.AssignedToId = request.AssignedToId;
        ticket.TeamId = request.TeamId;
        ticket.UpdatedBy = "system";
        ticket.UpdatedAt = DateTime.Now;

        await _context.SaveChangesAsync(cancellationToken);
    }
}