using TSCore.Domain.Common.Classes;

namespace TSCore.Domain.Tables;

public class Notification : BaseEntity
{
    public int UserId { get; set; }
    public int TeamId { get; set; }
    public string Message { get; set; }
}