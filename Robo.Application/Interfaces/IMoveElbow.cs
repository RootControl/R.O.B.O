using Robo.Domain.Entities;
using Robo.Domain.Enums;

namespace Robo.Application.Interfaces;

public interface IMoveElbow
{
    Task MoveElbowAsync(Arm arm, ElbowState newState);
}