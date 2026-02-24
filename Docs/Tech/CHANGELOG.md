# Changelog

## Step 9 - Unity 6000.3.9f1 compile-fix alignment
- Pinned `ProjectSettings/ProjectVersion.txt` to Unity `6000.3.9f1`.
- Reworked UI runtime scripts to remove hard compile-time dependency on TMP/uGUI types (`Component` + safe text reflection).
- Updated `TitleMenuPresenter` to inspector-driven button handlers for safer cross-version compatibility.
- Hardened `Tools/unity/preflight.sh` with Unity version checks and build entrypoint validation.


## Step 8 - Unity Test Readiness (Phase 1 wrap)
- Added `Assets/Editor/BuildScript.cs` with `Monsters.Editor.BuildScript.BuildDebugAndroid`.
- Added `Tools/unity/preflight.sh` for local Unity preflight + command guidance.
- Updated `Tools/verify.sh` to validate build entrypoint and run Unity preflight.
- Added `Docs/QA/UNITY_TEST_CHECKLIST.md` for Unity tester handoff.


## Step 7 - Parcel 1 Interaction Placeholders + Gizmos
- Added interaction tile primitives (`InteractionType`, `WorldInteractionPoint`).
- Extended world map contracts to query interaction type by tile.
- Added `Interact()` flow and sign/NPC tile placeholders in `TownSceneController`.
- Added editor gizmos for blocked/biome/sign/NPC tile authoring.
- Fixed movement cooldown application after successful movement.


## Step 6 - Parcel 1 Input Tuning
- Added DPI-aware swipe threshold calculation in `SwipeDirectionInput`.
- Updated `TownSceneController` to use inches-based swipe threshold config.
- Added movement cooldown gating to avoid accidental repeated steps.
- HUD now shows cooldown value for tuning visibility.


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
