using Microsoft.Extensions.Options;
using RawgApi.Configuration;
using System.Text.Json;

namespace RawgApi.Middleware;

/// <summary>
/// Middleware for API key authentication
/// </summary>
public class ApiKeyAuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IOptions<ApiKeyOptions> _options;
    private readonly ILogger<ApiKeyAuthenticationMiddleware> _logger;

    public ApiKeyAuthenticationMiddleware(
        RequestDelegate next,
        IOptions<ApiKeyOptions> options,
        ILogger<ApiKeyAuthenticationMiddleware> logger)
    {
        _next = next;
        _options = options;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Skip authentication for certain endpoints
        if (ShouldSkipAuthentication(context.Request.Path))
        {
            await _next(context);
            return;
        }

        // Check if API key authentication is required
        if (!_options.Value.RequireApiKey)
        {
            await _next(context);
            return;
        }

        // Extract API key from header or query parameter
        var apiKey = ExtractApiKey(context.Request);

        if (string.IsNullOrEmpty(apiKey))
        {
            _logger.LogWarning("API key missing from request to {Path}", context.Request.Path);
            await WriteUnauthorizedResponse(context, "API key is required");
            return;
        }

        // Validate API key
        if (!_options.Value.ValidApiKeys.Contains(apiKey))
        {
            _logger.LogWarning("Invalid API key provided for request to {Path}", context.Request.Path);
            await WriteUnauthorizedResponse(context, "Invalid API key");
            return;
        }

        // Add API key to context for potential rate limiting
        context.Items["ApiKey"] = apiKey;

        await _next(context);
    }

    private static bool ShouldSkipAuthentication(PathString path)
    {
        var skipPaths = new[]
        {
            "/api/status",
            "/api/status/health",
            "/swagger",
            "/favicon.ico"
        };

        return skipPaths.Any(skipPath => path.StartsWithSegments(skipPath));
    }

    private string? ExtractApiKey(HttpRequest request)
    {
        // Try to get API key from header
        if (request.Headers.TryGetValue(_options.Value.HeaderName, out var headerValue))
        {
            return headerValue.FirstOrDefault();
        }

        // Try to get API key from query parameter
        if (request.Query.TryGetValue(_options.Value.QueryParameterName, out var queryValue))
        {
            return queryValue.FirstOrDefault();
        }

        return null;
    }

    private static async Task WriteUnauthorizedResponse(HttpContext context, string message)
    {
        context.Response.StatusCode = 401;
        context.Response.ContentType = "application/json";

        var response = new
        {
            error = "Unauthorized",
            message = message
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
} 