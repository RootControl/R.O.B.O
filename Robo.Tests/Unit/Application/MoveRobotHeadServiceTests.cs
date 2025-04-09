using Moq;
using Robo.Application.Services;
using Robo.Domain.Entities;
using Robo.Domain.Enums;
using Robo.Domain.Interfaces;

namespace Robo.Tests.Unit.Application;

public class MoveRobotHeadServiceTests
{
    private readonly MoveRobotHeadService _moveRobotHeadService;
    private readonly Mock<IRobotRepository> _robotRepositoryMock;

    public MoveRobotHeadServiceTests()
    {
        _robotRepositoryMock = new Mock<IRobotRepository>();
        _moveRobotHeadService = new MoveRobotHeadService(_robotRepositoryMock.Object);
    }

    [Fact]
    public async Task RotateHeadAsync_ShouldUpdateRobotState()
    {
        _robotRepositoryMock.Setup(x => x.GetRobotStateAsync()).ReturnsAsync(new Robot());
        
        await _moveRobotHeadService.RotateHeadAsync(HeadRotation.RotatedMinus90);
        
        _robotRepositoryMock.Verify(x => x.SaveRobotStateAsync(It.IsAny<Robot>()), Times.Once);
    }
    
    [Fact]
    public async Task TiltHeadAsync_ShouldUpdateRobotState()
    {
        _robotRepositoryMock.Setup(x => x.GetRobotStateAsync()).ReturnsAsync(new Robot());
        
        await _moveRobotHeadService.TiltHeadAsync(HeadTilt.Down);
        
        _robotRepositoryMock.Verify(x => x.SaveRobotStateAsync(It.IsAny<Robot>()), Times.Once);
    }
}