using Robo.Domain.Enums;

namespace Robo.Domain.Entities;

public class Arm
{
    public ElbowState Elbow { get; set; } = ElbowState.Rested;
    public WristState Wrist { get; set; } = WristState.Rested;

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
    
    private bool CanMoveElbow(ElbowState newState) => (int) newState - (int) Elbow <= 1;
    private bool CanMoveWrist(WristState newState) => (int) newState - (int) Wrist <= 1;
}