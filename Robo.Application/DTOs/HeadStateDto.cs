using Robo.Domain.Entities;
using Robo.Domain.Enums;

namespace Robo.Application.DTOs;

public class HeadStateDto(Head head)
{
    public HeadRotation Rotation { get; set; } = head.Rotation;
    public HeadTilt Tilt { get; set; } = head.Tilt;
}