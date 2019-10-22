FROM mcr.microsoft.com/dotnet/core/sdk:3.0-alpine AS build-env
WORKDIR /app

# Alle Pakete wiederherstellen
COPY . ./
RUN dotnet restore

# Jetzt Bauen
RUN dotnet publish gitRelease.csproj -c Release -o out -r linux-x64 --self-contained true /p:PublishTrimmed=true
RUN chmod +x gitRelease

# Und Final
FROM mcr.microsoft.com/dotnet/core/runtime:3.0-alpine
WORKDIR /app
COPY --from=build-env /app/out .

ENTRYPOINT ["./gitRelease"]
