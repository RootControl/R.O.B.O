using Robo.Domain.Entities;

namespace Robo.Domain.Interfaces;

public interface IRobotRepository
{
    Task<Robot> GetRobotStateAsync();
    Task SaveRobotStateAsync(Robot robot);
}