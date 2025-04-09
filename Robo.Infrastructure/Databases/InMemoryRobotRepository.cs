using Robo.Domain.Entities;
using Robo.Domain.Interfaces;

namespace Robo.Infrastructure.Databases;

public class InMemoryRobotRepository : IRobotRepository
{
    private Robot _robot = new Robot();
    
    public Task<Robot> GetRobotStateAsync()
    {
        return Task.FromResult(_robot);
    }

    public Task SaveRobotStateAsync(Robot robot)
    {
        _robot = robot ?? throw new ArgumentNullException(nameof(robot));
        return Task.CompletedTask;
    }
}