using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TSCore.Application.Common.Exceptions;
using TSCore.Application.Common.Interfaces;

namespace TSCore.Application.Ticket.Queries;

public class GetTicketInfoQuery : IRequest<GetTicketQueryDto>
{
    public int TicketId { get; set; }
}

public class GetTicketInfoQueryHandler : IRequestHandler<GetTicketInfoQuery, GetTicketQueryDto>
{
    private readonly ITeamSyncDbContext _context;
    private readonly IMapper _mapper;

    public GetTicketInfoQueryHandler(ITeamSyncDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetTicketQueryDto> Handle(GetTicketInfoQuery request, CancellationToken cancellationToken)
    {
        var ticket = await _context.Tickets
            .Where(x => x.Id == request.TicketId)
            .ProjectTo<GetTicketQueryDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        NotFoundException.ThrowIfNull(ticket);
        
        return ticket;
    }
}