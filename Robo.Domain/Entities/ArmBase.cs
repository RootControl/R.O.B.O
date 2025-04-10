using Robo.Domain.Enums;

namespace Robo.Domain.Entities;

public abstract class ArmBase
{
    public ElbowState Elbow { get; private set; } = ElbowState.Rested;
    public WristState Wrist { get; private set; } = WristState.Rested;
    
    public void MoveElbow(ElbowState newState)
    {
        if (!CanMoveElbow(newState))
            throw new InvalidOperationException("Cannot move elbow to this state");
        
        Elbow = newState;
    }
    
    public void MoveWrist(WristState newState)
    {
        if (Elbow != ElbowState.FullyBent)
            throw new InvalidOperationException("Cannot move wrist when elbow is not fully bent");
        
        if (!CanMoveWrist(newState))
            throw new InvalidOperationException("Cannot move wrist to this state");
        
        Wrist = newState;
    }
    
    private bool CanMoveElbow(ElbowState newState) => Math.Abs((int) newState - (int) Elbow) <= 1;
    private bool CanMoveWrist(WristState newState) => Math.Abs((int) newState - (int) Wrist) <= 1;
}