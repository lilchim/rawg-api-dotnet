# Use the official .NET 8 runtime image as the base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Use the official .NET 8 SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project files
COPY ["src/RawgApi/RawgApi.csproj", "src/RawgApi/"]
COPY ["src/RawgApi.Models/RawgApi.Models.csproj", "src/RawgApi.Models/"]
COPY ["src/RawgApi.Client/RawgApi.Client.csproj", "src/RawgApi.Client/"]

# Restore dependencies
RUN dotnet restore "src/RawgApi/RawgApi.csproj"

# Copy the rest of the source code
COPY . .

# Build the application
WORKDIR "/src/src/RawgApi"
RUN dotnet build "RawgApi.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "RawgApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Build the final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Set environment variables
ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Production

# Create a non-root user
RUN adduser --disabled-password --gecos '' appuser && chown -R appuser /app
USER appuser

ENTRYPOINT ["dotnet", "RawgApi.dll"] 