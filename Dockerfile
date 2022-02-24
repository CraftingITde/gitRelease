FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Alle Pakete wiederherstellen
COPY . ./
RUN dotnet restore

# Jetzt Bauen
RUN dotnet publish gitRelease.csproj -c Release -o out -r linux-musl-x64 --self-contained true /p:PublishTrimmed=true

# Und Final
FROM mcr.microsoft.com/dotnet/runtime:6.0.2-alpine3.14-amd64
WORKDIR /app
COPY --from=build-env /app/out .

ENTRYPOINT ["./gitRelease"]
