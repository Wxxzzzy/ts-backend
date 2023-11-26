namespace TSCore.Domain.Common.Interfaces;

public interface IAuditableEntity : IEntity
{
    public DateTime? CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
    public string UpdatedBy { get; set; }
}