# Changelog

## Step 5 - Parcel 1 Mobile Input + TMP HUD
- Added `SwipeDirectionInput` service for thumb-first swipe movement detection.
- Updated `TownSceneController` to support swipe-driven movement while retaining button method endpoints.
- Migrated Town/Battle HUD labels from legacy `Text` to `TMP_Text`.
- Added `Unity.TextMeshPro` references to world/ui asmdefs.

## Step 4 - Parcel 1 Persistent Bootstrap Milestone
- Made `GameBootstrapper` a persistent singleton root (`DontDestroyOnLoad`) for cross-scene continuity.
- Refactored `SceneRouter` into a plain routing service consumed by bootstrapper.
- Updated title/town/battle runtime scripts to use `GameBootstrapper.Instance` first and reduce manual wiring fragility.

## Step 3 - Parcel 1 Integration Foundation (Town/Battle Controllers)
- Added `TownSceneController` to wire movement + encounter services into scene runtime flow.
- Added directional movement entry points for thumb-friendly mobile button mapping.
- Added `BattleSceneController` with return-to-town hook.
- Converted `WorldRuntimeState` into shared singleton state for scene transition continuity.

## Step 2 - Parcel 1 Kickoff (Exploration + Encounter Foundations)
- Added `Monsters.World` assembly boundary and modular world domain scripts.
- Added grid movement primitives and player movement service (`PlayerGridMover`).
- Added deterministic-ready encounter trigger service with seeded RNG abstraction.
- Added world runtime continuity helper for post-battle control/camera restoration.
- Expanded `Tools/verify.sh` to include Parcel 1 scaffold checks.

## Step 1 - Parcel 0 Bootstrap
- Added Android-first project scaffold, scene placeholders, and initial modular scripts.
- Added bootstrap/navigation contracts for Title, Town, and Battle flows.
- Added verification script and Step 1 implementation report.
