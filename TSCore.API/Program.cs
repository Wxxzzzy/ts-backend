using TSCore.Application;
using TSCore.Application.Common.Models;
using TSCore.Infrastructure;
using TSCore.Persistence;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//TODO: automatic migrations
builder.Services.Configure<AppConfiguration>(builder.Configuration);
var appConfiguration = builder.Configuration.Get<AppConfiguration>();

builder.Services
    .AddPersistence(appConfiguration.ConnectionStrings)
    .AddApplication()
    .AddInfrastructure();

app.Run();