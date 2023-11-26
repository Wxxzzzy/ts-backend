using Microsoft.Extensions.DependencyInjection;
using TSCore.Infrastructure.Services.Classes;
using TSCore.Infrastructure.Services.Interfaces;

namespace TSCore.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        return services;
    }
}