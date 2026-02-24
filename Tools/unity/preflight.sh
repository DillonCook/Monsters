#!/usr/bin/env bash
set -euo pipefail

ROOT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")/../.." && pwd)"
cd "$ROOT_DIR"

echo "[preflight] Repo root: $ROOT_DIR"

required_files=(
  "ProjectSettings/ProjectVersion.txt"
  "ProjectSettings/EditorBuildSettings.asset"
  "Assets/Editor/BuildScript.cs"
  "Assets/Scenes/TitleScene.unity"
  "Assets/Scenes/TownScene.unity"
  "Assets/Scenes/BattleScene.unity"
)

for file in "${required_files[@]}"; do
  if [[ ! -f "$file" ]]; then
    echo "[preflight] Missing required file: $file"
    exit 1
  fi
done

if ! rg -q 'BuildDebugAndroid' Assets/Editor/BuildScript.cs; then
  echo "[preflight] BuildScript missing BuildDebugAndroid entrypoint."
  exit 1
fi

unity_bin=""
if command -v Unity >/dev/null 2>&1; then
  unity_bin="Unity"
elif command -v unity >/dev/null 2>&1; then
  unity_bin="unity"
fi

if [[ -z "$unity_bin" ]]; then
  echo "[preflight] Unity not found in PATH (this is expected in CI container)."
  echo "[preflight] To run locally: /path/to/Unity -batchmode -quit -projectPath \"$ROOT_DIR\" -executeMethod Monsters.Editor.BuildScript.BuildDebugAndroid"
  exit 0
fi

echo "[preflight] Unity binary: $(command -v "$unity_bin")"
"$unity_bin" -version || true

echo "[preflight] Unity ready."
