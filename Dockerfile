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

RUN echo -e '#!/bin/bash\ndotnet /app/gitRelease.dll "$@"' > /usr/bin/gitRelease && \
    chmod +x /usr/bin/gitRelease 

ENTRYPOINT ["dotnet", "/app/gitRelease.dll"]
