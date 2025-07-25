using Microsoft.AspNetCore.Mvc;
using RawgApi.Models;
using RawgApi.Services;

namespace RawgApi.Controllers;

/// <summary>
/// Controller for genre-related endpoints
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class GenresController : ControllerBase
{
    private readonly IRawgApiService _rawgApiService;
    private readonly ILogger<GenresController> _logger;

    public GenresController(IRawgApiService rawgApiService, ILogger<GenresController> logger)
    {
        _rawgApiService = rawgApiService;
        _logger = logger;
    }

    /// <summary>
    /// Get a list of genres
    /// </summary>
    /// <param name="ordering">Ordering (e.g., name, -games_count)</param>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Page size</param>
    /// <returns>Paginated list of genres</returns>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResponse<Genre>), 200)]
    public async Task<IActionResult> GetGenres(
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

            var result = await _rawgApiService.GetAsync<PaginatedResponse<Genre>>("genres", parameters);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving genres");
            return StatusCode(500, new { error = "Internal server error" });
        }
    }

    /// <summary>
    /// Get a specific genre by ID
    /// </summary>
    /// <param name="id">Genre ID</param>
    /// <returns>Genre details</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Genre), 200)]
    public async Task<IActionResult> GetGenre(int id)
    {
        try
        {
            var genre = await _rawgApiService.GetAsync<Genre>($"genres/{id}");
            return Ok(genre);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving genre with ID {GenreId}", id);
            return StatusCode(500, new { error = "Internal server error" });
        }
    }

    /// <summary>
    /// Get games for a specific genre
    /// </summary>
    /// <param name="id">Genre ID</param>
    /// <param name="ordering">Ordering (e.g., name, -released, rating)</param>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Page size</param>
    /// <returns>Paginated list of games</returns>
    [HttpGet("{id}/games")]
    [ProducesResponseType(typeof(PaginatedResponse<Game>), 200)]
    public async Task<IActionResult> GetGenreGames(
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

            var result = await _rawgApiService.GetAsync<PaginatedResponse<Game>>($"genres/{id}/games", parameters);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving games for genre {GenreId}", id);
            return StatusCode(500, new { error = "Internal server error" });
        }
    }
} 