# Exercise 4 — Burndown Charts

## Part A — read the real Sprint 3 chart

Open `../charts/sprint-burndown-chart.png` (generated from
`../charts/burndown-data.csv`).

1. On which day does the actual line first fall *below* the ideal line?
   What does that mean happened on that day?
2. On which days is the actual line completely flat? Cross-reference the
   `note` column in `burndown-data.csv` for those days — what caused it?
3. Does the sprint finish exactly on Day 10, early, or late? Given the
   flat stretch you identified in question 2, does that surprise you?
   Why were they able to still finish on time despite the stall?

## Part B — build your own burndown data

Using your Sprint 4 backlog selection from Exercise 3, create a new
`sprint4-burndown-data.csv` (same column format as
`../charts/burndown-data.csv`) that tells a *plausible* story of how
that sprint might actually go — not just a straight line matching the
ideal. Include at least one of:
- A day where two small stories both finish, causing a bigger-than-usual
  drop.
- A stall (a blocker, unplanned leave, a production incident pulling
  someone away).
- A scope change (a story got split or re-estimated mid-sprint, so the
  total points at the start don't match what you'd expect from summing
  the original story estimates).

Then generate the chart:
```bash
cd charts
python3 generate_charts.py
```
(You'll need to either edit `generate_charts.py` to point at your new
CSV file and a new output filename, or temporarily rename your file to
`burndown-data.csv` before running it — either is fine for this
exercise.)

## Part C — what a burndown chart can't tell you

A burndown chart shows *how much work is left*, but says nothing about
*whether the remaining work is the right work*, or *whether what's
already "done" actually meets the Definition of Done*
(`../templates/definition-of-done.md`).

Write 2–3 sentences: describe a scenario where a burndown chart looks
perfect (a smooth, on-time line straight to zero) but the sprint was
actually a failure in some other way. What would you need to look at,
beyond the burndown chart itself, to catch that?

## Check yourself

- Could you explain the difference between a burndown chart (this
  exercise) and a velocity chart (`../charts/velocity-chart.png`) to
  someone who's never seen either? (Hint: one tracks progress *within* a
  single sprint; the other tracks a trend *across* sprints.)
- If your team consistently sees the same kind of stall (e.g. always
  around the same day, always the same root cause), what would you bring
  up in the Sprint Retrospective (see
  `../product-backlog/sprint-planning-example.md` §6)?
