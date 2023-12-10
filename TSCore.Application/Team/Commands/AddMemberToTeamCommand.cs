using System.Diagnostics;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TSCore.Application.Common.Exceptions;
using TSCore.Application.Common.Interfaces;
using TSCore.Domain.Tables;

namespace TSCore.Application.Team.Commands;

public class AddMemberToTeamCommand : IRequest<List<string>>
{
    public int TeamId { get; set; }
    public int UserId { get; set; }
}

public class AddMemberToTeamCommandHandler : IRequestHandler<AddMemberToTeamCommand, List<string>>
{
    private readonly ITeamSyncDbContext _context;

    public AddMemberToTeamCommandHandler(ITeamSyncDbContext context)
    {
        _context = context;
    }

    public async Task<List<string>> Handle(AddMemberToTeamCommand request, CancellationToken cancellationToken)
    {
        var team = await _context.Teams
            .Where(x => x.Id == request.TeamId)
            .FirstOrDefaultAsync(cancellationToken);

        NotFoundException.ThrowIfNull(team);

        var isMember = await _context.UserTeams
            .Where(x => x.TeamId == request.TeamId)
            .Where(x => x.UserId == request.UserId)
            .FirstOrDefaultAsync(cancellationToken);

        if (isMember != null)
        {
            throw new BadHttpRequestException("User already member of this team");
        }

        _context.UserTeams.Add(new UserTeam
        {
            TeamId = request.TeamId,
            UserId = request.UserId
        });
        await _context.SaveChangesAsync(cancellationToken);

        var allMembers = await _context.UserTeams
            .Where(x => x.TeamId == request.TeamId)
            .Select(x => x.User.Username)
            .ToListAsync(cancellationToken);

        return allMembers;
    }
}