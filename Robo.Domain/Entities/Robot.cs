using Robo.Domain.Enums;

namespace Robo.Domain.Entities;

public class Robot
{
    public Head Head { get; private set; } = new Head();
    public Arm LeftArm { get; private set; } = new Arm();
    public Arm RightArm { get; private set; } = new Arm();
    
    public void RotateHead(HeadRotation rotation) => Head.RotateHead(rotation);
    public void TiltHead(HeadTilt tilt) => Head.TiltHead(tilt);

    public void MoveElbow(Arm arm, ElbowState elbow) => arm.MoveElbow(elbow);
    public void MoveWrist(Arm arm, WristState wrist) => arm.MoveWrist(wrist);
}