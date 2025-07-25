using Microsoft.OpenApi.Models;
using RawgApi.Configuration;
using RawgApi.Middleware;
using RawgApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure RAWG API options
builder.Services.Configure<RawgApiOptions>(
    builder.Configuration.GetSection(RawgApiOptions.SectionName));

// Configure API Key options
builder.Services.Configure<ApiKeyOptions>(
    builder.Configuration.GetSection(ApiKeyOptions.SectionName));

// Configure CORS
builder.Services.AddCors(options =>
{
    var corsConfig = builder.Configuration.GetSection("Cors");
    if (corsConfig.GetValue<bool>("Enabled"))
    {
        options.AddDefaultPolicy(policy =>
        {
            policy.WithOrigins(corsConfig.GetSection("AllowedOrigins").Get<string[]>() ?? Array.Empty<string>())
                  .WithMethods(corsConfig.GetSection("AllowedMethods").Get<string[]>() ?? new[] { "GET" })
                  .WithHeaders(corsConfig.GetSection("AllowedHeaders").Get<string[]>() ?? new[] { "Content-Type" })
                  .SetIsOriginAllowedToAllowWildcardSubdomains();

            if (corsConfig.GetValue<bool>("AllowCredentials"))
            {
                policy.AllowCredentials();
            }
        });
    }
});

// Configure HTTP client for RAWG API
builder.Services.AddHttpClient<IRawgApiService, RawgApiService>(client =>
{
    var options = builder.Configuration.GetSection(RawgApiOptions.SectionName).Get<RawgApiOptions>();
    client.Timeout = TimeSpan.FromSeconds(options?.TimeoutSeconds ?? 30);
    client.DefaultRequestHeaders.Add("User-Agent", "RawgApi-DotNet/1.0");
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "RAWG API Proxy",
        Version = "v1",
        Description = "A .NET proxy for the RAWG API that provides a clean, secure interface for accessing gaming data.",
        Contact = new OpenApiContact
        {
            Name = "RAWG API Proxy",
            Url = new Uri("https://github.com/your-username/rawg-api-dotnet")
        }
    });

    // Add API Key authentication
    c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.ApiKey,
        In = ParameterLocation.Header,
        Name = "X-API-Key",
        Description = "API Key for authentication"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "ApiKey"
                }
            },
            Array.Empty<string>()
        }
    });

    // Include XML comments
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "RAWG API Proxy v1");
        c.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

// Use CORS
app.UseCors();

// Use API Key authentication middleware
app.UseMiddleware<ApiKeyAuthenticationMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
