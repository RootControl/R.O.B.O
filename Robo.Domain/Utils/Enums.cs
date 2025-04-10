using System.ComponentModel;

namespace Robo.Domain.Utils;

public static class Enums
{
    public static string GetDescription(this Enum value)
    {
        var fieldInfo = value.GetType().GetField(value.ToString());
        var descriptionAttributes = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
        return descriptionAttributes.Length == 0 ? value.ToString() : ((DescriptionAttribute)descriptionAttributes[0]).Description;
    }
}