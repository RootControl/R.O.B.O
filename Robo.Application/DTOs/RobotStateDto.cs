using Robo.Domain.Entities;
using Robo.Domain.Enums;

namespace Robo.Application.DTOs;

public class RobotStateDto(Robot robot)
{
    public HeadStateDto Head { get; set; } = new HeadStateDto(robot.Head);
    public ArmStateDto LeftArmState { get; set; } = new ArmStateDto(robot.LeftArm);
    public ArmStateDto RightArmState { get; set; } = new ArmStateDto(robot.RightArm);
}