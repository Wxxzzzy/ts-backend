using TSCore.Domain.Tables;

namespace TSCore.API.Interfaces;

public interface IInvitesService
{
    Task CreateNotification(int userId, int teamId, string message);
}