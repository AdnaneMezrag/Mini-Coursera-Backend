# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution file
COPY Backend/Backend.sln .

# Copy only the .csproj files to match paths expected in .sln
COPY Backend/API/API.csproj ./API.csproj
COPY Application/Application.csproj ./Application.csproj
COPY Domain/Domain.csproj ./Domain.csproj
COPY Infrastructure/Infrastructure.csproj ./Infrastructure.csproj

# Restore dependencies
RUN dotnet restore "Backend.sln"

# Copy full project folders
COPY Backend/API ./API
COPY Application ./Application
COPY Domain ./Domain
COPY Infrastructure ./Infrastructure

# Build and publish
WORKDIR /src/API
RUN dotnet build --no-restore -c Release
RUN dotnet publish -c Release -o /app/publish

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "API.dll"]
