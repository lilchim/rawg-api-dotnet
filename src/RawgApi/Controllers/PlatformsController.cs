using Microsoft.AspNetCore.Mvc;
using RawgApi.Models;
using RawgApi.Services;

namespace RawgApi.Controllers;

/// <summary>
/// Controller for platform-related endpoints
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PlatformsController : ControllerBase
{
    private readonly IRawgApiService _rawgApiService;
    private readonly ILogger<PlatformsController> _logger;

    public PlatformsController(IRawgApiService rawgApiService, ILogger<PlatformsController> logger)
    {
        _rawgApiService = rawgApiService;
        _logger = logger;
    }

    /// <summary>
    /// Get a list of platforms
    /// </summary>
    /// <param name="ordering">Ordering (e.g., name, -games_count)</param>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Page size</param>
    /// <returns>Paginated list of platforms</returns>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResponse<Platform>), 200)]
    public async Task<IActionResult> GetPlatforms(
        [FromQuery] string? ordering = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        try
        {
            var parameters = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(ordering))
                parameters["ordering"] = ordering;

            parameters["page"] = page.ToString();
            parameters["page_size"] = Math.Min(pageSize, 40).ToString();

            var result = await _rawgApiService.GetAsync<PaginatedResponse<Platform>>("platforms", parameters);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving platforms");
            return StatusCode(500, new { error = "Internal server error" });
        }
    }

    /// <summary>
    /// Get a specific platform by ID
    /// </summary>
    /// <param name="id">Platform ID</param>
    /// <returns>Platform details</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Platform), 200)]
    public async Task<IActionResult> GetPlatform(int id)
    {
        try
        {
            var platform = await _rawgApiService.GetAsync<Platform>($"platforms/{id}");
            return Ok(platform);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving platform with ID {PlatformId}", id);
            return StatusCode(500, new { error = "Internal server error" });
        }
    }

    /// <summary>
    /// Get games for a specific platform
    /// </summary>
    /// <param name="id">Platform ID</param>
    /// <param name="ordering">Ordering (e.g., name, -released, rating)</param>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Page size</param>
    /// <returns>Paginated list of games</returns>
    [HttpGet("{id}/games")]
    [ProducesResponseType(typeof(PaginatedResponse<Game>), 200)]
    public async Task<IActionResult> GetPlatformGames(
        int id,
        [FromQuery] string? ordering = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        try
        {
            var parameters = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(ordering))
                parameters["ordering"] = ordering;

            parameters["page"] = page.ToString();
            parameters["page_size"] = Math.Min(pageSize, 40).ToString();

            var result = await _rawgApiService.GetAsync<PaginatedResponse<Game>>($"platforms/{id}/games", parameters);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving games for platform {PlatformId}", id);
            return StatusCode(500, new { error = "Internal server error" });
        }
    }
} 