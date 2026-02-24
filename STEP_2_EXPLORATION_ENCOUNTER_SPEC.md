# Step 2 — Parcel 1 Implementation Spec (Exploration + Encounter Trigger)

## Objective
Ship the first playable overworld loop: **move, collide, interact, trigger wild encounter transition, return safely**.

## Screenshot Delivery Commitment
- **Earliest screenshot delivery:** immediately after these are green on device/emulator:
  1. Player can move on map.
  2. Collision blocks are respected.
  3. Biome tile can trigger encounter transition to Battle scene.
- If build setup is smooth, target is **same working day as Step 2 scene scaffold completion**.

---

## Scope (In)
- Top-down movement on grid-like tilemap.
- Collision masks for blocked vs walkable tiles.
- Interactable placeholders (signpost/NPC prompt).
- Biome-tag encounter tiles with configurable trigger chance.
- Encounter transition to Battle scene and safe return to overworld.

## Scope (Out)
- No final battle mechanics.
- No taming logic yet.
- No content balancing pass yet.

---

## Implementation Work Packages

### S2-01 Movement Controller (Mobile-first)
- Add `PlayerController2D` with:
  - thumb-friendly virtual stick input abstraction.
  - optional tap-to-move interface hook (stub if needed).
  - fixed timestep movement to avoid jitter.
- Keep movement speed data-driven (`WorldConfig`).

### S2-02 Tilemap + Collision Layering
- Define layers:
  - `Walkable`
  - `Blocked`
  - `BiomeEncounter`
  - `Interactable`
- Add `CollisionMapAuthoringGuide` (short doc section inside this file used by level designer).

### S2-03 Interactions
- Add `InteractableNode` interface:
  - `CanInteract(playerState)`
  - `OnInteract()`
- Implement two stubs:
  - `SignNode` (displays short text)
  - `NpcNode` (one-line utility prompt)

### S2-04 Encounter Trigger Service
- Add `EncounterDirector` with per-step probability checks.
- Input params:
  - region id
  - tile tag
  - base chance
  - anti-spam cooldown steps
- Output:
  - encounter event with selected table id.

### S2-05 Scene Transition Reliability
- Build `SceneTransitionService` (fade-in/fade-out stub acceptable).
- Ensure world state is preserved when returning from placeholder battle.

### S2-06 Bug Catch Pass (Required)
Run this bug sweep before sign-off:
1. Corner clipping against blocked tiles.
2. Movement drift when input is released.
3. Double-trigger encounter on single step.
4. Softlock after returning from battle scene.
5. Interaction prompt stale when walking away.
6. Frame hitch during scene transition.
7. Android back button causing unwanted app close in world scene.

---

## Data Contracts (Step 2 Minimal)

### `WorldConfig`
- `playerMoveSpeed`
- `inputDeadzone`
- `interactionRadius`
- `encounterCooldownSteps`

### `RegionEncounterConfig`
- `regionId`
- `tileTag`
- `baseEncounterChance`
- `tableId`

### `ReturnPointState`
- `sceneId`
- `position`
- `facing`

---

## Acceptance Tests (Step 2)
1. Player movement starts/stops without drift on touch input.
2. Blocked tiles cannot be traversed from any approach angle.
3. Interactable node can be focused and triggered only in range.
4. Biome tiles can trigger encounter based on configured chance.
5. Encounter cooldown prevents immediate retrigger loops.
6. Encounter transition reaches Battle scene in <1.2s on test hardware.
7. Return from battle restores previous world position/state.
8. No null refs during 10-minute exploration soak run.
9. Android back behavior follows expected scene-stack logic.
10. At least one exploration screenshot captured and attached to Step 2 report.

---

## QA Runbook (Fast)
- **Smoke 1 (2 min):** movement + collision only.
- **Smoke 2 (3 min):** interactions + prompts.
- **Smoke 3 (5 min):** encounter spam test with boosted chance.
- **Smoke 4 (5 min):** transition stability + return point integrity.

If any smoke fails, block merge and file fix ticket before proceeding.

---

## Deliverables to Commit for Step 2
- Movement/collision/interaction scripts.
- Encounter director and transition hooks.
- Region encounter configs (minimum one biome tile setup).
- `Docs/Tech/STEP2_REPORT.md` including:
  - bug list found/fixed,
  - remaining known issues,
  - performance notes,
  - screenshot(s).

---

## CTO Notes (Speed + Clarity)
- Build only one polished path first: Town edge -> biome patch -> battle transition -> return.
- Avoid speculative abstractions; use interfaces only where Step 3 depends on them.
- Keep logs concise and tagged (`[World]`, `[Encounter]`, `[Transition]`) for fast QA triage.
