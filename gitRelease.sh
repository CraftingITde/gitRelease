#!/bin/bash
pwd
cd /workspace 
echo "test" > test.md
ls -la
pwd

dotnet /app/gitRelease.dll "$@"