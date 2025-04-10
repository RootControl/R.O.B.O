using Robo.Application.Commands;
using Robo.Application.Interfaces;
using Robo.Domain.Entities;

namespace Robo.Api.Endpoints;

public static class RobotEndpoints
{
    public static void MapRobotEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/robot")
            .WithTags("R.O.B.O")
            .WithOpenApi();
        
        group.MapGet("/", async (IRobotQueryService robotQueryService) =>
        {
            var result = await robotQueryService.GetRobotStateAsync();
            return Results.Ok(result);
        })
        .WithName("GetRobotState")
        .WithDescription("Get the current state of the robot");
        
        group.MapPost("/head/rotate", async (IHeadCommandService headCommandService, 
                RotateHeadCommand command, 
                IRobotQueryService robotQueryService) =>
        {
            await headCommandService.RotateHeadAsync(command.Rotation);
            return Results.Ok(await robotQueryService.GetRobotStateAsync());
        })
        .WithName("RotateHead")
        .WithDescription("Rotate the head of the robot");
        
        group.MapPost("/head/tilt", async (IHeadCommandService headCommandService, 
                TiltHeadCommand command, 
                IRobotQueryService robotQueryService) =>
        {
            await headCommandService.TiltHeadAsync(command.Tilt);
            return Results.Ok(await robotQueryService.GetRobotStateAsync());
        })
        .WithName("TiltHead")
        .WithDescription("Tilt the head of the robot");
        
        group.MapPost("/left-arm/elbow", async (IArmCommandService<LeftArm> leftArmCommandService, 
            MoveElbowCommand command,
            IRobotQueryService robotQueryService) =>
        {
            await leftArmCommandService.MoveElbowAsync(command.Elbow);
            return Results.Ok(await robotQueryService.GetRobotStateAsync());
        })
        .WithName("MoveLeftArmElbow")
        .WithDescription("Move the elbow of the left arm of the robot");
        
        group.MapPost("/left-arm/wrist", async (IArmCommandService<LeftArm> leftArmCommandService, 
            MoveWristCommand command,
            IRobotQueryService robotQueryService) =>
        {
            await leftArmCommandService.MoveWristAsync(command.Wrist);
            return Results.Ok(await robotQueryService.GetRobotStateAsync());
        })
        .WithName("MoveLeftArmWrist")
        .WithDescription("Move the wrist of the left arm of the robot");
        
        group.MapPost("/right-arm/elbow", async (IArmCommandService<RightArm> rightArmCommandService, 
            MoveElbowCommand command,
            IRobotQueryService robotQueryService) =>
        {
            await rightArmCommandService.MoveElbowAsync(command.Elbow);
            return Results.Ok(await robotQueryService.GetRobotStateAsync());
        })
        .WithName("MoveRightArmElbow")
        .WithDescription("Move the elbow of the right arm of the robot");
        
        group.MapPost("/right-arm/wrist", async (IArmCommandService<RightArm> rightArmCommandService, 
            MoveWristCommand command,
            IRobotQueryService robotQueryService) =>
        {
            await rightArmCommandService.MoveWristAsync(command.Wrist);
            return Results.Ok(await robotQueryService.GetRobotStateAsync());
        })
        .WithName("MoveRightArmWrist")
        .WithDescription("Move the wrist of the right arm of the robot");
    }
}