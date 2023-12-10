using TSCore.Domain.Common.Classes;

namespace TSCore.Domain.Tables;

public class User : BaseAuditableEntity
{
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public string Salt { get; set; }
    public string Email { get; set; }
    public bool isBlocked { get; set; }
    
    public int? RoleId { get; set; }
    public Role Role { get; set; }

    public ICollection<Team> Teams { get; set; } // for team owner
    
    public ICollection<UserTeam> BelongTeams { get; set; }
}