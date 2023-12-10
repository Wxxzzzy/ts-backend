using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TSCore.Application.Common.Interfaces;

namespace TSCore.Application.Authentication.Queries;

public class GetUserAccessDataQuery : IRequest<UserAccessDataDto>
{
}

public class GetUserAccessDataQueryHandler : IRequestHandler<GetUserAccessDataQuery, UserAccessDataDto>
{
    private readonly ITeamSyncDbContext _context;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;

    public GetUserAccessDataQueryHandler(IMapper mapper, ITeamSyncDbContext context, ITokenService tokenService)
    {
        _mapper = mapper;
        _context = context;
        _tokenService = tokenService;
    }

    public async Task<UserAccessDataDto> Handle(GetUserAccessDataQuery request, CancellationToken cancellationToken)
    {
        var userId = _tokenService.GetUserId();
        var result = await _context.Users
            .Where(x => x.Id == userId)
            .ProjectTo<UserAccessDataDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        return result;
    }
}