using System.Text.Json.Serialization;

namespace RawgApi.Models;

/// <summary>
/// Represents a video game
/// </summary>
public class Game : RawgEntity
{
    /// <summary>
    /// Description of the game
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Release date of the game
    /// </summary>
    [JsonPropertyName("released")]
    public DateTime? Released { get; set; }

    /// <summary>
    /// Whether the game is free to play
    /// </summary>
    [JsonPropertyName("free_to_play")]
    public bool FreeToPlay { get; set; }

    /// <summary>
    /// Metacritic score
    /// </summary>
    [JsonPropertyName("metacritic")]
    public int? Metacritic { get; set; }

    /// <summary>
    /// Metacritic URL
    /// </summary>
    [JsonPropertyName("metacritic_url")]
    public string? MetacriticUrl { get; set; }

    /// <summary>
    /// Metacritic platform
    /// </summary>
    [JsonPropertyName("metacritic_platform")]
    public string? MetacriticPlatform { get; set; }

    /// <summary>
    /// Rating from 0 to 5
    /// </summary>
    [JsonPropertyName("rating")]
    public double Rating { get; set; }

    /// <summary>
    /// Number of ratings
    /// </summary>
    [JsonPropertyName("rating_top")]
    public int RatingTop { get; set; }

    /// <summary>
    /// Number of ratings
    /// </summary>
    [JsonPropertyName("ratings_count")]
    public int RatingsCount { get; set; }

    /// <summary>
    /// Number of reviews
    /// </summary>
    [JsonPropertyName("reviews_text_count")]
    public int ReviewsTextCount { get; set; }

    /// <summary>
    /// Number of additions to wishlists
    /// </summary>
    [JsonPropertyName("added")]
    public int Added { get; set; }

    /// <summary>
    /// Number of additions by users
    /// </summary>
    [JsonPropertyName("added_by_status")]
    public AddedByStatus? AddedByStatus { get; set; }

    /// <summary>
    /// Number of playtime hours
    /// </summary>
    [JsonPropertyName("playtime")]
    public int Playtime { get; set; }

    /// <summary>
    /// Number of suggestions
    /// </summary>
    [JsonPropertyName("suggestions_count")]
    public int SuggestionsCount { get; set; }

    /// <summary>
    /// Updated date
    /// </summary>
    [JsonPropertyName("updated")]
    public DateTime Updated { get; set; }

    /// <summary>
    /// ESRB rating
    /// </summary>
    [JsonPropertyName("esrb_rating")]
    public EsrbRating? EsrbRating { get; set; }

    /// <summary>
    /// Platforms the game is available on
    /// </summary>
    [JsonPropertyName("platforms")]
    public List<PlatformInfo>? Platforms { get; set; }

    /// <summary>
    /// Stores where the game can be purchased
    /// </summary>
    [JsonPropertyName("stores")]
    public List<StoreInfo>? Stores { get; set; }

    /// <summary>
    /// Genres of the game
    /// </summary>
    [JsonPropertyName("genres")]
    public List<Genre>? Genres { get; set; }

    /// <summary>
    /// Tags associated with the game
    /// </summary>
    [JsonPropertyName("tags")]
    public List<Tag>? Tags { get; set; }

    /// <summary>
    /// Publishers of the game
    /// </summary>
    [JsonPropertyName("publishers")]
    public List<Publisher>? Publishers { get; set; }

    /// <summary>
    /// Developers of the game
    /// </summary>
    [JsonPropertyName("developers")]
    public List<Developer>? Developers { get; set; }

    /// <summary>
    /// Creators of the game
    /// </summary>
    [JsonPropertyName("creators")]
    public List<Creator>? Creators { get; set; }

    /// <summary>
    /// Screenshots of the game
    /// </summary>
    [JsonPropertyName("short_screenshots")]
    public List<Screenshot>? ShortScreenshots { get; set; }

    /// <summary>
    /// Parent platforms
    /// </summary>
    [JsonPropertyName("parent_platforms")]
    public List<ParentPlatform>? ParentPlatforms { get; set; }

    /// <summary>
    /// Requirements for different platforms
    /// </summary>
    [JsonPropertyName("requirements")]
    public Requirements? Requirements { get; set; }

    /// <summary>
    /// Website URL
    /// </summary>
    [JsonPropertyName("website")]
    public string? Website { get; set; }

    /// <summary>
    /// Reddit URL
    /// </summary>
    [JsonPropertyName("reddit_url")]
    public string? RedditUrl { get; set; }

    /// <summary>
    /// Reddit name
    /// </summary>
    [JsonPropertyName("reddit_name")]
    public string? RedditName { get; set; }

    /// <summary>
    /// Reddit description
    /// </summary>
    [JsonPropertyName("reddit_description")]
    public string? RedditDescription { get; set; }

    /// <summary>
    /// Reddit logo
    /// </summary>
    [JsonPropertyName("reddit_logo")]
    public string? RedditLogo { get; set; }

    /// <summary>
    /// Reddit count
    /// </summary>
    [JsonPropertyName("reddit_count")]
    public int RedditCount { get; set; }

    /// <summary>
    /// Twitch count
    /// </summary>
    [JsonPropertyName("twitch_count")]
    public int TwitchCount { get; set; }

