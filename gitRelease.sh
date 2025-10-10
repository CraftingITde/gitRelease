#!/bin/bash
pwd
cd /workspace 
ls -la
pwd

dotnet /app/gitRelease.dll "$@"