# RawgApi.Client

This package provides a client library for integrating with the RAWG API.

## Features

- HTTP client for RAWG API
- Service extensions for dependency injection
- Automatic API key authentication
- Built-in logging support

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
    private readonly IRawgApiService _rawgApiService;

    public GameService(IRawgApiService rawgApiService)
    {
        _rawgApiService = rawgApiService;
    }

    public async Task<IEnumerable<Game>> GetGamesAsync()
    {
        return await _rawgApiService.GetGamesAsync();
    }
}
```

## Configuration

The client can be configured with the following options:

- `ApiKey`: Your RAWG API key
- `BaseUrl`: The base URL for the RAWG API (defaults to https://api.rawg.io/api)

## License

This project is licensed under the MIT License. 