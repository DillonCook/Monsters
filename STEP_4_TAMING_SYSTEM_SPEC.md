# Step 4 — Parcel 4 Implementation Spec (Taming System: Resonance Weave)

## Objective
Ship a short, skill-based taming flow that is mobile-friendly, deterministic in resolution, and clearly communicates success odds.

## Scope (In)
- Harmonize command gating logic.
- Resonance Weave mini-sequence (3 timing taps).
- Odds resolver (HP/status/item/skill score/level delta).
- Failure consequences and cooldown rules.
- UX copy + haptics + feedback cues for accessibility.

## Scope (Out)
- No roster-full release flow changes here (Step 5).
- No final VFX polish pass (Step 7).

---

## Work Packages

### S4-01 Taming Entry Rules
- Enable `Harmonize` only when wild HP <= 45%.
- Lockout 2 turns after a taming attempt.
- Show disabled reason text when unavailable.

### S4-02 Resonance Weave Interaction
- 3 pulse crossings over 2.5 to 4.0 seconds.
- Tap result buckets: Perfect / Good / Miss.
- Input buffering window for low-end touch latency.

### S4-03 Deterministic Taming Resolver
- Formula:
  `FinalChance = clamp(Base + HPBonus + StatusBonus + ItemBonus + SkillBonus - LevelPenalty, 5, 95)`
- Source all constants from config asset.
- Emit debug trace string for QA replay validation.

### S4-04 Failure and Cooldown Outcomes
- Failure grants wild `Resolve` (+10% DEF for 1 turn).
- Second failure in same battle grants temporary +10% damage for 1 turn.
- No item refund.

### S4-05 UX/Feedback
- Pre-attempt odds preview range.
- Distinct color + haptic signals for Perfect/Good/Miss.
- Result modal with exact reason summary (e.g., "Low HP bonus + status bonus applied").

---

## Acceptance Tests
1. Harmonize cannot be activated when HP threshold is unmet.
2. Cooldown prevents repeat attempts for 2 turns.
3. Perfect/Good/Miss scoring changes success chance exactly per config.
4. Same seed + same input timing yields same resolver outcome.
5. Failure buffs apply and expire at expected turns.
6. Odds preview updates immediately when status/HP/item changes.
7. Touch interaction remains reliable in 30 FPS stress mode.
8. 20-attempt QA run contains zero softlocks.

---

## Bug Hunt Checklist
- Missed taps on device edges.
- Timing desync when app resumes from background.
- Odds mismatch between preview and final roll.
- Cooldown reset on scene transitions.
- Double-consume of taming item.

---

## Deliverables
- Taming systems + config assets.
- Battle command integration for Harmonize.
- `Docs/Tech/STEP4_REPORT.md` with bug/fix table and test evidence.
