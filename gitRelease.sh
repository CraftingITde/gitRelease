#!/bin/bash
pwd
cd /workspace 
echo "test" > test.md
ls -la
ls -la core
whoami
pwd

dotnet /app/gitRelease.dll "$@"