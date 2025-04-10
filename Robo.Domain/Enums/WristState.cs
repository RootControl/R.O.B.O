using System.ComponentModel;

namespace Robo.Domain.Enums;

public enum WristState
{
    [Description("Rotated -90°")]
    RotatedMinus90,
    [Description("Rotated -45°")]
    RotatedMinus45, 
    [Description("Rested")]
    Rested,
    [Description("Rotated 45°")]
    Rotated45,
    [Description("Rotated 90°")]
    Rotated90,
    [Description("Rotated 135°")]
    Rotated135,
    [Description("Rotated 180°")]
    Rotated180
}