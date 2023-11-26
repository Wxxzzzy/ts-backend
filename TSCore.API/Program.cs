using TSCore.Application;
using TSCore.Persistence;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services
    .AddPersistence(connectionString)
    .AddApplication();

app.Run();