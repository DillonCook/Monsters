# Alpha Launch Playbook (Get the Game Off the Ground)

## 1) Alpha Definition (What “Alpha” means for this project)
Alpha is reached when a small external test cohort can run the full core loop with acceptable stability and clear UX:

- Explore -> encounter -> battle -> tame -> party/release -> save/load works end-to-end.
- No blocker defects open.
- Crash rate and performance meet minimum thresholds.
- Core metrics instrumented and visible.

### Alpha Exit Criteria (hard gate)
1. End-to-end loop passes 20/20 scripted runs.
2. Crash-free sessions >= 98% in internal test cohort.
3. Average battle duration between 30 and 90 seconds.
4. Save corruption recovery path proven in QA.
5. Party cap/release correctness verified in 100 stress operations.
6. Onboarding comprehension: tester reaches first battle in <= 3 minutes.

---

## 2) Practical Build Sequence (Fastest path)

### Phase A — Foundations (Week 1)
- Lock Unity version + Android settings.
- Build scene shell and state routing.
- Implement Step 2 world loop baseline (movement/collision/encounters).

### Phase B — Core Progression (Week 2)
- Implement deterministic battle core.
- Implement taming flow and roster overflow release UX.
- Hook items + statuses + type interactions.

### Phase C — Reliability (Week 3)
- Implement save/load with backup recovery.
- Add QA automation scripts and repeatable smoke tests.
- Resolve top-priority defects from soak testing.

### Phase D — Alpha Readiness (Week 4)
- UX polish + performance tuning.
- Balance pass for battle pacing and starter parity.
- Package internal alpha candidate and run go/no-go review.

---

## 3) Team Setup (Lean but sufficient)

### Minimum team
- 1 gameplay engineer (world + battle + taming)
- 1 client engineer (UI + save + performance)
- 1 technical designer (data tuning + encounter tables + balancing)
- 1 QA generalist (mobile regression + bug triage)
- 1 part-time artist/UI support (icon pass + readability)

### Daily cadence (keeps speed high)
- 15-min standup (blockers only)
- 1 daily playable build
- 1 bug triage pass at end of day
- Strict “one in-flight parcel” policy

---

## 4) Must-Have Technical Backbone Before External Alpha

### Deterministic combat and replayability
- Battle resolver must run independent of UI.
- Store battle seed and command stream for bug replay.

### Save resilience
- Atomic writes and one rolling backup.
- Fast corruption detection and safe fallback.

### Telemetry baseline
Track these from day one:
- Session start/end
- Time-to-first-battle
- Battle duration
- Tame attempt/success rates
- Defeat and respawn frequency
- Save/load success/failure counts

---

## 5) QA Plan That Actually Catches Real Mobile Bugs

### Test matrix
- Devices: low / mid / high Android profile.
- Modes: cold start, warm start, long session, low battery, background/resume.

### Daily smoke suite (10–15 min)
1. New game boot and title navigation.
2. World movement + collision + interaction.
3. Encounter trigger and battle entry.
4. One tame attempt + failure and success path.
5. Overflow release flow at party size 5.
6. Save + force close + continue.

### Twice-weekly soak suite (30 min)
- Repeat world/battle loop continuously.
- Track memory growth, frame-time spikes, and input degradation.

---

## 6) Alpha KPI Dashboard (simple and actionable)

### Product KPIs
- D1 return rate (internal cohort): target >= 35%
- Median session length: target 12–20 min
- Battles per session: target >= 5
- Tame conversion (eligible attempts): target 35–60%

### Quality KPIs
- Crash-free sessions: >= 98%
- ANR rate: <= 1%
- Critical bug reopen rate: < 10%

### UX KPIs
- Time-to-understanding (self-reported): <= 3 sec
- Mis-tap rate on battle actions: trending downward week-over-week

---

## 7) Release Process (Internal -> Closed Alpha)

### Internal Alpha Candidate (IAC)
- Build signed and versioned.
- Release notes include known issues and test focus areas.
- QA matrix pass attached.

### Closed Alpha Candidate (CAC)
- Top 10 defects from IAC fixed or mitigated.
- Performance thresholds verified on mid-tier device.
- Onboarding path and first battle reliability confirmed.

### Distribution
- Internal: direct APK + device lab.
- Closed alpha: Play internal testing track once stable.

---

## 8) Risk Register + Mitigation

1. **Risk: Too much planning, not enough playable builds**  
   Mitigation: daily build requirement + parcel-based signoff.

2. **Risk: Determinism breaks under UI refactors**  
   Mitigation: seed-based battle replay tests in CI.

3. **Risk: Mobile UX confusion in first 3 minutes**  
   Mitigation: guided first encounter and reduced initial choice overload.

4. **Risk: Save corruption during scene transitions**  
   Mitigation: debounce autosaves and transactional write pipeline.

5. **Risk: Performance regression late in cycle**  
   Mitigation: performance budget checks starting week 2, not week 4.

---

## 9) 14-Day “Start Now” Checklist

### Days 1–3
- Lock tooling versions.
- Build scene/state skeleton.
- Implement movement/collision baseline.

### Days 4–6
- Add encounters and battle entry/return.
- Integrate deterministic turn resolution skeleton.

### Days 7–9
- Implement taming + odds preview.
- Implement party cap + release flow.

### Days 10–11
- Save/load + backup recovery.
- Add telemetry events.

### Days 12–13
- Run full regression + bug bash.
- Fix blockers and major defects.

### Day 14
- Package internal alpha candidate.
- Go/no-go review and next sprint planning.

---

## 10) CTO Recommendation (Most important)
Do not wait for perfect content. Ship a **small, stable, instrumented core loop** first, then iterate with data. Alpha success is reliability + clarity, not content volume.
