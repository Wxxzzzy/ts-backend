using MediatR;
using Microsoft.EntityFrameworkCore;
using TSCore.Application.Common.Exceptions;
using TSCore.Application.Common.Interfaces;

namespace TSCore.Application.Ticket.Commands;

public class DeleteTicketCommand : IRequest
{
    public int Id { get; set; }
}

public class DeleteTicketCommandHandler : IRequestHandler<DeleteTicketCommand>
{
    private readonly ITeamSyncDbContext _context;

    public DeleteTicketCommandHandler(ITeamSyncDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(DeleteTicketCommand request, CancellationToken cancellationToken)
    {
        var ticket = await _context.Tickets
            .Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        
        NotFoundException.ThrowIfNull(ticket);

        _context.Tickets.Remove(ticket);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
