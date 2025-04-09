using Robo.Application.DTOs;
using Robo.Application.Interfaces;
using Robo.Domain.Entities;
using Robo.Domain.Enums;
using Robo.Domain.Interfaces;

namespace Robo.Application.Services;

public class RobotService(IRobotRepository robotRepository, IMoveRobotHeadService moveRobotHeadService, IMoveRobotArmService moveRobotArmService) : IRobotService
{
    private readonly IRobotRepository _robotRepository = robotRepository;
    private readonly IMoveRobotHeadService _moveRobotHeadService = moveRobotHeadService;
    private readonly IMoveRobotArmService _moveRobotArmService = moveRobotArmService;

    public async Task<RobotStateDto> GetRobotStateAsync()
    {
        var robot = await _robotRepository.GetRobotStateAsync();
        return RobotToDto(robot);
    }

    public Task RotateHeadAsync(HeadRotation newState) 
        => _moveRobotHeadService.RotateHeadAsync(newState);

    public Task TiltHeadAsync(HeadTilt newState)
        => _moveRobotHeadService.TiltHeadAsync(newState);

    public Task MoveElbowAsync(Arm arm, ElbowState newState) 
        => _moveRobotArmService.MoveElbowAsync(arm, newState);
    
    public Task MoveWristAsync(Arm arm, WristState newState) 
        => _moveRobotArmService.MoveWristAsync(arm, newState);
    
    private static RobotStateDto RobotToDto(Robot robot) => new RobotStateDto(robot);
}