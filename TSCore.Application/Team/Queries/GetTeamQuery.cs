using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TSCore.Application.Common.Exceptions;
using TSCore.Application.Common.Interfaces;

namespace TSCore.Application.Team.Queries;

public class GetTeamQuery : IRequest<GetTeamQueryDto>
{
    public int TeamId { get; set; }
}

public class GetTeamQueryHandler : IRequestHandler<GetTeamQuery, GetTeamQueryDto>
{
    private readonly ITeamSyncDbContext _context;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;

    public GetTeamQueryHandler(ITeamSyncDbContext context, IMapper mapper, ITokenService tokenService)
    {
        _context = context;
        _mapper = mapper;
        _tokenService = tokenService;
    }

    public async Task<GetTeamQueryDto> Handle(GetTeamQuery request, CancellationToken cancellationToken)
    {
        var userId = _tokenService.GetUserId();
        
        var team = await _context.Teams
            .Where(x => x.Id == request.TeamId)
            .ProjectTo<GetTeamQueryDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        NotFoundException.ThrowIfNull(team);

        // User cannot see not owned teams
        if (userId != team.Owner)
        {
            throw new NotFoundException("Team not found");
        }

        var owner = await _context.Teams
            .Where(x => x.Id == request.TeamId)
            .Select(x => x.User.Username)
            .FirstAsync(cancellationToken);

        team.TeamMembers.Add(owner);
        
        return team;
    }
}