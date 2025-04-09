using Robo.Domain.Enums;

namespace Robo.Domain.Entities;

public class Head
{
    public HeadRotation Rotation { get; set; } = HeadRotation.Rested;
    public HeadTilt Tilt { get; set; } = HeadTilt.Rested;
    
    public void RotateHead(HeadRotation newState)
    {
        if (Tilt == HeadTilt.Down)
            throw new InvalidOperationException("Cannot rotate head when it is tilted down");
        
        if (!CanRotate(newState))
            throw new InvalidOperationException("Cannot rotate head to this state");
        
        Rotation = newState;
    }
    
    public void TiltHead(HeadTilt newState)
    {
        if (!CanTilt(newState))
            throw new InvalidOperationException("Cannot tilt head to this state");
        
        Tilt = newState;
    }
    
    private bool CanRotate(HeadRotation newState) => (int) newState - (int) Rotation <= 1;
    private bool CanTilt(HeadTilt newState) => (int) newState - (int) Tilt <= 1;
}