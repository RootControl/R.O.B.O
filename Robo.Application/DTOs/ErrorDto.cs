using System.Text.Json.Serialization;

namespace Robo.Application.DTOs;

public class ErrorDto
{
    public string Error { get; set; }
    
    [JsonConstructor]
    public ErrorDto(string error)
    {
        Error = error;
    }
}