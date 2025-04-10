using Moq;
using Robo.Application.Services;
using Robo.Domain.Entities;
using Robo.Domain.Enums;
using Robo.Domain.Interfaces;

namespace Robo.Tests.Unit.Application;

public class HeadCommandServiceTests
{
    private readonly HeadCommandService _headCommandService;
    private readonly Mock<IRobotRepository> _robotRepositoryMock;
    
    public HeadCommandServiceTests()
    {
        _robotRepositoryMock = new Mock<IRobotRepository>();
        _headCommandService = new HeadCommandService(_robotRepositoryMock.Object);
    }
    
    [Fact]
    public async Task RotateHeadAsync_WhenHeadIsTiltedDown_ThrowsInvalidOperationException()
    {
        var robot = new Robot();
        robot.Head.TiltHead(HeadTilt.Down);
        
        _robotRepositoryMock.Setup(x => x.GetRobotStateAsync()).ReturnsAsync(robot);
        
        await Assert.ThrowsAsync<InvalidOperationException>(() => _headCommandService.RotateHeadAsync(HeadRotation.RotatedMinus45));
    }
    
    [Fact]
    public async Task RotateHeadAsync_WhenHeadIsTiltedUp_RotatesHead()
    {
        var robot = new Robot();
        robot.Head.TiltHead(HeadTilt.Up);
        
        _robotRepositoryMock.Setup(x => x.GetRobotStateAsync()).ReturnsAsync(robot);
        
        await _headCommandService.RotateHeadAsync(HeadRotation.RotatedMinus45);
        
        Assert.Equal(HeadRotation.RotatedMinus45, robot.Head.Rotation);
    }
    
    [Fact]
    public async Task RotateHeadAsync_WhenHeadIsTiltedRested_RotatesHead()
    {
        var robot = new Robot();
        
        _robotRepositoryMock.Setup(x => x.GetRobotStateAsync()).ReturnsAsync(robot);
        
        await _headCommandService.RotateHeadAsync(HeadRotation.RotatedMinus45);
        
        Assert.Equal(HeadRotation.RotatedMinus45, robot.Head.Rotation);
    }
    
    [Fact]
    public async Task RotateHeadAsync_WhenHeadSkipsOneState_ThrowsInvalidOperationException()
    {
        var robot = new Robot();
        
        _robotRepositoryMock.Setup(x => x.GetRobotStateAsync()).ReturnsAsync(robot);
        
        await Assert.ThrowsAsync<InvalidOperationException>(() => _headCommandService.RotateHeadAsync(HeadRotation.RotatedMinus90));
    }
    
    [Fact]
    public async Task TiltHeadAsync_WhenHeadSkipsOneState_ThrowsInvalidOperationException()
    {
        var robot = new Robot();
        robot.Head.TiltHead(HeadTilt.Down);
        
        _robotRepositoryMock.Setup(x => x.GetRobotStateAsync()).ReturnsAsync(robot);
        
        await Assert.ThrowsAsync<InvalidOperationException>(() => _headCommandService.TiltHeadAsync(HeadTilt.Up));
    }
    
    [Fact]
    public async Task TiltHeadAsync_WhenHeadIsTiltedDown_TiltsHead()
    {
        var robot = new Robot();
        robot.Head.TiltHead(HeadTilt.Down);
        
        _robotRepositoryMock.Setup(x => x.GetRobotStateAsync()).ReturnsAsync(robot);
        
        await _headCommandService.TiltHeadAsync(HeadTilt.Rested);
        
        Assert.Equal(HeadTilt.Rested, robot.Head.Tilt);
    }
    
    [Fact]
    public async Task TiltHeadAsync_WhenHeadIsRested_TiltsHead()
    {
        var robot = new Robot();
        
        _robotRepositoryMock.Setup(x => x.GetRobotStateAsync()).ReturnsAsync(robot);
        
        await _headCommandService.TiltHeadAsync(HeadTilt.Up);
        
        Assert.Equal(HeadTilt.Up, robot.Head.Tilt);
    }
}