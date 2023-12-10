namespace TSCore.Application.Common.Models;

public class AppConfiguration
{
    public DatabaseConnectionStrings ConnectionStrings { get; set; }
    public AudienceSettings Audience { get; set; }
    public string FrontEndEndpoints { get; set; }
}

public class DatabaseConnectionStrings
{
    public string Main { get; set; }
}

public class AudienceSettings
{
    public string Secret { get; set; }
    public int TokenExpiryInMinutes { get; set; }
    public string Issuer { get; set; }
    public string Aud { get; set; }
}