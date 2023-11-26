using Microsoft.Extensions.DependencyInjection;
using TSCore.Persistence.DBContext;

namespace TSCore.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        return services.AddScoped<ITeamSyncDbContext, TeamSyncDbContext>();
    }
}