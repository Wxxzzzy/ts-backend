using TSCore.Domain.Tables;

namespace TSCore.API.Interfaces;

public interface IClientNotificationHub
{
    public Task SendMessage(Notification notification);
}