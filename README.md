# gitRelease
gitRelease ist ein .net Consolen Program, welches verwendet werden kann um in Verschiedenen CI Systemen Releases zu erstellen oder zu bearbeiten. Aktuell wird GitHub und Gitea als Api Entpunkt unterstützt.

## GitHub Action

gitRelease kann auch als GitHub Action verwendet werden, um Releases direkt in deinen Workflows zu verwalten.

### Verwendung

```yaml
- name: Create Release
  uses: CraftingITde/gitRelease@master
  with:
    command: 'github create'
    token: ${{ secrets.GITHUB_TOKEN }}
    tag: 'v1.0.0'
    name: 'Release v1.0.0'
    body: 'Release description'
    prerelease: false
    draft: false
```

### Verfügbare Inputs

- `command`: Der auszuführende Befehl (z.B. `github create`, `github update`, `github upload`, `version next`)
- `token`: GitHub Token für die Authentifizierung (meist `secrets.GITHUB_TOKEN`)
- `owner`: Repository Owner (optional, Standard: aktueller Owner)
- `repo`: Repository Name (optional, Standard: aktuelles Repository)
- `tag`: Tag-Name für das Release
- `name`: Release-Name
- `body`: Release-Beschreibung
- `prerelease`: Als Prerelease markieren (true/false)
- `draft`: Als Draft markieren (true/false)
- `file`: Datei-Pfad für Asset-Upload
- `label`: Label für das Asset

## Usage

### Github
```` sh
bash-3.2$ gitrelease github
gitRelease 0.1.0
Copyright (C) 2020 gitRelease

  create          Creates a new Release

  update          Updates a present Release

  upload          Upload an asset

  updateBody      Updates a present Release

  isDraft         is the release a draft?

  isPrerelease    is the release a Prerelease?

  help            Display more information on a specific command.

  version         Display version information.
````

### Gitea
```` sh
bash-3.2$ gitrelease gitea
gitRelease 0.1.0
Copyright (C) 2020 gitRelease

  update          updating a Tag

  upload          Upload an asset

  isDraft         is the release a draft?

  isPrerelease    is the release a Prerelease?

  help            Display more information on a specific command.

  version         Display version information.
````

### Version
```` sh
bash-3.2$ gitrelease version
gitRelease 0.1.0
Copyright (C) 2020 gitRelease

  next            Gets the next available semantic version number.

  generate        Generates a new tag and version branch if necessary.

  help            Display more information on a specific command.

  version         Display version information.

  generate-notes  Generate release notes content for a release
````

## CLI Download 

Latest Releases for windows, linux and MacOS [here](https://github.com/kstruessmann/gitRelease/releases) 

## Docker [![](https://badgen.net/badge/docker/Docker?icon&label=View%20on)](https://hub.docker.com/r/craftingit/gitrelease) ![](https://badgen.net/docker/pulls/craftingit/gitrelease?icon=docker&label=pulls) ![](https://badgen.net/docker/stars/craftingit/gitrelease?icon=docker&label=stars) ![](https://badgen.net/docker/size/craftingit/gitrelease?icon=docker)

```sh
  docker pull craftingit/gitrelease:latest
```

## Nuget [![](https://badgen.net/nuget/v/gitrelease)](https://www.nuget.org/packages/gitRelease/)

```sh
  dotnet tool install --global gitRelease 
``` 
