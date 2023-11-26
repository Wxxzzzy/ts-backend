using Microsoft.Extensions.DependencyInjection;
using TSCore.Persistence.DBContext;
using Microsoft.EntityFrameworkCore;

namespace TSCore.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, string connectionString)
    {
        services.AddScoped<ITeamSyncDbContext, TeamSyncDbContext>();


        services.AddDbContext<TeamSyncDbContext>(options => options.UseSqlServer(connectionString));

        return services;
    }
}