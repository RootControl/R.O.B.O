using Robo.Domain.Entities;

namespace Robo.Application.Interfaces;

public interface IArmCommandService<TArmType> : IMoveElbow, IMoveWrist where TArmType : ArmBase
{
    
}