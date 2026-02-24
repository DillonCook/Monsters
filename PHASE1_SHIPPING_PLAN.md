# Phase 1 Shipping Plan (Parcel-by-Parcel)

## Goal
Ship the Android-first single-player MVP in **small, releasable increments**. Each parcel is scoped to be buildable, testable, and demoable on device.

## Product Principles
- Mobile-first clarity in under 3 seconds.
- Keep every loop fast (battle target: 30–90 sec).
- Data-driven content from day one.
- Deterministic battle logic isolated from UI.

---

## Current Execution Status
- **Now executing:** Step 1 / Parcel 0 (Project Bootstrap).
- Detailed implementation spec: `STEP_1_BOOTSTRAP_SPEC.md`.

---

## Parcel 0 — Project Bootstrap (2–3 days)
**Outcome:** runnable app shell with core folders and CI sanity checks.

### Deliverables
- Unity 2D project baseline (Android target configured).
- Folder/module skeleton:
  - `Core/`, `World/`, `Battle/`, `Taming/`, `Data/`, `UI/`, `Save/`, `Platform/`
- Initial scenes:
  - `TitleScene`, `TownScene`, `BattleScene`
- Build profiles for debug Android APK.
- Basic lint/build verification script.

### Acceptance checks
1. App launches to Title screen on Android device/emulator.
2. New Game and Continue buttons render and route placeholders.
3. Project builds from clean checkout without manual editor tweaks.

---

## Parcel 1 — Exploration + Encounter Trigger (3–4 days)
**Outcome:** player can move in top-down map and trigger wild battles.

### Deliverables
- Grid/tile movement with collision.
- Interaction system for signs/NPC placeholders.
- Region tile tags and encounter-enabled biome tiles.
- Encounter trigger service with configurable probability.

### Acceptance checks
1. Movement respects blocked tiles and map bounds.
2. Walking over biome tiles can trigger encounter transition.
3. Non-biome tiles never trigger encounters.
4. Return from battle restores world control and camera state.

---

## Parcel 2 — Deterministic Battle Core (4–6 days)
**Outcome:** complete 1v1 wild battle with turn resolution and win/loss.

### Deliverables
- `BattleResolver` (no UI dependencies).
- Turn order (speed + jitter), hit/crit, damage formula.
- 4-command battle actions: Move / Item / Harmonize / Swap (stubs okay where needed).
- Wild AI v1 weighted decision logic.
- Defeat flow: respawn at nearest outpost, no coin loss.

### Acceptance checks
1. Same seed + same inputs = identical battle result.
2. Damage, accuracy, crit values match configured formulas.
3. Wild AI always returns a valid legal action.
4. Loss causes respawn at nearest safe location.

---

## Parcel 3 — Data Pipeline + MVP Content v1 (4–5 days)
**Outcome:** monsters/moves/items/encounters loaded from data assets.

### Deliverables
- ScriptableObject authoring schemas + validation pass.
- Optional JSON export for diffing and future server sync.
- Implement content minimums:
  - 8 monsters total (4 starters + 4 wild)
  - 16 moves
  - 5 items
  - 1 town + 2 regions + 1 small challenge zone
- Type chart (8 types) and status catalog (4 statuses).

### Acceptance checks
1. Invalid IDs fail validation with actionable error output.
2. Encounter table weights produce expected spawn distribution.
3. All configured monsters load and battle without null references.

---

## Parcel 4 — Taming System (Resonance Weave) (3–4 days)
**Outcome:** skill-based taming loop fully playable in battle.

### Deliverables
- Harmonize eligibility gate (HP threshold + cooldown).
- Timed tap mini-sequence (3 pulse windows).
- Success formula integrating HP/status/item/skill score.
- Failure outcomes (Resolve buff, battle continues).

### Acceptance checks
1. Tame option disabled unless eligibility conditions are met.
2. Perfect/Good/Miss scoring affects success odds as designed.
3. Tame success adds monster to roster when under cap.
4. Tame fail applies failure effects and returns control.

---

## Parcel 5 — Party/Inventory/Release-at-Cap UX (3–4 days)
**Outcome:** full roster management with hard cap of 5 and safe release flow.

### Deliverables
- In-game menu pages: Party, Inventory, Journal, Map, Settings.
- Party storage cap enforcement (max 5).
- Roster-full flow:
  - comparison card view
  - choose release target
  - hold-to-confirm release
- Item use in battle and field.

### Acceptance checks
1. Party never exceeds 5 entities in save/runtime.
2. Overflow tame always triggers release flow before continue.
3. Release confirmation prevents accidental dismissal.
4. Inventory quantities persist correctly through save/load.

---

## Parcel 6 — Save/Load + Continuity (2–3 days)
**Outcome:** reliable local persistence for full gameplay loop.

### Deliverables
- Local encrypted save with backup snapshot.
- Auto-save checkpoints + manual save.
- Continue flow from title.
- Save schema version field + migration stub.

### Acceptance checks
1. Force-close/relaunch restores latest valid save.
2. Backup recovery works if primary save is corrupted.
3. Coins, party, inventory, location all persist correctly.

---

## Parcel 7 — UX Polish + Performance Pass (4–5 days)
**Outcome:** MVP quality target reached for readability and responsiveness.

### Deliverables
- Thumb-zone layout tuning and larger touch targets.
- Battle readability polish (status/type feedback clarity).
- Frame-time profiling on mid-tier Android device.
- Basic balancing pass for battle duration and starter parity.

### Acceptance checks
1. Average battle time lands in 30–90 second target range.
2. UI remains legible on 6.0" and 6.7" devices.
3. No major stutter spikes during encounter transitions or battles.
4. No critical blockers in 30-minute soak session.

---

## Execution Cadence
- **One parcel in progress at a time.**
- Demo + sign-off after each parcel before starting the next.
- Keep branch discipline:
  - `feature/pX-<short-name>` per parcel
  - squash merge after acceptance

## Suggested Immediate Next Step
Start with **Parcel 0** now and ship a runnable Android shell this week. After sign-off, immediately move to Parcel 1 (movement + encounters) so the core loop is visible early.

## Definition of Done (per parcel)
- Code merged and reviewed.
- Acceptance checks pass.
- Android build artifact generated.
- Brief changelog entry added.
