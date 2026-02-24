# Unity Test Checklist (Step 8 Readiness)

## Goal
Enable a developer/tester to open the project in Unity and validate Parcel 1 quickly on Android-oriented flows.

## Environment
- Unity version: `2022.3.25f1` (from `ProjectSettings/ProjectVersion.txt`).
- Open project root and allow script compilation.

## Scene wiring checklist
1. Open `TitleScene` and ensure a root object has `GameBootstrapper`.
2. In `TitleScene`, wire `TitleMenuPresenter` buttons and TMP status label.
3. Open `TownScene` and wire `TownSceneController` references:
   - `playerVisual`
   - `worldCamera`
   - `hudLabel` (TMP)
4. Assign sample tile coordinates:
   - `blockedTiles`: e.g. `(3,3)`, `(3,4)`
   - `biomeTiles`: e.g. `(6,6)`, `(6,7)`
   - `signTiles`: e.g. `(2,1)`
   - `npcTiles`: e.g. `(4,2)`
5. Open `BattleScene` and wire `BattleSceneController` TMP label + Return button.

## Runtime checks
1. Launch app: confirm title in <3 seconds.
2. New Game routes to Town.
3. Swipe and button movement both work.
4. Blocked tiles stop movement.
5. Biome tiles can trigger encounter transition.
6. `Interact()` on sign/NPC tiles updates HUD with placeholder text.
7. Return from Battle routes back to Town and control resumes.

## Build checks
1. Run `./Tools/verify.sh`.
2. Run `./Tools/unity/preflight.sh`.
3. Build APK in batchmode:
   - `/path/to/Unity -batchmode -quit -projectPath "<repo>" -executeMethod Monsters.Editor.BuildScript.BuildDebugAndroid`
