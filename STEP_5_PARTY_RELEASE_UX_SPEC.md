# Step 5 — Parcel 5 Implementation Spec (Party, Inventory, Roster-Cap Release UX)

## Objective
Ship robust party/inventory management with hard cap enforcement (5 max) and a safe, clear release flow when taming overflows.

## Scope (In)
- In-game menu tabs: Party, Inventory, Journal, Map, Settings.
- Party cap enforcement at runtime and save layer.
- Overflow release comparison flow with hold-to-confirm.
- Item use consistency in battle + field.

## Scope (Out)
- Journal deep taxonomy polish (future step).
- Advanced item economy balancing.

---

## Work Packages

### S5-01 Menu Navigation Shell
- Fast, thumb-first tab switching.
- Always-visible key numbers: HP, status, quantity, coins.
- Open/close animation under 180 ms.

### S5-02 Party Data Guardrails
- Central `PartyService` max-size validation.
- Prevent >5 from entering save payload.
- Integrity check on load with fallback rule.

### S5-03 Overflow Release Flow
- Triggered immediately after successful tame at full roster.
- Comparison card for new monster vs existing 5.
- Dual confirmation: select + hold-to-confirm (0.8s).

### S5-04 Inventory + Item Handlers
- Consistent consume rules in and out of battle.
- Disabled states with reason labels for unusable contexts.
- Quantity sync in HUD and menu.

### S5-05 UX Safety + Error Handling
- Prevent accidental release via undo grace toast (3s) if technically feasible.
- Hard block navigation while release unresolved.
- Crash-safe transactional update (release + add in one commit operation).

---

## Acceptance Tests
1. Party count never exceeds 5 in runtime or persisted save.
2. Overflow tame always routes to comparison flow before return.
3. Hold-to-confirm prevents accidental release in rapid tap tests.
4. Item quantities match expected values after mixed battle/field usage.
5. Menu can be operated one-handed on standard 6" device.
6. Load-time integrity check resolves invalid legacy party payload safely.
7. 50 sequential roster operations run without data corruption.
8. No null refs when entering/exiting menu repeatedly for 10 minutes.

---

## Bug Hunt Checklist
- Duplicate monster entries after overflow handling.
- Released monster reappears after save/load.
- Item quantity desync between views.
- Menu stutter on low-memory device.
- Hold-to-confirm prematurely firing.

---

## Deliverables
- Party/inventory/journal/map/settings menu flows.
- Overflow release UI + transactional service update.
- `Docs/Tech/STEP5_REPORT.md` with bug and test evidence.
