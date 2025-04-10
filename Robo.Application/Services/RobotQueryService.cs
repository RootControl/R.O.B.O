using Robo.Application.DTOs;
using Robo.Application.Interfaces;
using Robo.Domain.Interfaces;

namespace Robo.Application.Services;

public class RobotQueryService(IRobotRepository robotRepository) : IRobotQueryService
{
    private readonly IRobotRepository _robotRepository = robotRepository;
    
    public async Task<RobotStateDto> GetRobotStateAsync()
    {
        var robot = await _robotRepository.GetRobotStateAsync();
        return new RobotStateDto(robot);
    }
}