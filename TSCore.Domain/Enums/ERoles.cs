using System.ComponentModel;

namespace TSCore.Domain.Enums;

public enum ERoles
{
    [Description("Admin")]
    Admin = 1,
    
    [Description("User")]
    User = 2
}