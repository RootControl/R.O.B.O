using Robo.Domain.Entities;
using Robo.Domain.Enums;

namespace Robo.Application.DTOs;

public class ArmStateDto
{
    public ElbowState Elbow { get; set; }
    public WristState Wrist { get; set; }
    
    public ArmStateDto() { }

    public ArmStateDto(ArmBase arm)
    {
        Elbow = arm.Elbow;
        Wrist = arm.Wrist;
    }
}