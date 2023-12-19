using MediatR;
using Microsoft.EntityFrameworkCore;
using TSCore.Application.Common.Interfaces;
using TSCore.Domain.Enums;

namespace TSCore.Application.Authentication.Queries;

public class GetAllUsersQuery : IRequest<List<GetAllUserRecord>>
{
    
}

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<GetAllUserRecord>>
{
    private readonly ITeamSyncDbContext _context;

    public GetAllUsersQueryHandler(ITeamSyncDbContext context)
    {
        _context = context;
    }

    public async Task<List<GetAllUserRecord>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _context.Users
            .Where(x => (ERoles)x.RoleId == ERoles.User)
            .Select(x => new GetAllUserRecord(x.Username, x.isBlocked))
            .ToListAsync(cancellationToken);
        
        return users;
    }
}