# RAWG API Setup Guide

This guide will help you set up and run the RAWG API proxy.

## Prerequisites

- .NET 8 SDK
- Docker (optional, for containerized deployment)
- RAWG API key (get one at https://rawg.io/apidocs)

## Quick Start

### 1. Clone the Repository

```bash
git clone https://github.com/your-username/rawg-api-dotnet.git
cd rawg-api-dotnet
```

### 2. Configure API Keys

#### For Development (User Secrets)

```bash
# Set RAWG API key
dotnet user-secrets set "RawgApi:ApiKey" "your-rawg-api-key"

# Set API keys for authentication (optional)
dotnet user-secrets set "ApiKey:ValidApiKeys:0" "your-dev-key"
dotnet user-secrets set "ApiKey:RequireApiKey" "true"
```

#### For Production (Environment Variables)

```bash
# Copy example environment file
cp env.example .env

# Edit .env with your actual values
RAWG_API_KEY=your-actual-rawg-api-key
REQUIRE_API_KEY=true
API_KEY_1=your-production-api-key
```

### 3. Run the Application

#### Local Development

```bash
# Restore dependencies
dotnet restore

# Run the application
dotnet run --project src/RawgApi
```

The API will be available at:
- http://localhost:5000 (HTTP)
- https://localhost:5001 (HTTPS)
- Swagger UI: https://localhost:5001/swagger

#### Docker Deployment

```bash
# Build and run with Docker Compose
docker-compose up --build

# Or run individual containers
docker build -t rawg-api .
docker run -p 5000:80 -e RAWG_API_KEY=your-key rawg-api
```

## API Endpoints

### Games
- `GET /api/games` - List games with filtering
- `GET /api/games/{id}` - Get specific game
- `GET /api/games/{id}/screenshots` - Get game screenshots
- `GET /api/games/{id}/additions` - Get game DLCs
- `GET /api/games/{id}/stores` - Get game stores

### Platforms
- `GET /api/platforms` - List platforms
- `GET /api/platforms/{id}` - Get specific platform
- `GET /api/platforms/{id}/games` - Get platform games

### Genres
- `GET /api/genres` - List genres
- `GET /api/genres/{id}` - Get specific genre
- `GET /api/genres/{id}/games` - Get genre games

### Status
- `GET /api/status` - API status
- `GET /api/status/health` - Health check

## Configuration

### appsettings.json

```json
{
  "RawgApi": {
    "ApiKey": "",
    "BaseUrl": "https://api.rawg.io/api",
    "TimeoutSeconds": 30,
    "MaxRetries": 3
  },
  "ApiKey": {
    "RequireApiKey": false,
    "ValidApiKeys": [],
    "HeaderName": "X-API-Key",
    "QueryParameterName": "api_key"
  },
  "Cors": {
    "Enabled": true,
    "AllowedOrigins": ["http://localhost:3000"],
    "AllowedMethods": ["GET", "POST", "OPTIONS"],
    "AllowedHeaders": ["Content-Type", "X-API-Key"]
  }
}
```

### Environment Variables

| Variable | Description | Default |
|----------|-------------|---------|
| `RAWG_API_KEY` | RAWG API key | Required |
| `REQUIRE_API_KEY` | Enable API key auth | false |
| `API_KEY_1` | First valid API key | - |
| `CORS_ENABLED` | Enable CORS | true |

## Client Library Usage

```csharp
// In Program.cs or Startup.cs
using RawgApi.Client;

// Register the client with configuration
services.AddRawgApiClient(configuration);

// Or register with custom base URL
services.AddRawgApiClient("https://your-rawg-api-service.com");

// Use the client
var client = serviceProvider.GetRequiredService<RawgApiClient>();

// Get games
var games = await client.GetGamesAsync(search: "action");

// Get specific game
var game = await client.GetGameAsync(3498);

// Get platforms
var platforms = await client.GetPlatformsAsync();

// Get genres
var genres = await client.GetGenresAsync();
```

### Client Configuration

Add to your `appsettings.json`:

```json
{
  "RawgApiClient": {
    "BaseUrl": "https://your-rawg-api-service.com"
  }
}
```

## Popular Game IDs

| Game | ID |
|------|----|
| Grand Theft Auto V | 3498 |
| The Witcher 3 | 3328 |
| Red Dead Redemption 2 | 28 |
| Cyberpunk 2077 | 41494 |
| Elden Ring | 540 |

## Troubleshooting

### Common Issues

1. **401 Unauthorized**: Check your RAWG API key
2. **500 Internal Server Error**: Check RAWG API status
3. **CORS errors**: Configure allowed origins
4. **Rate limiting**: Implement exponential backoff

### Logs

Check application logs for detailed error information:

```bash
# Docker logs
docker-compose logs rawg-api

# Local logs
dotnet run --project src/RawgApi --verbosity detailed
```

## Security

- Never commit API keys to source control
- Use environment variables or user secrets
- Enable API key authentication for production
- Configure CORS properly
- Use HTTPS in production

## Performance

- The API includes retry logic with exponential backoff
- Responses are cached by default
- Consider implementing additional caching for production
- Monitor rate limits from RAWG API 