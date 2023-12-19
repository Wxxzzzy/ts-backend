using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using TSCore.API.Hubs;
using TSCore.API.Interfaces;
using TSCore.Application.Common.Exceptions;
using TSCore.Application.Common.Interfaces;
using TSCore.Domain.Tables;

namespace TSCore.API.Services;

public class InvitesService : IInvitesService
{
    private readonly ITeamSyncDbContext _context;
    private readonly IHubContext<ClientNotificationHub, IClientNotificationHub> _hub;

    public InvitesService(ITeamSyncDbContext context, IHubContext<ClientNotificationHub, IClientNotificationHub> hub)
    {
        _context = context;
        _hub = hub;
    }
    
    public async Task CreateNotification(int userId, int teamId, string message)
    {
        var notification = new Notification
        {
            UserId = userId,
            TeamId = teamId,
            Message = message
        };

        _context.Notifications.Add(notification);
        await _context.SaveChangesAsync(default);

        var savedNotification = await _context.Notifications
            .Where(x => x.UserId == userId)
            .Where(x => x.TeamId == teamId)
            .Where(x => x.Message == message)
            .FirstAsync(default);

        NotFoundException.ThrowIfNull(savedNotification);

        await _hub.Clients.User(userId.ToString()).SendMessage(savedNotification);
    }
}