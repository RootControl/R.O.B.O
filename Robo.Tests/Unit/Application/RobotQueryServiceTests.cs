using Moq;
using Robo.Application.Services;
using Robo.Domain.Entities;
using Robo.Domain.Enums;
using Robo.Domain.Interfaces;

namespace Robo.Tests.Unit.Application;

public class RobotQueryServiceTests
{
    private readonly RobotQueryService _robotQueryService;
    private readonly Mock<IRobotRepository> _robotRepositoryMock;
    
    public RobotQueryServiceTests()
    {
        _robotRepositoryMock = new Mock<IRobotRepository>();
        _robotQueryService = new RobotQueryService(_robotRepositoryMock.Object);
    }
    
    [Fact]
    public async Task GetRobotStateAsync_ShouldReturnRobotStateDto()
    {
        var robot = new Robot();
        
        _robotRepositoryMock.Setup(x => x.GetRobotStateAsync()).ReturnsAsync(robot);
        
        var result = await _robotQueryService.GetRobotStateAsync();
        
        Assert.Equal(robot.Head.Rotation, result.Head.Rotation);
        Assert.Equal(robot.Head.Tilt, result.Head.Tilt);
        Assert.Equal(robot.LeftArm.Elbow, result.LeftArmState.Elbow);
        Assert.Equal(robot.LeftArm.Wrist, result.LeftArmState.Wrist);
        Assert.Equal(robot.RightArm.Elbow, result.RightArmState.Elbow);
        Assert.Equal(robot.RightArm.Wrist, result.RightArmState.Wrist);
    }
    
    [Fact]
    public async Task GetRobotStateAsync_ShouldReturnRobotStateDto_WhenRobotStateIsChanged()
    {
        var robot = new Robot();
        robot.Head.RotateHead(HeadRotation.RotatedMinus45);
        robot.Head.TiltHead(HeadTilt.Up);
        robot.LeftArm.MoveElbow(ElbowState.SlightlyBent);
        robot.RightArm.MoveElbow(ElbowState.SlightlyBent);
        robot.RightArm.MoveElbow(ElbowState.Bent);
        robot.RightArm.MoveElbow(ElbowState.FullyBent);
        robot.RightArm.MoveWrist(WristState.RotatedMinus45);
        
        _robotRepositoryMock.Setup(x => x.GetRobotStateAsync()).ReturnsAsync(robot);
        
        var result = await _robotQueryService.GetRobotStateAsync();
        
        Assert.Equal(robot.Head.Rotation, result.Head.Rotation);
        Assert.Equal(robot.Head.Tilt, result.Head.Tilt);
        Assert.Equal(robot.LeftArm.Elbow, result.LeftArmState.Elbow);
        Assert.Equal(robot.LeftArm.Wrist, result.LeftArmState.Wrist);
        Assert.Equal(robot.RightArm.Elbow, result.RightArmState.Elbow);
        Assert.Equal(robot.RightArm.Wrist, result.RightArmState.Wrist);
    }
}