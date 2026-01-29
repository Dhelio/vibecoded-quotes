# Helper for Render: 
# Render automatically detects Dockerfile if present at root context (usually).
# But since this project is in a subdirectory, the user might need to specify the Docker Build Context or path.
# However, I will place the Dockerfile inside the project directory as requested.

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AntigravityQuotes.csproj", "."]
RUN dotnet restore "./AntigravityQuotes.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./AntigravityQuotes.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AntigravityQuotes.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AntigravityQuotes.dll"]
