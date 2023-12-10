using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TSCore.Application.Common.Interfaces;
using TSCore.Application.Team;

namespace TSCore.Application.Ticket.Queries;

public class GetUserTicketsQuery : IRequest<List<GetTicketQueryDto>>
{
    public int TeamId { get; set; }
    public int UserId { get; set; }
}

public class GetUserTicketsQueryHandler : IRequestHandler<GetUserTicketsQuery, List<GetTicketQueryDto>>
{
    private readonly ITeamSyncDbContext _context;
    private readonly IMapper _mapper;

    public GetUserTicketsQueryHandler(ITeamSyncDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<List<GetTicketQueryDto>> Handle(GetUserTicketsQuery request, CancellationToken cancellationToken)
    {
        var tickets = await _context.Tickets
            .Where(x => x.TeamId == request.TeamId)
            .Where(x => x.AssignedToId == request.UserId)
            .ProjectTo<GetTicketQueryDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return tickets;
    }
}