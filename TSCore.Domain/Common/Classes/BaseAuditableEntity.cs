using TSCore.Domain.Common.Interfaces;

namespace TSCore.Domain.Common.Classes;

public abstract class BaseAuditableEntity : BaseEntity, IAuditableEntity
{
    public DateTime? CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string UpdatedBy { get; set; }
}