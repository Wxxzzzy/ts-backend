using TSCore.Domain.Common.Classes;

namespace TSCore.Domain.Tables;

public class User : BaseAuditableEntity
{
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public string Email { get; set; }
    
    public int? RoleId { get; set; }
    public Role Role { get; set; }
}