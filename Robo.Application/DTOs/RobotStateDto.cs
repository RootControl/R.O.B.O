using System.Text.Json.Serialization;
using Robo.Domain.Entities;
using Robo.Domain.Enums;

namespace Robo.Application.DTOs;

public class RobotStateDto
{
    public HeadStateDto Head { get; set; }
    public ArmStateDto LeftArmState { get; set; }
    public ArmStateDto RightArmState { get; set; }
    
    [JsonConstructor]
    public RobotStateDto(HeadStateDto head, ArmStateDto leftArmState, ArmStateDto rightArmState)
    {
        Head = head ?? throw new ArgumentNullException(nameof(head));
        LeftArmState = leftArmState ?? throw new ArgumentNullException(nameof(leftArmState));
        RightArmState = rightArmState ?? throw new ArgumentNullException(nameof(rightArmState));
    }

    public RobotStateDto(Robot robot)
    {
        Head = new HeadStateDto(robot.Head);
        LeftArmState = new ArmStateDto(robot.LeftArm);
        RightArmState = new ArmStateDto(robot.RightArm);
    }
}