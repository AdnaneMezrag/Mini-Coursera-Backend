FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files
COPY Backend/Backend.sln ./
COPY Backend/API.csproj Backend/
COPY Application/Application.csproj Application/
COPY Domain/Domain.csproj Domain/
COPY Infrastructure/Infrastructure.csproj Infrastructure/

# Restore dependencies
RUN dotnet restore "Backend/Backend.sln"

# Copy the full source code
COPY . .

# Build and publish
WORKDIR /src/Backend
RUN dotnet build --no-restore -c Release
RUN dotnet publish -c Release -o /app/publish

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Backend.dll"]
