using Robo.Domain.Entities;
using Robo.Domain.Enums;

namespace Robo.Application.DTOs;

public class HeadStateDto
{
    public HeadRotation Rotation { get; set; }
    public HeadTilt Tilt { get; set; }
    
    public HeadStateDto() { }

    public HeadStateDto(Head head)
    {
        Rotation = head.Rotation;
        Tilt = head.Tilt;
    }
}