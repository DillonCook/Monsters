# Step 4 Report — Persistent Bootstrap Root (Parcel 1)

## What shipped
- Converted `GameBootstrapper` into a persistent singleton root (`DontDestroyOnLoad`).
- Converted `SceneRouter` into a plain service (non-MonoBehaviour) and injected it into bootstrapper.
- Updated `TitleMenuPresenter` to consume `GameBootstrapper.Instance` so title flow works without manual script wiring.
- Updated `TownSceneController` and `BattleSceneController` to route through shared bootstrapper instance first, with safe direct-scene fallback.

## Why this milestone matters
- Reduces scene-level fragility and manual drag-and-drop dependencies.
- Keeps navigation deterministic and centralized.
- Moves architecture closer to production Parcel 1 flow where world/battle transitions are reliable on device.

## Remaining work
1. Author real Unity scenes/prefabs and wire mobile controls visually.
2. Add TMP-based HUD in Town/Battle and remove legacy `Text` components.
3. Add playmode tests for transition loops (Title -> Town -> Battle -> Town).
