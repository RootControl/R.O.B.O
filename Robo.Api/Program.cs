using Robo.Api.Configurations;
using Robo.Api.Endpoints;
using Robo.Application.Interfaces;
using Robo.Application.Services;
using Robo.Domain.Entities;
using Robo.Domain.Interfaces;
using Robo.Infrastructure.Databases;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IRobotRepository, InMemoryRobotRepository>();
builder.Services.AddScoped<IRobotQueryService, RobotQueryService>();
builder.Services.AddScoped<IHeadCommandService, HeadCommandService>();
builder.Services.AddScoped<IArmCommandService<LeftArm>, LeftArmCommandService>();
builder.Services.AddScoped<IArmCommandService<RightArm>, RightArmCommandService>();

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "R.O.B.O Api v1"));
}

app.ConfigureExceptionHandler();
app.UseHttpsRedirection();
app.MapRobotEndpoints();
app.Run();