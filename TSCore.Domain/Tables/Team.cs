using System.Collections;
using TSCore.Domain.Common.Classes;

namespace TSCore.Domain.Tables;

public class Team : BaseAuditableEntity
{
    public string TeamName { get; set; }
    
    public int Owner { get; set; }
    public User User { get; set; } // for team owner

    public ICollection<UserTeam> TeamMembers { get; set; }
}