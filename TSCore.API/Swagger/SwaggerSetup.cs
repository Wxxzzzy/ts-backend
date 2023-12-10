using System.Reflection;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace TSCore.API.Swagger;

public static class SwaggerSetup
{
    public const string TSAPI = "TS-API";

    public static void SetupTsSwagger(this IServiceCollection services)
    {
        var version = Assembly.GetExecutingAssembly().GetName().Version?.ToString(3);
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc(TSAPI, new OpenApiInfo()
            {
                Version = version ?? "0.0.0",
                Title = "TeamSync API",
                Description = "API for TeamSync Application",
            });

            c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
            {
                Description = "JWT Authorization using Bearer Scheme: {Bearer ApiKey}",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = JwtBearerDefaults.AuthenticationScheme
            });
            
            c.OperationFilter<AddAuthorizationOperation>();
        });

        services.AddFluentValidationRulesToSwagger();
    }

    public static void ConfigureTsSwagger(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint($"/swagger/{TSAPI}/swagger.json", TSAPI);
            c.RoutePrefix = "api";
            c.DocExpansion(DocExpansion.None);
        });
    }
}