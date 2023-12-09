namespace TSCore.Application.Common.Models;

public class AppConfiguration
{
    public DatabaseConnectionStrings ConnectionStrings { get; set; }
}

public class DatabaseConnectionStrings
{
    public string Main { get; set; }
}