namespace RawgApi.Configuration;

/// <summary>
/// Configuration options for the RAWG API
/// </summary>
public class RawgApiOptions
{
    public const string SectionName = "RawgApi";

    /// <summary>
    /// The RAWG API key
    /// </summary>
    public string ApiKey { get; set; } = string.Empty;

    /// <summary>
    /// The base URL for the RAWG API
    /// </summary>
    public string BaseUrl { get; set; } = "https://api.rawg.io/api";

    /// <summary>
    /// Timeout for HTTP requests in seconds
    /// </summary>
    public int TimeoutSeconds { get; set; } = 30;

    /// <summary>
    /// Maximum number of retries for failed requests
    /// </summary>
    public int MaxRetries { get; set; } = 3;
} 