using Microsoft.AspNetCore.Mvc;
using RawgApi.Models;
using RawgApi.Services;

namespace RawgApi.Controllers;

/// <summary>
/// Controller for game-related endpoints
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class GamesController : ControllerBase
{
    private readonly IRawgApiService _rawgApiService;
    private readonly ILogger<GamesController> _logger;

    public GamesController(IRawgApiService rawgApiService, ILogger<GamesController> logger)
    {
        _rawgApiService = rawgApiService;
        _logger = logger;
    }

    /// <summary>
    /// Get a list of games with optional filtering
    /// </summary>
    /// <param name="search">Search query</param>
    /// <param name="searchPrecise">Precise search</param>
    /// <param name="searchExact">Exact search</param>
    /// <param name="parentPlatforms">Parent platforms (comma-separated IDs)</param>
    /// <param name="platforms">Platforms (comma-separated IDs)</param>
    /// <param name="stores">Stores (comma-separated IDs)</param>
    /// <param name="developers">Developers (comma-separated IDs)</param>
    /// <param name="publishers">Publishers (comma-separated IDs)</param>
    /// <param name="genres">Genres (comma-separated IDs)</param>
    /// <param name="tags">Tags (comma-separated IDs)</param>
    /// <param name="creators">Creators (comma-separated IDs)</param>
    /// <param name="dates">Release dates (comma-separated)</param>
    /// <param name="updated">Updated dates (comma-separated)</param>
    /// <param name="platformsCount">Number of platforms</param>
    /// <param name="metacritic">Metacritic score range (e.g., 80,90)</param>
    /// <param name="excludeCollection">Exclude collection games</param>
    /// <param name="excludeAdditions">Exclude additions</param>
    /// <param name="excludeParents">Exclude parent games</param>
    /// <param name="excludeGameSeries">Exclude game series</param>
    /// <param name="excludeStores">Exclude stores</param>
    /// <param name="ordering">Ordering (e.g., name, -released, rating)</param>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Page size (max 40)</param>
    /// <returns>Paginated list of games</returns>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResponse<Game>), 200)]
    public async Task<IActionResult> GetGames(
        [FromQuery] string? search = null,
        [FromQuery] bool? searchPrecise = null,
        [FromQuery] bool? searchExact = null,
        [FromQuery] string? parentPlatforms = null,
        [FromQuery] string? platforms = null,
        [FromQuery] string? stores = null,
        [FromQuery] string? developers = null,
        [FromQuery] string? publishers = null,
        [FromQuery] string? genres = null,
        [FromQuery] string? tags = null,
        [FromQuery] string? creators = null,
        [FromQuery] string? dates = null,
        [FromQuery] string? updated = null,
        [FromQuery] int? platformsCount = null,
        [FromQuery] string? metacritic = null,
        [FromQuery] int? excludeCollection = null,
        [FromQuery] int? excludeAdditions = null,
        [FromQuery] int? excludeParents = null,
        [FromQuery] int? excludeGameSeries = null,
        [FromQuery] string? excludeStores = null,
        [FromQuery] string? ordering = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        try
        {
            var parameters = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(search))
                parameters["search"] = search;
            if (searchPrecise.HasValue)
                parameters["search_precise"] = searchPrecise.Value.ToString().ToLower();
            if (searchExact.HasValue)
                parameters["search_exact"] = searchExact.Value.ToString().ToLower();
            if (!string.IsNullOrEmpty(parentPlatforms))
                parameters["parent_platforms"] = parentPlatforms;
            if (!string.IsNullOrEmpty(platforms))
                parameters["platforms"] = platforms;
            if (!string.IsNullOrEmpty(stores))
                parameters["stores"] = stores;
            if (!string.IsNullOrEmpty(developers))
                parameters["developers"] = developers;
            if (!string.IsNullOrEmpty(publishers))
                parameters["publishers"] = publishers;
            if (!string.IsNullOrEmpty(genres))
                parameters["genres"] = genres;
            if (!string.IsNullOrEmpty(tags))
                parameters["tags"] = tags;
            if (!string.IsNullOrEmpty(creators))
                parameters["creators"] = creators;
            if (!string.IsNullOrEmpty(dates))
                parameters["dates"] = dates;
            if (!string.IsNullOrEmpty(updated))
                parameters["updated"] = updated;
            if (platformsCount.HasValue)
                parameters["platforms_count"] = platformsCount.Value.ToString();
            if (!string.IsNullOrEmpty(metacritic))
                parameters["metacritic"] = metacritic;
            if (excludeCollection.HasValue)
                parameters["exclude_collection"] = excludeCollection.Value.ToString();
            if (excludeAdditions.HasValue)
                parameters["exclude_additions"] = excludeAdditions.Value.ToString();
            if (excludeParents.HasValue)
                parameters["exclude_parents"] = excludeParents.Value.ToString();
            if (excludeGameSeries.HasValue)
                parameters["exclude_game_series"] = excludeGameSeries.Value.ToString();
            if (!string.IsNullOrEmpty(excludeStores))
                parameters["exclude_stores"] = excludeStores;
            if (!string.IsNullOrEmpty(ordering))
                parameters["ordering"] = ordering;

            parameters["page"] = page.ToString();
            parameters["page_size"] = Math.Min(pageSize, 40).ToString();

            var result = await _rawgApiService.GetAsync<PaginatedResponse<Game>>("games", parameters);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving games");
            return StatusCode(500, new { error = "Internal server error" });
        }
    }

    /// <summary>
    /// Get a specific game by ID
    /// </summary>
    /// <param name="id">Game ID</param>
    /// <returns>Game details</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Game), 200)]
    public async Task<IActionResult> GetGame(int id)
    {
        try
        {
            var game = await _rawgApiService.GetAsync<Game>($"games/{id}");
            return Ok(game);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving game with ID {GameId}", id);
            return StatusCode(500, new { error = "Internal server error" });
        }
    }

    /// <summary>
    /// Get screenshots for a specific game
    /// </summary>
    /// <param name="id">Game ID</param>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Page size</param>
    /// <returns>Paginated list of screenshots</returns>
    [HttpGet("{id}/screenshots")]
    [ProducesResponseType(typeof(PaginatedResponse<Screenshot>), 200)]
    public async Task<IActionResult> GetGameScreenshots(int id, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        try
        {
            var parameters = new Dictionary<string, string>
            {
                ["page"] = page.ToString(),
                ["page_size"] = pageSize.ToString()
            };

            var result = await _rawgApiService.GetAsync<PaginatedResponse<Screenshot>>($"games/{id}/screenshots", parameters);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving screenshots for game {GameId}", id);
            return StatusCode(500, new { error = "Internal server error" });
        }
    }

    /// <summary>
    /// Get DLCs for a specific game
    /// </summary>
    /// <param name="id">Game ID</param>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Page size</param>
    /// <returns>Paginated list of DLCs</returns>
    [HttpGet("{id}/additions")]
    [ProducesResponseType(typeof(PaginatedResponse<Game>), 200)]
    public async Task<IActionResult> GetGameAdditions(int id, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        try
        {
            var parameters = new Dictionary<string, string>
            {
                ["page"] = page.ToString(),
                ["page_size"] = pageSize.ToString()
            };

            var result = await _rawgApiService.GetAsync<PaginatedResponse<Game>>($"games/{id}/additions", parameters);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving additions for game {GameId}", id);
            return StatusCode(500, new { error = "Internal server error" });
        }
    }

    /// <summary>
    /// Get parent games for a specific game
    /// </summary>
    /// <param name="id">Game ID</param>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Page size</param>
    /// <returns>Paginated list of parent games</returns>
    [HttpGet("{id}/parent-games")]
    [ProducesResponseType(typeof(PaginatedResponse<Game>), 200)]
    public async Task<IActionResult> GetGameParents(int id, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        try
        {
            var parameters = new Dictionary<string, string>
            {
                ["page"] = page.ToString(),
                ["page_size"] = pageSize.ToString()
            };

            var result = await _rawgApiService.GetAsync<PaginatedResponse<Game>>($"games/{id}/parent-games", parameters);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving parent games for game {GameId}", id);
            return StatusCode(500, new { error = "Internal server error" });
        }
    }

    /// <summary>
    /// Get game series for a specific game
    /// </summary>
    /// <param name="id">Game ID</param>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Page size</param>
    /// <returns>Paginated list of game series</returns>
    [HttpGet("{id}/game-series")]
    [ProducesResponseType(typeof(PaginatedResponse<Game>), 200)]
    public async Task<IActionResult> GetGameSeries(int id, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        try
        {
            var parameters = new Dictionary<string, string>
            {
                ["page"] = page.ToString(),
                ["page_size"] = pageSize.ToString()
            };

            var result = await _rawgApiService.GetAsync<PaginatedResponse<Game>>($"games/{id}/game-series", parameters);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving game series for game {GameId}", id);
            return StatusCode(500, new { error = "Internal server error" });
        }
    }

    /// <summary>
    /// Get stores for a specific game
    /// </summary>
    /// <param name="id">Game ID</param>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Page size</param>
    /// <returns>Paginated list of stores</returns>
    [HttpGet("{id}/stores")]
    [ProducesResponseType(typeof(PaginatedResponse<StoreInfo>), 200)]
    public async Task<IActionResult> GetGameStores(int id, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        try
        {
            var parameters = new Dictionary<string, string>
            {
                ["page"] = page.ToString(),
                ["page_size"] = pageSize.ToString()
            };

            var result = await _rawgApiService.GetAsync<PaginatedResponse<StoreInfo>>($"games/{id}/stores", parameters);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving stores for game {GameId}", id);
            return StatusCode(500, new { error = "Internal server error" });
        }
    }
} 