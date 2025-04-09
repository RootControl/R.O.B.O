using Robo.Domain.Enums;

namespace Robo.Application.Interfaces;

public interface IRotateHead
{
    Task RotateHeadAsync(HeadRotation newState);
}