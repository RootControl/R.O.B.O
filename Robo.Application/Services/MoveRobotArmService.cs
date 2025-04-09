using Robo.Application.Interfaces;
using Robo.Domain.Entities;
using Robo.Domain.Enums;
using Robo.Domain.Interfaces;

namespace Robo.Application.Services;

public class MoveRobotArmService(IRobotRepository robotRepository) : IMoveRobotArmService
{
    private readonly IRobotRepository _robotRepository = robotRepository;
    
    public async Task MoveElbowAsync(Arm arm, ElbowState newState)
    {
        var robot = await _robotRepository.GetRobotStateAsync();
        robot.MoveElbow(arm, newState);
        await _robotRepository.SaveRobotStateAsync(robot);
    }

    public async Task MoveWristAsync(Arm arm, WristState newState)
    {
        var robot = await _robotRepository.GetRobotStateAsync();
        robot.MoveWrist(arm, newState);
        await _robotRepository.SaveRobotStateAsync(robot);
    }
}