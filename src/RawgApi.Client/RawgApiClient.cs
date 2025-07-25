using Microsoft.Extensions.Logging;
using RawgApi.Models;
using System.Text.Json;

namespace RawgApi.Client;

/// <summary>
/// Client for interacting with the RAWG API proxy
/// </summary>
public class RawgApiClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<RawgApiClient> _logger;
    private readonly JsonSerializerOptions _jsonOptions;

    public RawgApiClient(HttpClient httpClient, ILogger<RawgApiClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    /// <summary>
    /// Get a list of games with optional filtering
    /// </summary>
    /// <param name="search">Search query</param>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Page size</param>
    /// <returns>Paginated list of games</returns>
    public async Task<PaginatedResponse<Game>> GetGamesAsync(
        string? search = null,
        int page = 1,
        int pageSize = 20)
    {
        var parameters = new List<string>();
        
        if (!string.IsNullOrEmpty(search))
            parameters.Add($"search={Uri.EscapeDataString(search)}");
        if (page > 1)
            parameters.Add($"page={page}");
        if (pageSize != 20)
            parameters.Add($"page_size={pageSize}");

        var queryString = parameters.Count > 0 ? "?" + string.Join("&", parameters) : "";
        var response = await _httpClient.GetAsync($"/api/games{queryString}");
        
        response.EnsureSuccessStatusCode();
        
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<PaginatedResponse<Game>>(content, _jsonOptions) 
            ?? throw new InvalidOperationException("Failed to deserialize games response");
    }

    /// <summary>
    /// Get a specific game by ID
    /// </summary>
    /// <param name="id">Game ID</param>
    /// <returns>Game details</returns>
    public async Task<Game> GetGameAsync(int id)
    {
        var response = await _httpClient.GetAsync($"/api/games/{id}");
        
        response.EnsureSuccessStatusCode();
        
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<Game>(content, _jsonOptions) 
            ?? throw new InvalidOperationException("Failed to deserialize game response");
    }

    /// <summary>
    /// Get screenshots for a specific game
    /// </summary>
    /// <param name="id">Game ID</param>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Page size</param>
    /// <returns>Paginated list of screenshots</returns>
    public async Task<PaginatedResponse<Screenshot>> GetGameScreenshotsAsync(
        int id,
        int page = 1,
        int pageSize = 20)
    {
        var parameters = new List<string>();
        
        if (page > 1)
            parameters.Add($"page={page}");
        if (pageSize != 20)
            parameters.Add($"page_size={pageSize}");

        var queryString = parameters.Count > 0 ? "?" + string.Join("&", parameters) : "";
        var response = await _httpClient.GetAsync($"/api/games/{id}/screenshots{queryString}");
        
        response.EnsureSuccessStatusCode();
        
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<PaginatedResponse<Screenshot>>(content, _jsonOptions) 
            ?? throw new InvalidOperationException("Failed to deserialize screenshots response");
    }

    /// <summary>
    /// Get DLCs for a specific game
    /// </summary>
    /// <param name="id">Game ID</param>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Page size</param>
    /// <returns>Paginated list of DLCs</returns>
    public async Task<PaginatedResponse<Game>> GetGameAdditionsAsync(
        int id,
        int page = 1,
        int pageSize = 20)
    {
        var parameters = new List<string>();
        
        if (page > 1)
            parameters.Add($"page={page}");
        if (pageSize != 20)
            parameters.Add($"page_size={pageSize}");

        var queryString = parameters.Count > 0 ? "?" + string.Join("&", parameters) : "";
        var response = await _httpClient.GetAsync($"/api/games/{id}/additions{queryString}");
        
        response.EnsureSuccessStatusCode();
        
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<PaginatedResponse<Game>>(content, _jsonOptions) 
            ?? throw new InvalidOperationException("Failed to deserialize additions response");
    }

    /// <summary>
    /// Get stores for a specific game
    /// </summary>
    /// <param name="id">Game ID</param>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Page size</param>
    /// <returns>Paginated list of stores</returns>
    public async Task<PaginatedResponse<StoreInfo>> GetGameStoresAsync(
        int id,
        int page = 1,
        int pageSize = 20)
    {
        var parameters = new List<string>();
        
        if (page > 1)
            parameters.Add($"page={page}");
        if (pageSize != 20)
            parameters.Add($"page_size={pageSize}");

        var queryString = parameters.Count > 0 ? "?" + string.Join("&", parameters) : "";
        var response = await _httpClient.GetAsync($"/api/games/{id}/stores{queryString}");
        
        response.EnsureSuccessStatusCode();
        
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<PaginatedResponse<StoreInfo>>(content, _jsonOptions) 
            ?? throw new InvalidOperationException("Failed to deserialize stores response");
    }

    /// <summary>
    /// Get platforms
    /// </summary>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Page size</param>
    /// <returns>Paginated list of platforms</returns>
    public async Task<PaginatedResponse<Platform>> GetPlatformsAsync(
        int page = 1,
        int pageSize = 20)
    {
        var parameters = new List<string>();
        
        if (page > 1)
            parameters.Add($"page={page}");
        if (pageSize != 20)
            parameters.Add($"page_size={pageSize}");

        var queryString = parameters.Count > 0 ? "?" + string.Join("&", parameters) : "";
        var response = await _httpClient.GetAsync($"/api/platforms{queryString}");
        
        response.EnsureSuccessStatusCode();
        
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<PaginatedResponse<Platform>>(content, _jsonOptions) 
            ?? throw new InvalidOperationException("Failed to deserialize platforms response");
    }

    /// <summary>
    /// Get genres
    /// </summary>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Page size</param>
    /// <returns>Paginated list of genres</returns>
    public async Task<PaginatedResponse<Genre>> GetGenresAsync(
        int page = 1,
        int pageSize = 20)
    {
        var parameters = new List<string>();
        
        if (page > 1)
            parameters.Add($"page={page}");
        if (pageSize != 20)
            parameters.Add($"page_size={pageSize}");

        var queryString = parameters.Count > 0 ? "?" + string.Join("&", parameters) : "";
        var response = await _httpClient.GetAsync($"/api/genres{queryString}");
        
        response.EnsureSuccessStatusCode();
        
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<PaginatedResponse<Genre>>(content, _jsonOptions) 
            ?? throw new InvalidOperationException("Failed to deserialize genres response");
    }

    /// <summary>
    /// Get API status
    /// </summary>
    /// <returns>API status information</returns>
    public async Task<ApiStatus> GetStatusAsync()
    {
        var response = await _httpClient.GetAsync("/api/status");
        response.EnsureSuccessStatusCode();
        
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<ApiStatus>(content, _jsonOptions) 
            ?? throw new InvalidOperationException("Failed to deserialize status response");
    }

    /// <summary>
    /// Get health check status
    /// </summary>
    /// <returns>Health check response</returns>
    public async Task<HealthStatus> GetHealthAsync()
    {
        var response = await _httpClient.GetAsync("/api/status/health");
        response.EnsureSuccessStatusCode();
        
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<HealthStatus>(content, _jsonOptions) 
            ?? throw new InvalidOperationException("Failed to deserialize health response");
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