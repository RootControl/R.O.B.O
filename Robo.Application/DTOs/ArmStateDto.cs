using Robo.Domain.Entities;
using Robo.Domain.Enums;

namespace Robo.Application.DTOs;

public class ArmStateDto(ArmBase leftArm)
{
    public ElbowState Elbow { get; set; } = leftArm.Elbow;
    public WristState Wrist { get; set; } = leftArm.Wrist;
}