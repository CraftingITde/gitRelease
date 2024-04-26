FROM mcr.microsoft.com/dotnet/sdk:8.0-jammy AS build-env
WORKDIR /app

# Alle Pakete wiederherstellen
COPY . ./
RUN dotnet restore

# Jetzt Bauen
RUN dotnet publish gitRelease.csproj -c Release -o out --no-self-contained --no-restore
# Und Final
FROM mcr.microsoft.com/dotnet/runtime:8.0-jammy
RUN apk add --no-cache \
        git
WORKDIR /app
COPY --from=build-env /app/out .
ENV PATH="${PATH}:/app"
RUN  chmod u+x /app/gitRelease
ENTRYPOINT ["gitRelease"]
