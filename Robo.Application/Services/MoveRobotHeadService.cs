using Robo.Application.Interfaces;
using Robo.Domain.Enums;
using Robo.Domain.Interfaces;

namespace Robo.Application.Services;

public class MoveRobotHeadService(IRobotRepository robotRepository) : IMoveRobotHeadService
{
    private readonly IRobotRepository _robotRepository = robotRepository;

    public async Task RotateHeadAsync(HeadRotation newState)
    {
        var robot = await _robotRepository.GetRobotStateAsync();
        robot.RotateHead(newState);
        await _robotRepository.SaveRobotStateAsync(robot);
    }

    public async Task TiltHeadAsync(HeadTilt newState)
    {
        var robot = _robotRepository.GetRobotStateAsync();
        robot.Result.TiltHead(newState);
        await _robotRepository.SaveRobotStateAsync(robot.Result);
    }
}