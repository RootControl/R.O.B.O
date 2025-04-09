using Moq;
using Robo.Application.DTOs;
using Robo.Application.Interfaces;
using Robo.Application.Services;
using Robo.Domain.Entities;
using Robo.Domain.Enums;
using Robo.Domain.Interfaces;

namespace Robo.Tests.Unit.Application;

public class RobotServiceTests
{
    private readonly Mock<IRobotRepository> _robotRepositoryMock;
    private readonly Mock<IMoveRobotArmService> _moveRobotArmServiceMock;
    private readonly Mock<IMoveRobotHeadService> _moveRobotHeadServiceMock;
    private readonly RobotService _robotService;

    public RobotServiceTests()
    {
        _robotRepositoryMock = new Mock<IRobotRepository>();
        _moveRobotArmServiceMock = new Mock<IMoveRobotArmService>();
        _moveRobotHeadServiceMock = new Mock<IMoveRobotHeadService>();
        
        _robotService = new RobotService(
            _robotRepositoryMock.Object, 
            _moveRobotHeadServiceMock.Object,
            _moveRobotArmServiceMock.Object);
    }
    
    [Fact]
    public async Task GetRobotStateAsync_ShouldReturnRobotStateDtoRested()
    {
        var robot = new Robot();
        _robotRepositoryMock.Setup(x => x.GetRobotStateAsync()).ReturnsAsync(robot);
        
        var result = await _robotService.GetRobotStateAsync();
        
        Assert.NotNull(result);
        Assert.IsType<RobotStateDto>(result);
        Assert.Equal(ElbowState.Rested, result.LeftArmState.Elbow);
        Assert.Equal(WristState.Rested, result.LeftArmState.Wrist);
        Assert.Equal(ElbowState.Rested, result.RightArmState.Elbow);
        Assert.Equal(WristState.Rested, result.RightArmState.Wrist);
        Assert.Equal(HeadRotation.Rested, result.Head.Rotation);
        Assert.Equal(HeadTilt.Rested, result.Head.Tilt);
    }
    
    [Fact]
    public async Task RotateHeadAsync_ShouldCallMoveRobotHeadService()
    {
        var robot = new Robot();
        _robotRepositoryMock.Setup(x => x.GetRobotStateAsync()).ReturnsAsync(robot);
        
        await _robotService.RotateHeadAsync(HeadRotation.Rotated45);
        
        _moveRobotHeadServiceMock.Verify(x => x.RotateHeadAsync(HeadRotation.Rotated45), Times.Once);
    }
    
    [Fact]
    public async Task TiltHeadAsync_ShouldCallMoveRobotHeadService()
    {
        var robot = new Robot();
        _robotRepositoryMock.Setup(x => x.GetRobotStateAsync()).ReturnsAsync(robot);
        
        await _robotService.TiltHeadAsync(HeadTilt.Down);
        
        _moveRobotHeadServiceMock.Verify(x => x.TiltHeadAsync(HeadTilt.Down), Times.Once);
    }
    
    [Fact]
    public async Task MoveElbowAsync_ShouldCallMoveRobotArmService()
    {
        var robot = new Robot();
        _robotRepositoryMock.Setup(x => x.GetRobotStateAsync()).ReturnsAsync(robot);
        
        await _robotService.MoveElbowAsync(robot.LeftArm, ElbowState.Bent);
        
        _moveRobotArmServiceMock.Verify(x => x.MoveElbowAsync(robot.LeftArm, ElbowState.Bent), Times.Once);
    }
    
    [Fact]
    public async Task MoveWristAsync_ShouldCallMoveRobotArmService()
    {
        var robot = new Robot();
        _robotRepositoryMock.Setup(x => x.GetRobotStateAsync()).ReturnsAsync(robot);
        
        await _robotService.MoveWristAsync(robot.LeftArm, WristState.Rotated180);
        
        _moveRobotArmServiceMock.Verify(x => x.MoveWristAsync(robot.LeftArm, WristState.Rotated180), Times.Once);
    }
}