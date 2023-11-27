using Microsoft.Extensions.DependencyInjection;
using TSCore.Application.Common.Interfaces;
using TSCore.Infrastructure.Services.Classes;

namespace TSCore.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        return services;
    }
}