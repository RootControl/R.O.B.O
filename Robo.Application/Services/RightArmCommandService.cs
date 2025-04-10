using Robo.Application.Interfaces;
using Robo.Domain.Entities;
using Robo.Domain.Enums;
using Robo.Domain.Interfaces;

namespace Robo.Application.Services;

public class RightArmCommandService(IRobotRepository robotRepository) : IArmCommandService<RightArm>
{
    private readonly IRobotRepository _robotRepository = robotRepository;
    
    public async Task MoveElbowAsync(ElbowState newState)
    {
        var robot = await _robotRepository.GetRobotStateAsync();
        robot.RightArm.MoveElbow(newState);
        await _robotRepository.SaveRobotStateAsync(robot);
    }

    public async Task MoveWristAsync(WristState newState)
    {
        var robot = await _robotRepository.GetRobotStateAsync();
        robot.RightArm.MoveWrist(newState);
        await _robotRepository.SaveRobotStateAsync(robot);
    }
}