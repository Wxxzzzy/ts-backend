using System.ComponentModel;

namespace TSCore.Application.Common.Extensions;

public static class EnumExtensions
{
    public static string ToDescription(this Enum value)
    {
        var attributes = (DescriptionAttribute[])value
            .GetType()
            .GetField(value.ToString())
            ?.GetCustomAttributes(typeof(DescriptionAttribute), false)!;

        return attributes.Length > 0
            ? attributes[0].Description
            : string.Empty;
    }
}