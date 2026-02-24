# Step 8 — Pre-Alpha Release Gate Spec (Ship Readiness)

## Objective
Consolidate Steps 4–7 into a single release gate so the MVP is demo-ready, testable, and stable enough for external feedback.

## Scope (In)
- Cross-system regression pass.
- Final acceptance checklist sign-off.
- Build packaging and release notes.
- Known-issues log with severity tags.

## Scope (Out)
- Public store release metadata and marketing assets.
- Multiplayer functionality.

---

## Work Packages

### S8-01 Full Regression Matrix
- Exploration -> encounter -> battle -> tame -> overflow release -> save -> continue.
- Defeat and respawn validation.
- Item/status/type interactions sanity pass.

### S8-02 Device Compatibility Sweep
- Smoke on low/mid/high Android profiles.
- Verify layout + performance + touch behavior.

### S8-03 Build and Packaging
- Generate signed internal test build.
- Include versioning/changelog.
- Archive profiler and QA evidence.

### S8-04 Defect Triage and Freeze
- Blockers must be zero.
- Major issues need mitigation plan + owner.
- Minor issues deferred with explicit backlog tags.

### S8-05 Go/No-Go Review
- Review acceptance metrics from Steps 4–7.
- Decide ship to controlled testers or hold.

---

## Release Gate Acceptance Criteria
1. No blocker-severity open defects.
2. End-to-end core loop passes 20 consecutive runs.
3. Save recovery path validated at least once per test cycle.
4. Average battle duration remains in target range.
5. Party cap + overflow release correctness confirmed.
6. Deterministic battle resolver replay checks pass.
7. App crash rate within defined threshold for soak tests.
8. Known issues document published with priorities.

---

## Deliverables
- `Docs/Tech/STEP8_RELEASE_REPORT.md`
- Final QA matrix results.
- Candidate build artifact metadata.
- Signed go/no-go decision record.
