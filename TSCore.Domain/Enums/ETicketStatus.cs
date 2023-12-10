using System.ComponentModel;

namespace TSCore.Domain.Enums;

public enum ETicketStatus
{
    [Description("Open")]
    Open = 1,
    
    [Description("InProgress")]
    InProgress = 2,
    
    [Description("Resolved")]
    Resolved = 3
}