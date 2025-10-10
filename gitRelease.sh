#!/bin/bash
cd /workspace 
ls -la

dotnet /app/gitRelease.dll "$@"