using Robo.Domain.Entities;
using Robo.Domain.Enums;

namespace Robo.Application.DTOs;

public class ArmStateDto(Arm arm)
{
    public ElbowState Elbow { get; set; } = arm.Elbow;
    public WristState Wrist { get; set; } = arm.Wrist;
}