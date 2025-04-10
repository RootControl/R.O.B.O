using System.ComponentModel;

namespace Robo.Domain.Enums;

public enum ElbowState
{
    [Description("Rested")]
    Rested,
    [Description("Slightly Bent")]
    SlightlyBent,
    [Description("Bent")]
    Bent,
    [Description("Fully Bent")]
    FullyBent
}