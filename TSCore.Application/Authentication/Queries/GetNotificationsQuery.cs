using MediatR;
using Microsoft.EntityFrameworkCore;
using TSCore.Application.Common.Interfaces;
using TSCore.Domain.Tables;

namespace TSCore.Application.Authentication.Queries;

public class GetNotificationsQuery : IRequest<List<Notification>>
{
    public int UserId { get; set; }
}

public class GetNotificationsQueryHandler : IRequestHandler<GetNotificationsQuery, List<Notification>>
{
    private readonly ITeamSyncDbContext _context;

    public GetNotificationsQueryHandler(ITeamSyncDbContext context)
    {
        _context = context;
    }

    public async Task<List<Notification>> Handle(GetNotificationsQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.Notifications
            .Where(x => x.UserId == request.UserId)
            .ToListAsync(cancellationToken);

        return result;
    }
}