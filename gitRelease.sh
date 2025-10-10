#!/bin/bash
cd /workspace 
ls -la
PWD

dotnet /app/gitRelease.dll "$@"