using Robo.Domain.Enums;

namespace Robo.Application.Interfaces;

public interface ITiltHead
{
    Task TiltHeadAsync(HeadTilt newState);
}