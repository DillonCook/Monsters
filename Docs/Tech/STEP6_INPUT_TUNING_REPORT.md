# Step 6 Report — Input Tuning (DPI-aware swipe + movement cooldown)

## Current status
- We are currently in **Parcel 1 (Exploration + Encounter Trigger)**.
- This is **Step 6** of the implementation progression in this repo.

## What shipped
- Added DPI-aware swipe threshold calculation so gesture sensitivity scales better across devices.
- Replaced fixed pixel swipe distance config with inches + fallback DPI settings.
- Added a lightweight movement cooldown gate to reduce accidental multi-step bursts from rapid touch chains.
- Updated HUD line to expose cooldown value for quick tuning in playtests.

## Why this is the next step
- Improves thumb control consistency across Android device classes.
- Keeps movement loop responsive while preventing noisy over-input.
- Maintains modular design by keeping gesture parsing in `SwipeDirectionInput` and scene orchestration in `TownSceneController`.

## Next suggested step
1. Add sign/NPC interaction placeholders on grid tiles.
2. Add editor gizmos for blocked/biome tiles for faster content iteration.
3. Capture runtime screenshots once Unity runtime is available.
