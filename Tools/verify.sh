#!/usr/bin/env bash
set -euo pipefail

ROOT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
cd "$ROOT_DIR"

echo "[verify] Checking required scenes..."
required_scenes=(
  "Assets/Scenes/TitleScene.unity"
  "Assets/Scenes/TownScene.unity"
  "Assets/Scenes/BattleScene.unity"
)

for scene in "${required_scenes[@]}"; do
  if [[ ! -f "$scene" ]]; then
    echo "[verify] Missing required scene: $scene"
    exit 1
  fi
done

echo "[verify] Checking Build Settings contains all required scenes..."
for scene in "${required_scenes[@]}"; do
  if ! rg -q "$scene" ProjectSettings/EditorBuildSettings.asset; then
    echo "[verify] Scene not in Build Settings: $scene"
    exit 1
  fi
done

echo "[verify] Checking Parcel 1 world systems scaffold..."
required_world_scripts=(
  "Assets/Scripts/World/Monsters.World.asmdef"
  "Assets/Scripts/World/Movement/PlayerGridMover.cs"
  "Assets/Scripts/World/Movement/WorldInteractionPoint.cs"
  "Assets/Scripts/World/Movement/InteractionType.cs"
  "Assets/Scripts/World/Encounter/EncounterTriggerService.cs"
  "Assets/Scripts/World/Runtime/WorldRuntimeState.cs"
  "Assets/Scripts/World/Runtime/TownSceneController.cs"
  "Assets/Scripts/World/Runtime/BattleSceneController.cs"
  "Assets/Scripts/World/Runtime/SwipeDirectionInput.cs"
  "Assets/Editor/BuildScript.cs"
)

for script in "${required_world_scripts[@]}"; do
  if [[ ! -f "$script" ]]; then
    echo "[verify] Missing Parcel 1 script: $script"
    exit 1
  fi
done

echo "[verify] Lint placeholder: C# lint tooling not configured yet (pass)."

echo "[verify] Unity batchmode build template:"
echo "  /path/to/Unity -batchmode -quit -projectPath \"$ROOT_DIR\" -buildTarget Android -executeMethod BuildScript.BuildDebugAndroid"

echo "[verify] Running Unity preflight..."
./Tools/unity/preflight.sh

echo "[verify] Bootstrap + Parcel 1 scaffold checks passed."
