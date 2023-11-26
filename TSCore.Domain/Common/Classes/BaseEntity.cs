using TSCore.Domain.Common.Interfaces;

namespace TSCore.Domain.Common.Classes;

public abstract class BaseEntity : IEntity
{
    public int Id { get; set; }
}