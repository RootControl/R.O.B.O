using Robo.Domain.Enums;

namespace Robo.Domain.Entities;

public class Robot
{
    public Head Head { get; private set; } = new Head();
    public LeftArm LeftArm { get; private set; } = new LeftArm();
    public RightArm RightArm { get; private set; } = new RightArm();
}