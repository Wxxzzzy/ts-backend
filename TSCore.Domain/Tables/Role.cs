using TSCore.Domain.Common.Classes;

namespace TSCore.Domain.Tables;

public class Role : BaseAuditableEntity
{
    public string RoleName { get; set; }
    public string Description { get; set; }

    public ICollection<User> Users { get; set; }
}