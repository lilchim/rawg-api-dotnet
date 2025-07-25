using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RawgApi.Client;

/// <summary>
/// Service collection extensions for RawgApiClient
/// </summary>
public static class ServiceExtensions
{
    /// <summary>
    /// Add RawgApiClient to the service collection
    /// </summary>
    /// <param name="services">The service collection</param>
    /// <param name="configuration">The configuration</param>
    /// <returns>The service collection</returns>
    public static IServiceCollection AddRawgApiClient(this IServiceCollection services, IConfiguration configuration)
    {
        var baseUrl = configuration["RawgApiClient:BaseUrl"] ?? "http://localhost:5254";
        
        services.AddHttpClient<RawgApiClient>(client =>
        {
            client.BaseAddress = new Uri(baseUrl);
            client.Timeout = TimeSpan.FromSeconds(30);
        });

        return services;
    }

    /// <summary>
    /// Add RawgApiClient to the service collection with custom configuration
    /// </summary>
    /// <param name="services">The service collection</param>
    /// <param name="baseUrl">The base URL for the RAWG API service</param>
    /// <returns>The service collection</returns>
    public static IServiceCollection AddRawgApiClient(this IServiceCollection services, string baseUrl)
    {
        services.AddHttpClient<RawgApiClient>(client =>
        {
            client.BaseAddress = new Uri(baseUrl);
            client.Timeout = TimeSpan.FromSeconds(30);
        });

        return services;
    }
} 