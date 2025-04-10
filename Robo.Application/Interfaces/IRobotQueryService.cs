using Robo.Application.DTOs;

namespace Robo.Application.Interfaces;

public interface IRobotQueryService
{
    Task<RobotStateDto> GetRobotStateAsync();
}