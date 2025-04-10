using Moq;
using Robo.Application.Services;
using Robo.Domain.Entities;
using Robo.Domain.Enums;
using Robo.Domain.Interfaces;

namespace Robo.Tests.Unit.Application;

public class LeftArmCommandServiceTests
{
    private readonly LeftArmCommandService _leftArmCommandService;
    private readonly Mock<IRobotRepository> _robotRepositoryMock;
    
    public LeftArmCommandServiceTests()
    {
        _robotRepositoryMock = new Mock<IRobotRepository>();
        _leftArmCommandService = new LeftArmCommandService(_robotRepositoryMock.Object);
    }
    
    [Fact]
    public async Task MoveElbowAsync_WhenElbowSkipsOneState_ThrowsInvalidOperationException()
    {
        var robot = new Robot();
        
        _robotRepositoryMock.Setup(x => x.GetRobotStateAsync()).ReturnsAsync(robot);
        
        await Assert.ThrowsAsync<InvalidOperationException>(() => _leftArmCommandService.MoveElbowAsync(ElbowState.FullyBent));
    }
    
    [Fact]
    public async Task MoveWristAsync_WhenElbowIsNotFullyBent_ThrowsInvalidOperationException()
    {
        var robot = new Robot();
        
        _robotRepositoryMock.Setup(x => x.GetRobotStateAsync()).ReturnsAsync(robot);
        
        await Assert.ThrowsAsync<InvalidOperationException>(() => _leftArmCommandService.MoveWristAsync(WristState.RotatedMinus90));
    }
    
    [Fact]
    public async Task MoveWristAsync_WhenWristSkipsOneState_ThrowsInvalidOperationException()
    {
        var robot = new Robot();
        robot.LeftArm.MoveElbow(ElbowState.SlightlyBent);
        robot.LeftArm.MoveElbow(ElbowState.Bent);
        robot.LeftArm.MoveElbow(ElbowState.FullyBent);
        
        _robotRepositoryMock.Setup(x => x.GetRobotStateAsync()).ReturnsAsync(robot);
        
        await Assert.ThrowsAsync<InvalidOperationException>(() => _leftArmCommandService.MoveWristAsync(WristState.RotatedMinus90));
    }
    
    [Fact]
    public async Task MoveElbowAsync_ShouldMoveElbow()
    {
        var robot = new Robot();
        
        _robotRepositoryMock.Setup(x => x.GetRobotStateAsync()).ReturnsAsync(robot);
        
        await _leftArmCommandService.MoveElbowAsync(ElbowState.SlightlyBent);
        
        Assert.Equal(ElbowState.SlightlyBent, robot.LeftArm.Elbow);
    }
    
    [Fact]
    public async Task MoveWristAsync_ShouldMoveWrist_WhenElbowIsFullyBent()
    {
        var robot = new Robot();
        robot.LeftArm.MoveElbow(ElbowState.SlightlyBent);
        robot.LeftArm.MoveElbow(ElbowState.Bent);
        robot.LeftArm.MoveElbow(ElbowState.FullyBent);
        
        _robotRepositoryMock.Setup(x => x.GetRobotStateAsync()).ReturnsAsync(robot);
        
        await _leftArmCommandService.MoveWristAsync(WristState.RotatedMinus45);
        
        Assert.Equal(WristState.RotatedMinus45, robot.LeftArm.Wrist);
    }
}