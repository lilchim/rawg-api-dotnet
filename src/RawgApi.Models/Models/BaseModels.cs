using System.Text.Json.Serialization;

namespace RawgApi.Models;

/// <summary>
/// Base class for paginated responses from RAWG API
/// </summary>
public class PaginatedResponse<T>
{
    /// <summary>
    /// Number of results returned
    /// </summary>
    [JsonPropertyName("count")]
    public int Count { get; set; }

    /// <summary>
    /// URL for the next page of results
    /// </summary>
    [JsonPropertyName("next")]
    public string? Next { get; set; }

    /// <summary>
    /// URL for the previous page of results
    /// </summary>
    [JsonPropertyName("previous")]
    public string? Previous { get; set; }

    /// <summary>
    /// The actual results
    /// </summary>
    [JsonPropertyName("results")]
    public List<T> Results { get; set; } = new();
}

/// <summary>
/// Base class for RAWG API entities
/// </summary>
public class RawgEntity
{
    /// <summary>
    /// Unique identifier
    /// </summary>
    [JsonPropertyName("id")]
    public int Id { get; set; }

    /// <summary>
    /// Name of the entity
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Slug used in URLs
    /// </summary>
    [JsonPropertyName("slug")]
    public string Slug { get; set; } = string.Empty;

    /// <summary>
    /// Background image URL
    /// </summary>
    [JsonPropertyName("background_image")]
    public string? BackgroundImage { get; set; }

    /// <summary>
    /// Games count (for platforms, genres, etc.)
    /// </summary>
    [JsonPropertyName("games_count")]
    public int? GamesCount { get; set; }

    /// <summary>
    /// Image URL
    /// </summary>
    [JsonPropertyName("image")]
    public string? Image { get; set; }

    /// <summary>
    /// Year established (for platforms, publishers, etc.)
    /// </summary>
    [JsonPropertyName("year_start")]
    public int? YearStart { get; set; }

    /// <summary>
    /// Year ended (for platforms, publishers, etc.)
    /// </summary>
    [JsonPropertyName("year_end")]
    public int? YearEnd { get; set; }
}

/// <summary>
/// Base class for game-related entities
/// </summary>
public class GameEntity : RawgEntity
{
    /// <summary>
    /// Games associated with this entity
    /// </summary>
    [JsonPropertyName("games")]
    public List<Game>? Games { get; set; }
}

/// <summary>
/// Represents a tag
/// </summary>
public class Tag : RawgEntity
{
    /// <summary>
    /// Language of the tag
    /// </summary>
    [JsonPropertyName("language")]
    public string? Language { get; set; }

    /// <summary>
    /// Image background URL
    /// </summary>
    [JsonPropertyName("image_background")]
    public string? ImageBackground { get; set; }
}

/// <summary>
/// Represents a store
/// </summary>
public class Store : RawgEntity
{
    /// <summary>
    /// Domain of the store
    /// </summary>
    [JsonPropertyName("domain")]
    public string? Domain { get; set; }

    /// <summary>
    /// Description of the store
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }
}

/// <summary>
/// Represents a creator
/// </summary>
public class Creator : RawgEntity
{
    /// <summary>
    /// Image background URL
    /// </summary>
    [JsonPropertyName("image_background")]
    public string? ImageBackground { get; set; }
}

/// <summary>
/// Represents a developer
/// </summary>
public class Developer : RawgEntity
{
    /// <summary>
    /// Image background URL
    /// </summary>
    [JsonPropertyName("image_background")]
    public string? ImageBackground { get; set; }
}

/// <summary>
/// Represents a publisher
/// </summary>
public class Publisher : RawgEntity
{
    /// <summary>
    /// Image background URL
    /// </summary>
    [JsonPropertyName("image_background")]
    public string? ImageBackground { get; set; }
} 