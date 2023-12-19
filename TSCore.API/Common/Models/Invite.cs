namespace TSCore.API.Common.Models;

public class Invite
{
    public int UserId { get; set; }
    public int TeamId { get; set; }
    public string Message { get; set; }
}