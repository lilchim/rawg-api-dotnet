using Microsoft.Extensions.Options;
using RawgApi.Configuration;
using System.Text.Json;

namespace RawgApi.Services;

/// <summary>
/// Service for interacting with the RAWG API
/// </summary>
public class RawgApiService : IRawgApiService
{
    private readonly HttpClient _httpClient;
    private readonly IOptions<RawgApiOptions> _options;
    private readonly ILogger<RawgApiService> _logger;

    public RawgApiService(
        HttpClient httpClient,
        IOptions<RawgApiOptions> options,
        ILogger<RawgApiService> logger)
    {
        _httpClient = httpClient;
        _options = options;
        _logger = logger;
    }

    public async Task<JsonElement> GetAsync(string endpoint, Dictionary<string, string>? parameters = null)
    {
        var url = BuildUrl(endpoint, parameters);
        _logger.LogDebug("Making request to RAWG API: {Url}", url);

        var retryCount = 0;
        while (retryCount <= _options.Value.MaxRetries)
        {
            try
            {
                var response = await _httpClient.GetAsync(url);
                
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<JsonElement>(content);
                }

                _logger.LogWarning("RAWG API returned error status {StatusCode} for endpoint {Endpoint}", 
                    response.StatusCode, endpoint);

                if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                {
                    retryCount++;
                    if (retryCount <= _options.Value.MaxRetries)
                    {
                        var delay = TimeSpan.FromSeconds(Math.Pow(2, retryCount)); // Exponential backoff
                        _logger.LogInformation("Rate limited, retrying in {Delay} seconds (attempt {RetryCount}/{MaxRetries})", 
                            delay.TotalSeconds, retryCount, _options.Value.MaxRetries);
                        await Task.Delay(delay);
                        continue;
                    }
                }

                throw new HttpRequestException($"RAWG API returned status code {response.StatusCode}");
            }
            catch (HttpRequestException ex) when (retryCount < _options.Value.MaxRetries)
            {
                retryCount++;
                var delay = TimeSpan.FromSeconds(Math.Pow(2, retryCount)); // Exponential backoff
                _logger.LogWarning(ex, "Request failed, retrying in {Delay} seconds (attempt {RetryCount}/{MaxRetries})", 
                    delay.TotalSeconds, retryCount, _options.Value.MaxRetries);
                await Task.Delay(delay);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error making request to RAWG API endpoint {Endpoint}", endpoint);
                throw;
            }
        }

        throw new HttpRequestException($"Failed to make request to RAWG API after {_options.Value.MaxRetries} retries");
    }

    public async Task<T> GetAsync<T>(string endpoint, Dictionary<string, string>? parameters = null)
    {
        var jsonElement = await GetAsync(endpoint, parameters);
        return JsonSerializer.Deserialize<T>(jsonElement.GetRawText()) 
            ?? throw new InvalidOperationException($"Failed to deserialize response to type {typeof(T).Name}");
    }

    private string BuildUrl(string endpoint, Dictionary<string, string>? parameters)
    {
        var uriBuilder = new UriBuilder($"{_options.Value.BaseUrl.TrimEnd('/')}/{endpoint.TrimStart('/')}");
        
        var query = new List<string>();
        
        // Add API key
        if (!string.IsNullOrEmpty(_options.Value.ApiKey))
        {
            query.Add($"key={Uri.EscapeDataString(_options.Value.ApiKey)}");
        }

        // Add additional parameters
        if (parameters != null)
        {
            foreach (var param in parameters)
            {
                if (!string.IsNullOrEmpty(param.Value))
                {
                    query.Add($"{Uri.EscapeDataString(param.Key)}={Uri.EscapeDataString(param.Value)}");
                }
            }
        }

        if (query.Count > 0)
        {
            uriBuilder.Query = string.Join("&", query);
        }

        return uriBuilder.ToString();
    }
} 