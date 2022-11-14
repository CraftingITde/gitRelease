FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Alle Pakete wiederherstellen
COPY . ./
RUN dotnet restore -r alpine.3.9-x64

# Jetzt Bauen
RUN dotnet publish gitRelease.csproj -c Release -o out --no-self-contained --no-restore -r alpine.3.9-x64 /p:PublishSingleFile=true

# Und Final
FROM mcr.microsoft.com/dotnet/runtime:7.0-alpine
RUN apk add --no-cache \
        git
WORKDIR /app
COPY --from=build-env /app/out .
ENV PATH="${PATH}:/app"
RUN  chmod u+x /app/gitRelease
ENTRYPOINT ["gitRelease"]
