using MediatR;
using Microsoft.EntityFrameworkCore;
using TSCore.Application.Common.Interfaces;
using TSCore.Application.Common.Models;

namespace TSCore.Application.Team.Queries;

public class GetUsersQuery : IRequest<List<KeyValuesBase>>
{
    public int TeamId { get; set; }
}

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<KeyValuesBase>>
{
    private readonly ITeamSyncDbContext _context;

    public GetUsersQueryHandler(ITeamSyncDbContext context)
    {
        _context = context;
    }


    public async Task<List<KeyValuesBase>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var includedUsers = await _context.UserTeams
            .Where(x => x.TeamId == request.TeamId)
            .Select(x => x.User.Username)
            .ToListAsync(cancellationToken);

        var ownerUsername = await _context.Teams
            .Where(x => x.Id == request.TeamId)
            .Select(x => x.User.Username)
            .FirstAsync(cancellationToken);
        
        includedUsers.Add(ownerUsername);

        var freeUsers = await _context.Users
            .Where(x => !includedUsers.Contains(x.Username))
            .Select(x => new KeyValuesBase
            {
                Id = x.Id,
                Value = x.Username
            })
            .ToListAsync(cancellationToken);

        return freeUsers;
    }
}