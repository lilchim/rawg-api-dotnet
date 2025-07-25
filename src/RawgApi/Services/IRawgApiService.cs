using System.Text.Json;

namespace RawgApi.Services;

/// <summary>
/// Service interface for interacting with the RAWG API
/// </summary>
public interface IRawgApiService
{
    /// <summary>
    /// Makes a GET request to the RAWG API
    /// </summary>
    /// <param name="endpoint">The API endpoint (without base URL)</param>
    /// <param name="parameters">Optional query parameters</param>
    /// <returns>The API response as a JsonElement</returns>
    Task<JsonElement> GetAsync(string endpoint, Dictionary<string, string>? parameters = null);

    /// <summary>
    /// Makes a GET request to the RAWG API and deserializes the response
    /// </summary>
    /// <typeparam name="T">The type to deserialize the response to</typeparam>
    /// <param name="endpoint">The API endpoint (without base URL)</param>
    /// <param name="parameters">Optional query parameters</param>
    /// <returns>The deserialized response</returns>
    Task<T> GetAsync<T>(string endpoint, Dictionary<string, string>? parameters = null);
} 