FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /app

# Alle Pakete wiederherstellen
COPY . ./

# Jetzt Bauen
RUN dotnet publish gitRelease.csproj -c Release -o out
# Und Final
FROM mcr.microsoft.com/dotnet/runtime:9.0

RUN apt-get -y update &&  \ 
    apt-get install --no-install-recommends  \
    -y git && \
    rm -rf /var/lib/apt/lists/*

COPY --from=build-env /app/out /app

COPY gitRelease.sh /usr/bin/gitRelease 
RUN chmod +x /usr/bin/gitRelease 
RUN mkdir /workspace

WORKDIR /workspace

ENTRYPOINT ["gitRelease"]
