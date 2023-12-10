using MediatR;
using Microsoft.EntityFrameworkCore;
using TSCore.Application.Common.Exceptions;
using TSCore.Application.Common.Interfaces;

namespace TSCore.Application.Team.Commands;

public class DeleteTeamCommand : IRequest
{
    public int TeamId { get; set; }
}

public class DeleteTeamCommandHandler : IRequestHandler<DeleteTeamCommand>
{
    private readonly ITeamSyncDbContext _context;
    private readonly ITokenService _tokenService;

    public DeleteTeamCommandHandler(ITeamSyncDbContext context, ITokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    public async Task Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
    {
        var team = await _context.Teams
            .Where(x => x.Id == request.TeamId)
            .FirstOrDefaultAsync(cancellationToken);

        NotFoundException.ThrowIfNull(team);

        var userId = _tokenService.GetUserId();
        if (team.Owner != userId)
        {
            throw new NotAllowedException("Only owner can delete his team");
        }
        
        _context.Teams.Remove(team);
        await _context.SaveChangesAsync(cancellationToken);
    }
}