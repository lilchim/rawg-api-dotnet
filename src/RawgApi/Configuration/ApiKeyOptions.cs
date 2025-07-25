namespace RawgApi.Configuration;

/// <summary>
/// Configuration options for API key authentication
/// </summary>
public class ApiKeyOptions
{
    public const string SectionName = "ApiKey";

    /// <summary>
    /// Whether API key authentication is required
    /// </summary>
    public bool RequireApiKey { get; set; } = false;

    /// <summary>
    /// List of valid API keys
    /// </summary>
    public List<string> ValidApiKeys { get; set; } = new();

    /// <summary>
    /// The header name to use for API key authentication
    /// </summary>
    public string HeaderName { get; set; } = "X-API-Key";

    /// <summary>
    /// The query parameter name to use for API key authentication
    /// </summary>
    public string QueryParameterName { get; set; } = "api_key";

    /// <summary>
    /// Rate limiting configuration per API key
    /// </summary>
    public RateLimitOptions RateLimit { get; set; } = new();
}

/// <summary>
/// Rate limiting configuration
/// </summary>
public class RateLimitOptions
{
    /// <summary>
    /// Maximum requests per minute per API key
    /// </summary>
    public int RequestsPerMinute { get; set; } = 100;

    /// <summary>
    /// Maximum requests per hour per API key
    /// </summary>
    public int RequestsPerHour { get; set; } = 1000;
} 