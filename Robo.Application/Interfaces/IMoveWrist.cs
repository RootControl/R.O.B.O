using Robo.Domain.Entities;
using Robo.Domain.Enums;

namespace Robo.Application.Interfaces;

public interface IMoveWrist
{
    Task MoveWristAsync(WristState newState);
}