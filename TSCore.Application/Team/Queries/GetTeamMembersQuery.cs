using MediatR;
using Microsoft.EntityFrameworkCore;
using TSCore.Application.Common.Exceptions;
using TSCore.Application.Common.Interfaces;
using TSCore.Application.Common.Models;

namespace TSCore.Application.Team.Queries;

public class GetTeamMembersQuery : IRequest<List<KeyValuesBase>>
{
    public int TeamId { get; set; }
}

public class GetTeamMembersQueryHandler : IRequestHandler<GetTeamMembersQuery, List<KeyValuesBase>>
{
    private readonly ITeamSyncDbContext _context;

    public GetTeamMembersQueryHandler(ITeamSyncDbContext context)
    {
        _context = context;
    }

    public async Task<List<KeyValuesBase>> Handle(GetTeamMembersQuery request, CancellationToken cancellationToken)
    {
        var members = await _context.UserTeams
            .Where(x => x.TeamId == request.TeamId)
            .Select(x => new KeyValuesBase
            {
                Id = x.UserId,
                Value = x.User.Username
            })
            .ToListAsync(cancellationToken);

        var owner = await _context.Teams.Where(x => x.Id == request.TeamId)
            .Select(x => new KeyValuesBase
            {
                Id = x.Owner,
                Value = x.User.Username
            })
            .FirstOrDefaultAsync(cancellationToken);
        
        NotFoundException.ThrowIfNull(owner);

        members.Add(owner);
        
        return members;
    }
}