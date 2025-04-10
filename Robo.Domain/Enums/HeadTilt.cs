using System.ComponentModel;

namespace Robo.Domain.Enums;

public enum HeadTilt
{
    [Description("Tilted Up")]
    Up,
    [Description("Rested")]
    Rested,
    [Description("Tilted Down")]
    Down
}