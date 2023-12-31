using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using TSCore.API.Hubs;
using TSCore.API.Interfaces;
using TSCore.API.Services;
using TSCore.API.Swagger;
using TSCore.Application;
using TSCore.Application.Common.Interfaces;
using TSCore.Application.Common.Models;
using TSCore.Infrastructure;
using TSCore.Infrastructure.Services.Classes;
using TSCore.Persistence;

var builder = WebApplication.CreateBuilder(args);

// TODO: automatic migrations
// TODO: move services configurations to extension methods
builder.Services.Configure<AppConfiguration>(builder.Configuration);
var appConfiguration = builder.Configuration.Get<AppConfiguration>();

builder.Services
    .AddPersistence(appConfiguration.ConnectionStrings)
    .AddApplication()
    .AddInfrastructure();


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        var origins = appConfiguration.FrontEndEndpoints.Split(';');
        policy.WithOrigins(origins)
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddSignalR();
builder.Services.AddScoped<IInvitesService, InvitesService>();

builder.Services.AddControllers(obj =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();

    obj.Filters.Add(new AuthorizeFilter(policy));
}).AddNewtonsoftJson(options =>
{
    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
    options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.SetupTsSwagger();

builder.Services.AddHttpContextAccessor();
builder.Services
    .AddScoped<ITokenService, TokenService>()
    .AddScoped<IAuthService, AuthService>();

builder.Services.AddAuthentication(TokenService.AuthScheme)
    .AddJwtBearer(TokenService.AuthScheme, x =>
    {
        var key = Encoding.ASCII.GetBytes(appConfiguration.Audience.Secret);
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            // The signing key must match
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),

            // Validate the JWT Issuer claim
            ValidateIssuer = true,
            ValidIssuer = appConfiguration.Audience.Issuer,

            // Validate the JWT Audience (aud) claim
            ValidateAudience = true,
            ValidAudience = appConfiguration.Audience.Aud,

            //Validate token expiry
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.DisableImplicitFromServicesParameters = true;
});

var app = builder.Build();

app.UseCors();
app.MapHub<ClientNotificationHub>(ClientNotificationHub.Endpoint);
app.MapControllers();
app.UseHttpsRedirection();
app.ConfigureTsSwagger();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.Run();