FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /app

# Alle Pakete wiederherstellen
COPY . ./

# Jetzt Bauen
RUN dotnet publish gitRelease.csproj -c Release -o out --self-contained
# Und Final
FROM ubuntu:24.04
WORKDIR /app

RUN apt-get update && apt-get install -y --no-install-recommends \
        git

COPY --from=build-env /app/out .
ENV PATH="${PATH}:/app"
RUN  chmod u+x /app/gitRelease
ENTRYPOINT ["gitRelease"]
