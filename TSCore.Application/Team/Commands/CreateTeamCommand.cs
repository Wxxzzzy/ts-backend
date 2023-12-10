using MediatR;
using Microsoft.EntityFrameworkCore;
using TSCore.Application.Common.Interfaces;
using TSCore.Domain.Tables;

namespace TSCore.Application.Team.Commands;

public class CreateTeamCommand : IRequest
{
    public string TeamName { get; set; }
}

public class CreateTeamCommandHandler : IRequestHandler<CreateTeamCommand>
{
    private readonly ITeamSyncDbContext _context;
    private readonly ITokenService _tokenService;

    public CreateTeamCommandHandler(ITeamSyncDbContext context, ITokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }
    
    public async Task Handle(CreateTeamCommand request, CancellationToken cancellationToken)
    {
        var ownerId = _tokenService.GetUserId();
        
        var username = await _context.Users
            .Where(x => x.Id == ownerId)
            .Select(x => x.Username)
            .FirstOrDefaultAsync(cancellationToken);

        var team = new Domain.Tables.Team
        {
            TeamName = request.TeamName,
            Owner = ownerId,
            CreatedBy = username ?? "system",
            CreatedAt = DateTime.Today,
        };

        _context.Teams.Add(team);
        await _context.SaveChangesAsync(cancellationToken);
    }
}