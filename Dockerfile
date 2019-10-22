FROM mcr.microsoft.com/dotnet/core/sdk:3.0-alpine AS build-env
WORKDIR /app

# Alle Pakete wiederherstellen
COPY . ./
RUN dotnet restore

# Jetzt Bauen
RUN dotnet publish gitRelease.csproj -c Release -o out --self-contained true /p:PublishTrimmed=true

# Und Final
FROM alpine:3.10.3
WORKDIR /app
COPY --from=build-env /app/out .

ENTRYPOINT ["./gitrelease"]