    /// <summary>
    /// YouTube count
    /// </summary>
    [JsonPropertyName("youtube_count")]
    public int YoutubeCount { get; set; }

    /// <summary>
    /// Reviews count
    /// </summary>
    [JsonPropertyName("reviews_count")]
    public int ReviewsCount { get; set; }

    /// <summary>
    /// Achievements count
    /// </summary>
    [JsonPropertyName("achievements_count")]
    public int AchievementsCount { get; set; }

    /// <summary>
    /// Parents count
    /// </summary>
    [JsonPropertyName("parents_count")]
    public int ParentsCount { get; set; }

    /// <summary>
    /// Additions count
    /// </summary>
    [JsonPropertyName("additions_count")]
    public int AdditionsCount { get; set; }

    /// <summary>
    /// Game series count
    /// </summary>
    [JsonPropertyName("game_series_count")]
    public int GameSeriesCount { get; set; }

    /// <summary>
    /// User game
    /// </summary>
    [JsonPropertyName("user_game")]
    public object? UserGame { get; set; }

    /// <summary>
    /// Reviews average
    /// </summary>
    [JsonPropertyName("reviews_average")]
    public double ReviewsAverage { get; set; }

    /// <summary>
    /// Saturated color
    /// </summary>
    [JsonPropertyName("saturated_color")]
    public string? SaturatedColor { get; set; }

    /// <summary>
    /// Dominant color
    /// </summary>
    [JsonPropertyName("dominant_color")]
    public string? DominantColor { get; set; }

    /// <summary>
    /// Alternative names
    /// </summary>
    [JsonPropertyName("alternative_names")]
    public List<string>? AlternativeNames { get; set; }

    /// <summary>
    /// Ratings breakdown
    /// </summary>
    [JsonPropertyName("ratings")]
    public List<Rating>? Ratings { get; set; }

    /// <summary>
    /// Community rating
    /// </summary>
    [JsonPropertyName("community_rating")]
    public int? CommunityRating { get; set; }

    /// <summary>
    /// Average playtime
    /// </summary>
    [JsonPropertyName("avg_playtime")]
    public int AvgPlaytime { get; set; }

    /// <summary>
    /// Median playtime
    /// </summary>
    [JsonPropertyName("median_playtime")]
    public int MedianPlaytime { get; set; }

    /// <summary>
    /// TBA status
    /// </summary>
    [JsonPropertyName("tba")]
    public bool Tba { get; set; }

    /// <summary>
    /// Rating count
    /// </summary>
    [JsonPropertyName("rating_count")]
    public int RatingCount { get; set; }

    /// <summary>
    /// Clip
    /// </summary>
    [JsonPropertyName("clip")]
    public object? Clip { get; set; }

    /// <summary>
    /// Description raw
    /// </summary>
    [JsonPropertyName("description_raw")]
    public string? DescriptionRaw { get; set; }
}

/// <summary>
/// Represents added by status
/// </summary>
public class AddedByStatus
{
    [JsonPropertyName("yet")]
    public int Yet { get; set; }

    [JsonPropertyName("owned")]
    public int Owned { get; set; }

    [JsonPropertyName("beaten")]
    public int Beaten { get; set; }

    [JsonPropertyName("toplay")]
    public int ToPlay { get; set; }

    [JsonPropertyName("dropped")]
    public int Dropped { get; set; }

    [JsonPropertyName("playing")]
    public int Playing { get; set; }
}

/// <summary>
/// Represents ESRB rating
/// </summary>
public class EsrbRating : RawgEntity
{
}

/// <summary>
/// Represents platform information
/// </summary>
public class PlatformInfo
{
    [JsonPropertyName("platform")]
    public Platform Platform { get; set; } = new();

    [JsonPropertyName("requirements")]
    public Requirements? Requirements { get; set; }

    [JsonPropertyName("released_at")]
    public DateTime? ReleasedAt { get; set; }
}

/// <summary>
/// Represents a platform
/// </summary>
public class Platform : RawgEntity
{
    [JsonPropertyName("image_background")]
    public string? ImageBackground { get; set; }
}

/// <summary>
/// Represents store information
/// </summary>
public class StoreInfo
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("store")]
    public Store Store { get; set; } = new();

    [JsonPropertyName("url")]
    public string? Url { get; set; }
}

/// <summary>
/// Represents a genre
/// </summary>
public class Genre : RawgEntity
{
    [JsonPropertyName("image_background")]
    public string? ImageBackground { get; set; }
}

/// <summary>
/// Represents a screenshot
/// </summary>
public class Screenshot
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("image")]
    public string Image { get; set; } = string.Empty;
}

/// <summary>
/// Represents a parent platform
/// </summary>
public class ParentPlatform
{
    [JsonPropertyName("platform")]
    public Platform Platform { get; set; } = new();
}

/// <summary>
/// Represents requirements
/// </summary>
public class Requirements
{
    [JsonPropertyName("minimum")]
    public string? Minimum { get; set; }

    [JsonPropertyName("recommended")]
    public string? Recommended { get; set; }
}

/// <summary>
/// Represents a rating
/// </summary>
public class Rating
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("count")]
    public int Count { get; set; }

    [JsonPropertyName("percent")]
    public double Percent { get; set; }
} 