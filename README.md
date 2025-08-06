# RAWG API .NET

A .NET 8 implementation of the RAWG API proxy that provides a clean, secure interface for accessing gaming data from the RAWG database. This service acts as a proxy to the official RAWG API, handling authentication and providing a consistent REST interface.

## Features

* 🔐 **Secure API Key Management** - Server-side RAWG API key storage
* 📚 **Comprehensive Documentation** - Full Swagger/OpenAPI documentation
* 🐳 **Docker Support** - Easy deployment with Docker and Docker Compose
* 🔄 **RESTful Interface** - Clean REST endpoints for all RAWG API methods
* 📊 **Status Monitoring** - Built-in health checks and configuration status
* 🛡️ **Error Handling** - Robust error handling and logging
* 📦 **NuGet Packages** - Reusable models and client libraries
* 🎮 **Gaming Data** - Access to comprehensive gaming database

## Quick Start

### Option 1: Using Docker Compose (Recommended)

```bash
# Clone the repository
git clone https://github.com/your-username/rawg-api-dotnet.git
cd rawg-api-dotnet

# Create environment file
cp .env.example .env
# Edit .env with your RAWG API key and other settings

# Start the service
docker-compose up --build
```

### Option 2: Local Development

```bash
# Clone the repository
git clone https://github.com/your-username/rawg-api-dotnet.git
cd rawg-api-dotnet

# Restore dependencies
dotnet restore

# Set up user secrets (for development)
dotnet user-secrets set "RawgApi:ApiKey" "your-rawg-api-key"
dotnet user-secrets set "ApiKey:ValidApiKeys:0" "your-dev-key"

# Run the application
dotnet run --project src/RawgApi
```

## Client Library

The project includes a **RawgApi.Client** NuGet package for easy integration into your applications.

### Installation

```bash
dotnet add package RawgApi.Client
```

### Usage

```csharp
// In Program.cs or Startup.cs
using RawgApi.Client;

// Register the client
services.AddRawgApiClient(configuration);

// Use the client
var client = serviceProvider.GetRequiredService<RawgApiClient>();

// Get games
var games = await client.GetGamesAsync(search: "action");

// Get specific game
var game = await client.GetGameAsync(3498);

// Get platforms and genres
var platforms = await client.GetPlatformsAsync();
var genres = await client.GetGenresAsync();
```

### Configuration

Add to your `appsettings.json`:

```json
{
  "RawgApiClient": {
    "BaseUrl": "https://your-rawg-api-service.com"
  }
}
```

## API Endpoints

### Games

#### `GET /api/games`

Get a list of games with optional filtering.

**Parameters:**
- `search` (query): Search query
- `page` (query): Page number (default: 1)
- `page_size` (query): Page size, max 40 (default: 20)
- `ordering` (query): Ordering (e.g., name, -released, rating)
- `metacritic` (query): Metacritic score range (e.g., 80,90)
- `genres` (query): Comma-separated genre IDs
- `platforms` (query): Comma-separated platform IDs
- `stores` (query): Comma-separated store IDs
- `developers` (query): Comma-separated developer IDs
- `publishers` (query): Comma-separated publisher IDs

**Example:**
```bash
# Search for action games
GET /api/games?search=action&genres=4&ordering=-rating

# Get popular games
GET /api/games?ordering=-metacritic&page_size=10
```

#### `GET /api/games/{id}`

Get a specific game by ID.

**Example:**
```bash
# Get game details
GET /api/games/3498
```

#### `GET /api/games/{id}/screenshots`

Get screenshots for a specific game.

**Example:**
```bash
# Get game screenshots
GET /api/games/3498/screenshots
```

#### `GET /api/games/{id}/additions`

Get DLCs for a specific game.

**Example:**
```bash
# Get game DLCs
GET /api/games/3498/additions
```

#### `GET /api/games/{id}/stores`

Get stores where a game can be purchased.

**Example:**
```bash
# Get game stores
GET /api/games/3498/stores
```

### Status

#### `GET /api/status`

Get the overall API status.

**Example:**
```bash
# Get API status
GET /api/status
```

#### `GET /api/status/health`

Get health check status.

**Example:**
```bash
# Get health status
GET /api/status/health
```

## Configuration

### Environment Variables

| Variable | Description | Default |
|----------|-------------|---------|
| `RAWG_API_KEY` | Your RAWG API key | Required |
| `REQUIRE_API_KEY` | Enable API key authentication | false |
| `API_KEY_1` | First valid API key | - |
| `API_KEY_2` | Second valid API key | - |
| `CORS_ENABLED` | Enable CORS | true |
| `CORS_ORIGIN_1` | First allowed CORS origin | http://localhost:3000 |
| `CORS_ORIGIN_2` | Second allowed CORS origin | https://localhost:3000 |

