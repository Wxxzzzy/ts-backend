using MediatR;
using Microsoft.EntityFrameworkCore;
using TSCore.Application.Common.Exceptions;
using TSCore.Application.Common.Interfaces;
using TSCore.Domain.Tables;

namespace TSCore.Application.Team.Commands;

public class AcceptInviteCommand : IRequest
{
    public int InviteId { get; set; }
    public bool Decline { get; set; } = false;
}

public class AcceptInviteCommandHandler : IRequestHandler<AcceptInviteCommand>
{
    private readonly ITeamSyncDbContext _context;

    public AcceptInviteCommandHandler(ITeamSyncDbContext context)
    {
        _context = context;
    }


    public async Task Handle(AcceptInviteCommand request, CancellationToken cancellationToken)
    {
        var invite = await _context.Notifications
            .Where(x => x.Id == request.InviteId)
            .FirstAsync(cancellationToken);
        
        NotFoundException.ThrowIfNull(invite);

        if (!request.Decline)
        {
            _context.UserTeams.Add(new UserTeam
            {
                TeamId = invite.TeamId,
                UserId = invite.UserId
            });    
        }
        _context.Notifications.Remove(invite);
        await _context.SaveChangesAsync(cancellationToken);
    }
}