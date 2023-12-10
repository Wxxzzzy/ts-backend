using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TSCore.Application.Common.Exceptions;
using TSCore.Application.Common.Interfaces;

namespace TSCore.Application.Ticket.Queries;

public class GetTicketsByTeamQuery : IRequest<List<GetTicketQueryDto>>
{
    public int TeamId { get; set; }
}

public class GetTicketsByTeamQueryHandler : IRequestHandler<GetTicketsByTeamQuery, List<GetTicketQueryDto>>
{
    private readonly ITeamSyncDbContext _context;
    private readonly IMapper _mapper;

    public GetTicketsByTeamQueryHandler(ITeamSyncDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<List<GetTicketQueryDto>> Handle(GetTicketsByTeamQuery request, CancellationToken cancellationToken)
    {
        var tickets = await _context.Tickets
            .Where(x => x.TeamId == request.TeamId)
            .ProjectTo<GetTicketQueryDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        
        return tickets;
    }
}