using TSCore.Application;
using TSCore.Persistence;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Services
    .AddPersistence()
    .AddApplication();

app.Run();