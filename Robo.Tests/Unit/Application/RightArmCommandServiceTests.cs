using Moq;
using Robo.Application.Services;
using Robo.Domain.Entities;
using Robo.Domain.Enums;
using Robo.Domain.Interfaces;

namespace Robo.Tests.Unit.Application;

public class RightArmCommandServiceTests
{
    private readonly RightArmCommandService _rightArmCommandService;
    private readonly Mock<IRobotRepository> _robotRepositoryMock;
    
    public RightArmCommandServiceTests()
    {
        _robotRepositoryMock = new Mock<IRobotRepository>();
        _rightArmCommandService = new RightArmCommandService(_robotRepositoryMock.Object);
    }
    
    [Fact]
    public async Task MoveElbowAsync_WhenElbowSkipsOneState_ThrowsInvalidOperationException()
    {
        var robot = new Robot();
        
        _robotRepositoryMock.Setup(x => x.GetRobotStateAsync()).ReturnsAsync(robot);
        
        await Assert.ThrowsAsync<InvalidOperationException>(() => _rightArmCommandService.MoveElbowAsync(ElbowState.FullyBent));
    }
    
    [Fact]
    public async Task MoveWristAsync_WhenElbowIsNotFullyBent_ThrowsInvalidOperationException()
    {
        var robot = new Robot();
        
        _robotRepositoryMock.Setup(x => x.GetRobotStateAsync()).ReturnsAsync(robot);
        
        await Assert.ThrowsAsync<InvalidOperationException>(() => _rightArmCommandService.MoveWristAsync(WristState.RotatedMinus90));
    }
    
    [Fact]
    public async Task MoveWristAsync_WhenWristSkipsOneState_ThrowsInvalidOperationException()
    {
        var robot = new Robot();
        robot.RightArm.MoveElbow(ElbowState.SlightlyBent);
        robot.RightArm.MoveElbow(ElbowState.Bent);
        robot.RightArm.MoveElbow(ElbowState.FullyBent);
        
        _robotRepositoryMock.Setup(x => x.GetRobotStateAsync()).ReturnsAsync(robot);
        
        await Assert.ThrowsAsync<InvalidOperationException>(() => _rightArmCommandService.MoveWristAsync(WristState.RotatedMinus90));
    }
    
    [Fact]
    public async Task MoveElbowAsync_ShouldMoveElbow()
    {
        var robot = new Robot();
        
        _robotRepositoryMock.Setup(x => x.GetRobotStateAsync()).ReturnsAsync(robot);
        
        await _rightArmCommandService.MoveElbowAsync(ElbowState.SlightlyBent);
        
        Assert.Equal(ElbowState.SlightlyBent, robot.RightArm.Elbow);
    }
    
    [Fact]
    public async Task MoveWristAsync_ShouldMoveWrist_WhenElbowIsFullyBent()
    {
        var robot = new Robot();
        robot.RightArm.MoveElbow(ElbowState.SlightlyBent);
        robot.RightArm.MoveElbow(ElbowState.Bent);
        robot.RightArm.MoveElbow(ElbowState.FullyBent);
        
        _robotRepositoryMock.Setup(x => x.GetRobotStateAsync()).ReturnsAsync(robot);
        
        await _rightArmCommandService.MoveWristAsync(WristState.RotatedMinus45);
        
        Assert.Equal(WristState.RotatedMinus45, robot.RightArm.Wrist);
    }
}