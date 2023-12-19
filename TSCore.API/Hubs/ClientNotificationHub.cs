using Microsoft.AspNetCore.SignalR;
using TSCore.API.Interfaces;

namespace TSCore.API.Hubs;

public class ClientNotificationHub : Hub<IClientNotificationHub>
{
    public const string Endpoint = "api/notifications";
}