### User Secrets (Development)

```bash
# Set RAWG API key
dotnet user-secrets set "RawgApi:ApiKey" "your-rawg-api-key"

# Set API keys for authentication
dotnet user-secrets set "ApiKey:ValidApiKeys:0" "your-dev-key"
dotnet user-secrets set "ApiKey:ValidApiKeys:1" "another-key"

# Enable API key authentication
dotnet user-secrets set "ApiKey:RequireApiKey" "true"
```

## Client Library

The project includes a client library for easy integration:

```csharp
// Register the client
services.AddHttpClient<RawgApiClient>(client =>
{
    client.BaseAddress = new Uri("https://your-api-url.com");
});

// Use the client
var client = serviceProvider.GetRequiredService<RawgApiClient>();

// Get games
var games = await client.GetGamesAsync(search: "action", page: 1, pageSize: 20);

// Get specific game
var game = await client.GetGameAsync(3498);

// Get game screenshots
var screenshots = await client.GetGameScreenshotsAsync(3498);
```

## Popular Game IDs

| Game | ID |
|------|----|
| Grand Theft Auto V | 3498 |
| The Witcher 3: Wild Hunt | 3328 |
| Red Dead Redemption 2 | 28 |
| Cyberpunk 2077 | 41494 |
| Elden Ring | 540 |
| God of War | 58175 |
| The Last of Us Part II | 58132 |
| Spider-Man | 58134 |

## Error Handling

The API returns standard HTTP status codes:

- `200` - Success
- `400` - Bad Request (invalid parameters)
- `401` - Unauthorized (invalid API key)
- `500` - Internal Server Error (RAWG API issues)

Error responses include descriptive messages to help with debugging.

## Development

### Project Structure

```
rawg-api-dotnet/
├── src/
│   ├── RawgApi/              # Main Web API project
│   ├── RawgApi.Models/       # Shared models (NuGet package)
│   └── RawgApi.Client/       # HTTP client library (NuGet package)
├── docker/                    # Docker configuration
├── docs/                      # Documentation
└── README.md                  # This file
```

### Technology Stack

* **.NET 8** - Latest LTS version
* **ASP.NET Core** - Web API framework
* **Docker** - Containerization
* **Swagger/OpenAPI** - API documentation
* **RAWG API** - Gaming database

### Adding New Endpoints

1. Create a new controller in `src/RawgApi/Controllers/`
2. Use the `IRawgApiService` to make RAWG API calls
3. Add proper validation and error handling
4. Include XML documentation comments for Swagger
5. Update this README with examples

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests if applicable
5. Update documentation
6. Submit a pull request

## Links

* **RAWG API Documentation**: https://api.rawg.io/docs/
* **GitHub Repository**: https://github.com/lilchim/rawg-api-dotnet
* **NuGet Packages**: 
  * RawgApi.Models
  * RawgApi.Client



## API Key Authentication

This project uses API Key authentication middleware to secure API endpoints.

### How It Works

* All API requests (except `/api/status`, `/api/status/health`, and `/swagger`) require a valid API key when enabled.
* The API key must be provided in the `X-API-Key` header or as an `api_key` query parameter.
* Requests without a valid API key will receive a `401 Unauthorized` response.

### Configuration

* **API keys are NOT stored in source code or committed to Git.**
* API keys are configured via User Secrets (for development) or environment variables (for production).
* To require API key authentication, set `"RequireApiKey": true` in configuration.

### Using the API Key

* Add your API key to the `X-API-Key` header in requests:
  ```
  X-API-Key: your-api-key
  ```
* Or as a query parameter:
  ```
  GET /api/games?api_key=your-api-key
  ```

## CORS Configuration

Cross-Origin Resource Sharing (CORS) is configurable to control which domains can access your API.

### Configuration Options

* **Enabled**: Enable/disable CORS globally
* **AllowedOrigins**: List of domains that can access the API
* **AllowedMethods**: HTTP methods allowed (GET, POST, etc.)
* **AllowedHeaders**: Headers that can be sent with requests
* **AllowCredentials**: Whether to allow cookies/authorization headers

### Example Configuration

```json
{
  "Cors": {
    "Enabled": true,
    "AllowedOrigins": [
      "http://localhost:3000",
      "https://yourdomain.com"
    ],
    "AllowedMethods": ["GET", "POST", "OPTIONS"],
    "AllowedHeaders": ["Content-Type", "X-API-Key"],
    "AllowCredentials": false
  }
}
```

## License

This project is open source and available under the MIT License.
