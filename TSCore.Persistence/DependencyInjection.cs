using Microsoft.Extensions.DependencyInjection;
using TSCore.Persistence.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TSCore.Application.Common.Interfaces;
using TSCore.Application.Common.Models;

namespace TSCore.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, DatabaseConnectionStrings connectionStrings)
    {
        services.AddDbContext<TeamSyncDbContext>(options =>
        {
            options.UseSqlServer(connectionStrings.Main,
                x => x.MigrationsHistoryTable("__MigrationHistory", "TS"));

            options.ConfigureWarnings(warnings =>
            {
                warnings.Ignore(CoreEventId.RedundantIndexRemoved);
                warnings.Ignore(CoreEventId.MultipleNavigationProperties);
                warnings.Ignore(RelationalEventId.AmbientTransactionWarning);
            });
        });

        services.AddScoped<ITeamSyncDbContext>(provider => provider.GetService<TeamSyncDbContext>());
        
        return services;
    }
}