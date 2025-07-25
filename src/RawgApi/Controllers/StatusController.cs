using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RawgApi.Configuration;
using RawgApi.Services;

namespace RawgApi.Controllers;

/// <summary>
/// Controller for API status and health checks
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class StatusController : ControllerBase
{
    private readonly IRawgApiService _rawgApiService;
    private readonly IOptions<RawgApiOptions> _rawgOptions;
    private readonly IOptions<ApiKeyOptions> _apiKeyOptions;
    private readonly ILogger<StatusController> _logger;

    public StatusController(
        IRawgApiService rawgApiService,
        IOptions<RawgApiOptions> rawgOptions,
        IOptions<ApiKeyOptions> apiKeyOptions,
        ILogger<StatusController> logger)
    {
        _rawgApiService = rawgApiService;
        _rawgOptions = rawgOptions;
        _apiKeyOptions = apiKeyOptions;
        _logger = logger;
    }

    /// <summary>
    /// Get the overall API status
    /// </summary>
    /// <returns>API status information</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiStatus), 200)]
    public async Task<IActionResult> GetStatus()
    {
        try
        {
            var status = new ApiStatus
            {
                Status = "OK",
                Timestamp = DateTime.UtcNow,
                Version = "1.0.0",
                RawgApiConfigured = !string.IsNullOrEmpty(_rawgOptions.Value.ApiKey),
                ApiKeyAuthenticationEnabled = _apiKeyOptions.Value.RequireApiKey,
                ValidApiKeysCount = _apiKeyOptions.Value.ValidApiKeys.Count
            };

            // Test RAWG API connectivity if configured
            if (status.RawgApiConfigured)
            {
                try
                {
                    await _rawgApiService.GetAsync("games", new Dictionary<string, string> { { "page_size", "1" } });
                    status.RawgApiStatus = "Connected";
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "RAWG API connectivity test failed");
                    status.RawgApiStatus = "Error";
                    status.Status = "Degraded";
                }
            }
            else
            {
                status.RawgApiStatus = "Not Configured";
            }

            return Ok(status);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting API status");
            return StatusCode(500, new { error = "Internal server error" });
        }
    }

    /// <summary>
    /// Get health check status
    /// </summary>
    /// <returns>Health check response</returns>
    [HttpGet("health")]
    [ProducesResponseType(typeof(HealthStatus), 200)]
    public IActionResult GetHealth()
    {
        var health = new HealthStatus
        {
            Status = "Healthy",
            Timestamp = DateTime.UtcNow,
            Checks = new Dictionary<string, string>
            {
                ["rawg_api_configured"] = !string.IsNullOrEmpty(_rawgOptions.Value.ApiKey) ? "OK" : "Not Configured",
                ["api_key_auth"] = _apiKeyOptions.Value.RequireApiKey ? "Enabled" : "Disabled"
            }
        };

        return Ok(health);
    }
}

/// <summary>
/// API status response model
/// </summary>
public class ApiStatus
{
    public string Status { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public string Version { get; set; } = string.Empty;
    public bool RawgApiConfigured { get; set; }
    public string RawgApiStatus { get; set; } = string.Empty;
    public bool ApiKeyAuthenticationEnabled { get; set; }
    public int ValidApiKeysCount { get; set; }
}

/// <summary>
/// Health check response model
/// </summary>
public class HealthStatus
{
    public string Status { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public Dictionary<string, string> Checks { get; set; } = new();
} 