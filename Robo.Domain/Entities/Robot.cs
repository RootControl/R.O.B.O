using Robo.Domain.Enums;

namespace Robo.Domain.Entities;

public class Robot
{
    public Head Head { get; set; } = new Head();
    public Arm LeftArm { get; set; } = new Arm();
    public Arm RightArm { get; set; } = new Arm();
    
    public void MoveHead(HeadRotation rotation, HeadTilt tilt)
    {
        Head.RotateHead(rotation);
        Head.TiltHead(tilt);
    }
    
    public void MoveLeftArm(ElbowState elbow, WristState wrist)
    {
        LeftArm.MoveElbow(elbow);
        LeftArm.MoveWrist(wrist);
    }
    
    public void MoveRightArm(ElbowState elbow, WristState wrist)
    {
        RightArm.MoveElbow(elbow);
        RightArm.MoveWrist(wrist);
    }
}