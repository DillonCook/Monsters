# Step 1 Report — Parcel 0 Bootstrap

## What shipped
- Unity-style repository scaffold for Android-first 2D project.
- Scene stubs: `TitleScene`, `TownScene`, `BattleScene`.
- Core routing contract and boot coordinator:
  - `SceneId`, `ISceneRouter`, `SceneRouter`, `GameBootstrapper`.
- Title UI presenter with New Game / Continue / Settings handlers.
- Save interface and local save placeholder implementation.
- Android lifecycle bridge stub for pause/focus events.
- Verification script at `Tools/verify.sh` to validate scene and build settings integrity.

## Known gaps
- Scene files are placeholders and still need final Unity-authored objects/layout.
- Continue flow intentionally returns false until save persistence is implemented.
- Unity build command is a template and requires a real `BuildScript` implementation.
- No gameplay systems included (encounters/battle/taming are out of scope for Step 1).

## QA smoke checklist status
- Launch to title: Pending (requires Unity runtime validation).
- New Game routes to town: Implemented in bootstrap script.
- Continue behavior: Implemented as clear "No save found" placeholder.
- Settings panel open/close: Implemented in presenter.
- Android back behavior: Pending runtime hookup.

## Next-step handoff
1. Open project in Unity 2022.3.25f1 and wire `GameBootstrapper` + `TitleMenuPresenter` in `TitleScene`.
2. Replace placeholder scene YAML with Unity-authored scenes and mobile-first UI layout.
3. Add `BuildScript.BuildDebugAndroid` and connect CI to call `Tools/verify.sh`.
4. Begin Parcel 1 movement and encounter trigger systems.
