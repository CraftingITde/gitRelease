FROM mcr.microsoft.com/dotnet/core/sdk:3.1.401-alpine AS build-env
WORKDIR /app

# Alle Pakete wiederherstellen
COPY . ./
RUN dotnet restore

# Jetzt Bauen
RUN dotnet publish gitRelease.csproj -c Release -o out -r linux-musl-x64 --self-contained true /p:PublishTrimmed=true

# Und Final
FROM mcr.microsoft.com/dotnet/core/runtime-deps:3.1.7-alpine
WORKDIR /app
COPY --from=build-env /app/out .

ENTRYPOINT ["./gitRelease"]
