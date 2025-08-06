# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
COPY ["src/RawgApi/RawgApi.csproj", "src/RawgApi/"]
COPY ["src/RawgApi.Models/RawgApi.Models.csproj", "src/RawgApi.Models/"]
COPY ["src/RawgApi.Client/RawgApi.Client.csproj", "src/RawgApi.Client/"]
RUN dotnet restore "src/RawgApi/RawgApi.csproj"

# Copy the rest of the source code
COPY . .

# Build the application
WORKDIR "/src/src/RawgApi"
RUN dotnet build "RawgApi.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "RawgApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copy published application
COPY --from=publish /app/publish .

# Expose port 80
EXPOSE 80

# Set the entry point
ENTRYPOINT ["dotnet", "RawgApi.dll"] 