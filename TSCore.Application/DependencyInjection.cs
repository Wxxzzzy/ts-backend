using System.Reflection;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TSCore.Application.Common.Behaviors;
using TSCore.Application.Common.Interfaces;

namespace TSCore.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));


        var serializer = new JsonSerializer()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
        };
        services.AddSingleton(serializer);

        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(
            configuration => configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

    services.AddValidatorsFromAssemblyContaining<ITeamSyncDbContext>();
        
        // TODO: Configure sieve ???
        
        return services;
    }
}