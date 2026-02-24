#!/usr/bin/env bash
set -euo pipefail

ROOT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")/../.." && pwd)"
cd "$ROOT_DIR"

TARGET_UNITY_VERSION="6000.3.9f1"

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

if ! rg -q 'm_EditorVersion: 6000.3.9f1' ProjectSettings/ProjectVersion.txt; then
  echo "[preflight] ProjectVersion mismatch. Expected Unity $TARGET_UNITY_VERSION"
  exit 1
fi

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
  echo "[preflight] Unity not found in PATH (cannot runtime-verify Unity version in this container)."
  echo "[preflight] Project is pinned to $TARGET_UNITY_VERSION via ProjectVersion.txt."
  echo "[preflight] To run locally: /path/to/Unity -batchmode -quit -projectPath \"$ROOT_DIR\" -executeMethod Monsters.Editor.BuildScript.BuildDebugAndroid"
  exit 0
fi

installed_version="$($unity_bin -version | head -n 1 | tr -d '\r')"
if [[ "$installed_version" != *"$TARGET_UNITY_VERSION"* ]]; then
  echo "[preflight] Unity binary version mismatch: found '$installed_version', expected '$TARGET_UNITY_VERSION'"
  exit 1
fi

echo "[preflight] Unity binary: $(command -v "$unity_bin")"
echo "[preflight] Unity version: $installed_version"
echo "[preflight] Unity ready."
