# Step 6 — Parcel 6 Implementation Spec (Save/Load Continuity)

## Objective
Ship reliable local persistence with backup recovery and versioning so users never lose progression during normal crashes/force-close scenarios.

## Scope (In)
- Local save file with encryption/obfuscation.
- Auto-save checkpoints + manual save.
- Continue flow from title.
- Primary + backup save strategy.
- Save schema version and migration stub.

## Scope (Out)
- Cloud save merge logic (future interface only).
- Account-linked profile sync.

---

## Work Packages

### S6-01 Save Service Core
- `SaveServiceLocal` with atomic write path.
- Temp file -> verify -> swap approach.
- Crash-safe lock handling.

### S6-02 Data Coverage
- Persist: player location, coins, party state, inventory, world gates, settings snapshot.
- Validate on write and read.

### S6-03 Backup and Recovery
- Keep rolling backup (N=1 for MVP).
- On primary read failure, auto-fallback to backup.
- User-facing recovery message in plain language.

### S6-04 Versioning + Migration Stub
- Include schema `version`.
- Add migration handler scaffold for future upgrades.
- Unit tests for unsupported version behavior.

### S6-05 Save Triggers
- Auto-save on region transition and key state commits.
- Manual save from menu.
- Debounce rapid-save storms.

---

## Acceptance Tests
1. Force-close during play restores to latest valid checkpoint.
2. Corrupted primary save falls back to backup automatically.
3. Manual save + continue restores exact party/inventory/coins/location.
4. Repeated save/load cycles (100x) show no data drift.
5. Unsupported version path exits safely with clear message.
6. Save operation does not freeze UI >200ms on target device.
7. No duplicate writes when autosave triggers rapidly.
8. Save file integrity check catches truncated payload.

---

## Bug Hunt Checklist
- Partial writes after app backgrounding.
- Backup overwritten with corrupted data.
- Continue button enabled without valid save.
- Settings snapshot not restoring.
- Save race between battle end and scene load.

---

## Deliverables
- Save/load services + schema versioning stubs.
- Recovery UX and continue flow integration.
- `Docs/Tech/STEP6_REPORT.md` with reliability metrics.
