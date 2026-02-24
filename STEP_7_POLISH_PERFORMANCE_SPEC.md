# Step 7 — Parcel 7 Implementation Spec (UX Polish + Performance)

## Objective
Hit MVP quality targets for responsiveness, readability, and battle pacing on mid-tier Android hardware.

## Scope (In)
- Thumb-zone layout tuning.
- Visual clarity pass for status/type effects.
- Performance profiling and optimization pass.
- Core balance tweaks for battle duration target.

## Scope (Out)
- Long-tail content expansion.
- Advanced animation systems beyond MVP need.

---

## Work Packages

### S7-01 UX Readability Pass
- Ensure critical numbers are visible at a glance.
- Increase contrast and spacing for small screens.
- Standardize icon language across world/battle/menu.

### S7-02 Input Comfort Pass
- Touch targets >= 44dp.
- Reduce accidental taps near screen edges.
- Haptic feedback consistency for key actions.

### S7-03 Performance Profiling
- Profile world traversal, encounter transitions, battle command processing.
- Track frame-time spikes and memory churn.
- Optimize draw calls, allocations, and effect budgets.

### S7-04 Battle Pace Tuning
- Adjust damage/status parameters to keep average battle 30–90 sec.
- Verify no starter over-dominance in neutral matchups.
- Tune AI action weights to reduce dead turns.

### S7-05 Stability Soak
- 30-minute uninterrupted session pass.
- Test suspend/resume behavior under memory pressure.

---

## Acceptance Tests
1. Average battle duration remains between 30 and 90 seconds.
2. UI readability passes on 6.0" and 6.7" devices.
3. Interaction latency remains low and consistent after 20 minutes.
4. Frame-time spikes above threshold reduced to agreed limit.
5. No critical issues in 30-minute soak run.
6. Input mis-tap rate reduced compared with Step 5 baseline.
7. Battery/thermal behavior acceptable for 20-minute session.
8. Crash-free rate meets pre-alpha gate criteria.

---

## Bug Hunt Checklist
- UI overlap in localized strings.
- Frame hitch on first transition after cold start.
- Battle VFX causing touch delay.
- Memory growth after repeated scene swaps.
- Status icons desync from actual state.

---

## Deliverables
- Performance tuning commits + UX refinements.
- Balance parameter updates and benchmark summary.
- `Docs/Tech/STEP7_REPORT.md` with profiling artifacts.
