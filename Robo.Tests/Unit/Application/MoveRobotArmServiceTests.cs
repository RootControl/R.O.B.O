using Moq;
using Robo.Application.Interfaces;
using Robo.Application.Services;
using Robo.Domain.Entities;
using Robo.Domain.Enums;
using Robo.Domain.Interfaces;

namespace Robo.Tests.Unit.Application;

public class MoveRobotArmServiceTests
{
    private readonly IMoveRobotArmService _moveRobotArmService;
    private readonly Mock<IRobotRepository> _robotRepositoryMock;

    public MoveRobotArmServiceTests()
    {
        _robotRepositoryMock = new Mock<IRobotRepository>();
        _moveRobotArmService = new MoveRobotArmService(_robotRepositoryMock.Object);
    }
    
    [Fact]
    public async Task MoveElbowAsync_ShouldUpdateRobotState()
    {
        _robotRepositoryMock.Setup(x => x.GetRobotStateAsync()).ReturnsAsync(new Robot());
        
        await _moveRobotArmService.MoveElbowAsync(new Arm(), ElbowState.SlightlyBent);
        
        _robotRepositoryMock.Verify(x => x.SaveRobotStateAsync(It.IsAny<Robot>()), Times.Once);
    }
    
    [Fact]
    public async Task MoveWristAsync_ShouldUpdateRobotState_WhenElbowIsFullyBent()
    {
        var fullyBentArm = new Arm();
        fullyBentArm.MoveElbow(ElbowState.SlightlyBent);
        fullyBentArm.MoveElbow(ElbowState.Bent);
        fullyBentArm.MoveElbow(ElbowState.FullyBent);
        
        _robotRepositoryMock.Setup(x => x.GetRobotStateAsync()).ReturnsAsync(new Robot());
        
        await _moveRobotArmService.MoveWristAsync(fullyBentArm, WristState.RotatedMinus45);
        
        _robotRepositoryMock.Verify(x => x.SaveRobotStateAsync(It.IsAny<Robot>()), Times.Once);
    }
    
    [Fact]
    public async Task MoveWristAsync_ShouldThrowException_WhenElbowIsNotFullyBent()
    {
        var notFullyBentArm = new Arm();
        notFullyBentArm.MoveElbow(ElbowState.SlightlyBent);
        
        _robotRepositoryMock.Setup(x => x.GetRobotStateAsync()).ReturnsAsync(new Robot());
        
        await Assert.ThrowsAsync<InvalidOperationException>(() => _moveRobotArmService.MoveWristAsync(notFullyBentArm, WristState.RotatedMinus45));
    }
    
    [Fact]
    public async Task MoveWristAsync_ShouldThrowException_WhenWristIsNotNextToCurrentState()
    {
        _robotRepositoryMock.Setup(x => x.GetRobotStateAsync()).ReturnsAsync(new Robot());
        
        await Assert.ThrowsAsync<InvalidOperationException>(() => _moveRobotArmService.MoveWristAsync(new Arm(), WristState.RotatedMinus45));
    }
    
    [Fact]
    public async Task MoveElbowAsync_ShouldThrowException_WhenElbowIsNotNextToCurrentState()
    {
        _robotRepositoryMock.Setup(x => x.GetRobotStateAsync()).ReturnsAsync(new Robot());
        
        await Assert.ThrowsAsync<InvalidOperationException>(() => _moveRobotArmService.MoveElbowAsync(new Arm(), ElbowState.Bent));
    }
}