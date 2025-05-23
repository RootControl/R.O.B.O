using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Robo.Application.DTOs;
using Robo.Domain.Enums;
using System.Text.Json;

namespace Robo.Web.Pages;

public class IndexModel(IHttpClientFactory httpClientFactory) : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public RobotStateDto? RobotState { get; set; }
    
    [TempData]
    public string? ErrorMessage { get; set; }

    public async Task OnGetAsync()
    {
        await SendQueryAsync();
    }

    public async Task<IActionResult> OnPostRotateHeadAsync(HeadRotation rotation)
    {
        await SendCommandAsync("head/rotate", new { Rotation = rotation });
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostTiltHeadAsync(HeadTilt tilt)
    {
        await SendCommandAsync("head/tilt", new { Tilt = tilt });
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostMoveElbowAsync(string side, ElbowState elbow)
    {
        await SendCommandAsync($"{side}/elbow", new { Elbow = elbow });
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostMoveWristAsync(string side, WristState wrist)
    {
        await SendCommandAsync($"{side}/wrist", new { Wrist = wrist });
        return RedirectToPage();
    }

    private async Task SendQueryAsync(string endpoint = "")
    {
        var client = _httpClientFactory.CreateClient("RobotApi");
        var response = await client.GetAsync(endpoint);
            
        if (response.IsSuccessStatusCode)
            RobotState = await response.Content.ReadFromJsonAsync<RobotStateDto>(_jsonOptions);
        else
        {
            var error = await response.Content.ReadFromJsonAsync<ErrorDto>();
            ErrorMessage = error?.Error;
        }
    }

    private async Task SendCommandAsync(string endpoint, object requestBody)
    {
        var client = _httpClientFactory.CreateClient("RobotApi");
        var json = JsonSerializer.Serialize(requestBody);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await client.PostAsync(endpoint, content);
            
        if (response.IsSuccessStatusCode)
            RobotState = await response.Content.ReadFromJsonAsync<RobotStateDto>(_jsonOptions);
        else
        {
            var error = await response.Content.ReadFromJsonAsync<ErrorDto>();
            ErrorMessage = error?.Error;
        }
    }
}