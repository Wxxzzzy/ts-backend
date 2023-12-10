using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TSCore.Application.Common.Interfaces;

namespace TSCore.Application.Team.Queries;

public class GetUserTeamsQuery : IRequest<List<GetTeamQueryDto>>
{
    public int UserId { get; set; }
}

public class GetUserTeamsQueryHandler : IRequestHandler<GetUserTeamsQuery, List<GetTeamQueryDto>>
{
    private readonly ITeamSyncDbContext _context;
    private readonly IMapper _mapper;
    
    public GetUserTeamsQueryHandler(ITeamSyncDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<List<GetTeamQueryDto>> Handle(GetUserTeamsQuery request, CancellationToken cancellationToken)
    {
        var teams = _context.Teams
            .Where(x => x.Owner == request.UserId || x.TeamMembers.Any(u => u.UserId == request.UserId))
            .ProjectTo<GetTeamQueryDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return teams;
    }
}