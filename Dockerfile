FROM mcr.microsoft.com/dotnet/core/sdk:3.0-alpine AS build-env
WORKDIR /app

# Alle Pakete wiederherstellen
COPY . ./
RUN dotnet restore

# Jetzt Bauen
RUN dotnet publish gitRelease.csproj -c Release -o out

# Und Final
FROM microsoft/dotnet:3.0-runtime-deps-stretch-slim
WORKDIR /app
COPY --from=build-env /app/out .

ENTRYPOINT ["dotnet", "gitrelease.dll"]