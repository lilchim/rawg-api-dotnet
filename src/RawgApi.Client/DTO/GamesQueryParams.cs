namespace RawgApi.Client.DTO;

/// <summary>
/// Query parameters for games endpoint
/// </summary>
public class GamesQueryParameters
{
    /// <summary>
    /// Search query
    /// </summary>
    public string? Search { get; set; }

    /// <summary>
    /// Enable precise search
    /// </summary>
    public bool? SearchPrecise { get; set; }

    /// <summary>
    /// Enable exact search
    /// </summary>
    public bool? SearchExact { get; set; }

    /// <summary>
    /// Parent platforms (comma-separated IDs)
    /// </summary>
    public string? ParentPlatforms { get; set; }

    /// <summary>
    /// Platforms (comma-separated IDs)
    /// </summary>
    public string? Platforms { get; set; }

    /// <summary>
    /// Stores (comma-separated IDs)
    /// </summary>
    public string? Stores { get; set; }

    /// <summary>
    /// Developers (comma-separated IDs)
    /// </summary>
    public string? Developers { get; set; }

    /// <summary>
    /// Publishers (comma-separated IDs)
    /// </summary>
    public string? Publishers { get; set; }

    /// <summary>
    /// Genres (comma-separated IDs)
    /// </summary>
    public string? Genres { get; set; }

    /// <summary>
    /// Tags (comma-separated IDs)
    /// </summary>
    public string? Tags { get; set; }

    /// <summary>
    /// Creators (comma-separated IDs)
    /// </summary>
    public string? Creators { get; set; }

    /// <summary>
    /// Release dates (comma-separated)
    /// </summary>
    public string? Dates { get; set; }

    /// <summary>
    /// Updated dates (comma-separated)
    /// </summary>
    public string? Updated { get; set; }

    /// <summary>
    /// Number of platforms
    /// </summary>
    public int? PlatformsCount { get; set; }

    /// <summary>
    /// Metacritic score range (e.g., 80,90)
    /// </summary>
    public string? Metacritic { get; set; }

    /// <summary>
    /// Exclude collection games
    /// </summary>
    public int? ExcludeCollection { get; set; }

    /// <summary>
    /// Exclude additions
    /// </summary>
    public int? ExcludeAdditions { get; set; }

    /// <summary>
    /// Exclude parent games
    /// </summary>
    public int? ExcludeParents { get; set; }

    /// <summary>
    /// Exclude game series
    /// </summary>
    public int? ExcludeGameSeries { get; set; }

    /// <summary>
    /// Exclude stores (comma-separated IDs)
    /// </summary>
    public string? ExcludeStores { get; set; }

    /// <summary>
    /// Ordering (e.g., name, -released, rating)
    /// </summary>
    public string? Ordering { get; set; }

    /// <summary>
    /// Page number
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    /// Page size (max 40)
    /// </summary>
    public int PageSize { get; set; } = 20;
}