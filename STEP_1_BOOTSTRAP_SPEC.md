# Step 1 — Parcel 0 Implementation Spec (Project Bootstrap)

## Objective
Deliver a **runnable Android shell** with clean module boundaries so later gameplay features plug in without refactors.

## Step 1 Scope (In)
- Unity 2D project baseline with Android target settings.
- Scene shell: Title, Town, Battle (placeholder content allowed).
- App navigation: New Game / Continue / Settings routes.
- Core module folder structure and assembly definitions.
- Build + verification scripts for repeatable local CI checks.

## Out of Scope (Step 1)
- No real combat resolution yet.
- No taming, no encounters, no final art.
- No cloud save/account/multiplayer implementation.

---

## Execution Plan

### 1) Repository Skeleton
Create this top-level layout:

```text
/Docs
  /Design
  /Tech
/ProjectSettings
/Assets
  /Scenes
  /Scripts
    /Core
    /World
    /Battle
    /Taming
    /Data
    /UI
    /Save
    /Platform
  /Prefabs
  /Art
  /Audio
  /UI
/Tools
  verify.sh
```

### 2) Unity Scenes
- `TitleScene.unity`
  - Buttons: New Game, Continue, Settings.
  - Minimal clear visual hierarchy for 3-second comprehension.
- `TownScene.unity`
  - Placeholder tilemap + player spawn marker.
- `BattleScene.unity`
  - Placeholder battle canvas + command ring slots.

### 3) Navigation Contracts
Define scene routes in one place:
- `SceneId.Title`
- `SceneId.Town`
- `SceneId.Battle`

Add one coordinator entry point:
- `GameBootstrapper` handles boot and scene transitions.

### 4) Script Architecture
- `Core`
  - app state enum, scene routing, service registry.
- `UI`
  - title menu presenter + button handlers only.
- `Platform`
  - Android lifecycle hooks (pause/resume stub).
- `Save`
  - local save interface + placeholder implementation.

> Keep all gameplay logic out of UI classes.

### 5) Android Baseline Settings
- Target orientation: portrait-first.
- Min SDK: choose current practical floor for device coverage.
- Scripting backend: IL2CPP.
- Stripping level: conservative for first build.

### 6) Verification Script
`Tools/verify.sh` should run:
1. project sanity checks (required scenes exist)
2. style/lint command placeholder
3. optional headless Unity batchmode build command template

### 7) QA Smoke Checklist
- App launches to title.
- New Game transitions to town.
- Continue loads placeholder path (or disabled with clear label if no save).
- Settings opens and closes correctly.
- Back button behavior on Android is deterministic.

---

## Acceptance Tests (Step 1)
1. Fresh clone can be opened and built without manual scene relinking.
2. All three required scenes exist and are in Build Settings.
3. Title buttons are thumb-reachable on a 6" device.
4. App start-to-title time is under 3 seconds on test hardware target.
5. Debug APK generation succeeds from documented command path.
6. No null-reference errors during first 60-second navigation smoke run.
7. Project folder/module boundaries match architecture plan.
8. Step 1 changelog entry exists.

---

## Deliverables to Commit for Step 1
- Unity project scaffolding and scene stubs.
- Initial scripts for bootstrap + title navigation.
- Build/verification scripts.
- `Docs/Tech/STEP1_REPORT.md` with:
  - what was shipped,
  - known gaps,
  - screenshots,
  - next-step handoff.

---

## Suggested Ticket Breakdown
- **S1-01**: Initialize Unity project + Android build settings.
- **S1-02**: Create scene trio (Title/Town/Battle).
- **S1-03**: Implement scene routing and bootstrap.
- **S1-04**: Implement title UI handlers.
- **S1-05**: Add save interface stub + continue button behavior.
- **S1-06**: Add verify script + docs + smoke checklist.

---

## Risks & Mitigations (Step 1)
- **Risk:** setup churn across machines.
  - **Mitigation:** lock Unity editor version and commit project settings.
- **Risk:** early UI overdesign.
  - **Mitigation:** keep placeholder visuals, prioritize interaction clarity.
- **Risk:** tight coupling between UI and systems.
  - **Mitigation:** enforce coordinator + interface boundaries from day one.

---

## Exit Criteria
Step 1 is done only when a tester can pull repo, build Android debug, launch, navigate Title → Town, and return cleanly with no critical errors.
