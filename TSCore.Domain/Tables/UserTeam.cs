using TSCore.Domain.Common.Classes;

namespace TSCore.Domain.Tables;

public class UserTeam : BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; }
    
    public int TeamId { get; set; }
    public Team Team { get; set; }
}