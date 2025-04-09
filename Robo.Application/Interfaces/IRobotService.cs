using Robo.Application.DTOs;
using Robo.Domain.Entities;
using Robo.Domain.Enums;

namespace Robo.Application.Interfaces;

public interface IRobotService
{
    Task<RobotStateDto> GetRobotStateAsync();
    Task RotateHeadAsync(HeadRotation newState);
    Task TiltHeadAsync(HeadTilt newState);
    Task MoveElbowAsync(Arm arm, ElbowState newState);
    Task MoveWristAsync(Arm arm, WristState newState);
}