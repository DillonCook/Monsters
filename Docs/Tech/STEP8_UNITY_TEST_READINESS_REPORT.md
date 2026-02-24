# Step 8 Report — Unity Test Readiness (Phase 1 Wrap)

## Current status
- Phase 1 implementation progression has reached **Step 8**.
- Parcel 1 systems are prepared for Unity-side integration and smoke testing.

## What shipped
- Added a Unity editor batch build entrypoint: `Monsters.Editor.BuildScript.BuildDebugAndroid`.
- Added `Tools/unity/preflight.sh` to validate required files and provide a deterministic local Unity command path.
- Expanded `Tools/verify.sh` to include build script presence and Unity preflight execution.
- Added a practical Unity tester handoff checklist in `Docs/QA/UNITY_TEST_CHECKLIST.md`.

## Why this is production-useful
- Standardizes test/build bootstrapping for devs and QA.
- Reduces setup ambiguity before real on-device playtesting.
- Keeps Parcel 1 handoff focused on validation instead of environment guesswork.

## Next step after Unity validation
1. Capture Title/Town/Battle screenshots.
2. Log acceptance results against Parcel 1 checks.
3. If signed off, start Parcel 2 deterministic battle core.


## Unity 6 migration patch
- Updated project pin to Unity `6000.3.9f1`.
- Added package manifest with `com.unity.ugui` + `com.unity.textmeshpro` to resolve UI/TMP compile dependencies.
- Updated preflight to assert expected Unity version and package dependencies.
