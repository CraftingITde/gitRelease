#!/bin/bash
pwd
cd /workspace 
echo "test" > test.md
ls -la
whoami
pwd

dotnet /app/gitRelease.dll "$@"