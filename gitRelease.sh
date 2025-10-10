#!/bin/bash
if [ -n "$WORKSPACE_DIR" ]; then
  cd "$WORKSPACE_DIR"
fi

ls -la

dotnet /app/gitRelease.dll "$@"