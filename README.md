# gitRelease [![Build Status](https://jenkins.craftingit.de/buildStatus/icon?job=kstruessmann%28GitHub%29%2FgitRelease%2Fmaster)](https://jenkins.craftingit.de/job/kstruessmann(GitHub)/job/gitRelease/job/master/)

gitRelease ist ein .net Core Consolen Program, welches verwendet werden kann um in Verschiedenen CI Systemen Releases zu erstellen oder zu bearbeiten. Aktuell wird GitHub und Gitea als Api Entpunkt unterstützt.

## Usage

### Github
 ![](.img/github.png)

### Gitea
 ![](.img/gitea.png)

## CLI Download 

Latest Releases for windows, linux and MacOS [here](https://github.com/kstruessmann/gitRelease/releases) 

## Docker [![](https://badgen.net/badge/docker/Docker?icon&label=View%20on)](https://hub.docker.com/r/craftingit/gitrelease) ![](https://badgen.net/docker/pulls/craftingit/nextcloud-cron?icon=docker&label=pulls) ![](https://badgen.net/docker/stars/craftingit/gitrelease?icon=docker&label=stars) ![](https://badgen.net/docker/size/craftingit/gitrelease?icon=docker)

```sh
  docker pull craftingit/gitrelease:latest
```

## Nuget [![](https://badgen.net/nuget/v/gitrelease)](https://www.nuget.org/packages/gitRelease/)

```sh
  dotnet tool install --global gitRelease 
``` 
