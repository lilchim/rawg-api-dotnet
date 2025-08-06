# RawgApi.Client

This package provides a client library for integrating with the RAWG API.

## Features

- HTTP client for RAWG API
- Service extensions for dependency injection
- Automatic API key authentication
- Built-in logging support
- Comprehensive query parameter support for games endpoint

## Installation

```bash
dotnet add package RawgApi.Client
```

## Usage

### Register the service

```csharp
using RawgApi.Client;

// In your Program.cs or Startup.cs
builder.Services.AddRawgApiClient(options =>
{
    options.ApiKey = "your-api-key";
    options.BaseUrl = "https://api.rawg.io/api";
});
```

### Use the client

```csharp
public class GameService
{
    private readonly RawgApiClient _rawgApiClient;

    public GameService(RawgApiClient rawgApiClient)
    {
        _rawgApiClient = rawgApiClient;
    }

    // Simple usage
    public async Task<PaginatedResponse<Game>> GetGamesAsync()
    {
        return await _rawgApiClient.GetGamesAsync();
    }

    // Advanced usage with comprehensive filtering
    public async Task<PaginatedResponse<Game>> GetActionGamesAsync()
    {
        var parameters = new GamesQueryParameters
        {
            Search = "action",
            Genres = "4", // Action genre ID
            Ordering = "-rating",
            PageSize = 20,
            Metacritic = "80,100" // Games with Metacritic score 80-100
        };
        
        return await _rawgApiClient.GetGamesAsync(parameters);
    }

    // Get games for specific platforms
    public async Task<PaginatedResponse<Game>> GetPlayStationGamesAsync()
    {
        var parameters = new GamesQueryParameters
        {
            Platforms = "187,18", // PlayStation 4, PlayStation 5
            Ordering = "-released",
            PageSize = 40
        };
        
        return await _rawgApiClient.GetGamesAsync(parameters);
    }
}
```

## GamesQueryParameters

The `GamesQueryParameters` class provides comprehensive filtering options for the games endpoint:

### Search Parameters
- `Search`: Search query string
- `SearchPrecise`: Enable precise search matching
- `SearchExact`: Enable exact search matching

### Filtering Parameters
- `ParentPlatforms`: Comma-separated parent platform IDs
- `Platforms`: Comma-separated platform IDs
- `Stores`: Comma-separated store IDs
- `Developers`: Comma-separated developer IDs
- `Publishers`: Comma-separated publisher IDs
- `Genres`: Comma-separated genre IDs
- `Tags`: Comma-separated tag IDs
- `Creators`: Comma-separated creator IDs
- `Dates`: Release date ranges (comma-separated)
- `Updated`: Updated date ranges (comma-separated)
- `PlatformsCount`: Number of platforms filter
- `Metacritic`: Metacritic score range (e.g., "80,90")

### Exclusion Parameters
- `ExcludeCollection`: Exclude collection games
- `ExcludeAdditions`: Exclude DLC/additions
- `ExcludeParents`: Exclude parent games
- `ExcludeGameSeries`: Exclude game series
- `ExcludeStores`: Comma-separated store IDs to exclude

### Sorting and Pagination
- `Ordering`: Sort order (e.g., "name", "-released", "rating")
- `Page`: Page number (default: 1)
- `PageSize`: Page size, max 40 (default: 20)

## Configuration

The client can be configured with the following options:

- `ApiKey`: Your RAWG API key
- `BaseUrl`: The base URL for the RAWG API (defaults to https://api.rawg.io/api)

## License

This project is licensed under the MIT License. 