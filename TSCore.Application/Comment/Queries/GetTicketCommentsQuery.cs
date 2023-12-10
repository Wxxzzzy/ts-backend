using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TSCore.Application.Common.Interfaces;

namespace TSCore.Application.Comment.Queries;

public class GetTicketCommentsQuery : IRequest<List<GetCommentQueryDto>>
{
    public int TicketId { get; set; }
}

public class GetTicketCommentsQueryHandler : IRequestHandler<GetTicketCommentsQuery, List<GetCommentQueryDto>>
{
    private readonly ITeamSyncDbContext _context;
    private readonly IMapper _mapper;

    public GetTicketCommentsQueryHandler(ITeamSyncDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<List<GetCommentQueryDto>> Handle(GetTicketCommentsQuery request, CancellationToken cancellationToken)
    {
        var comments = await _context.Comments
            .Where(x => x.TicketId == request.TicketId)
            .ProjectTo<GetCommentQueryDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return comments;
    }
}