# Module 13 – Agile Methodology

Covers every learning objective in **Module 13** of the DN 5.0 Deep
Skilling handbook: the Agile Manifesto, Scrum, estimation/planning
techniques, and writing effective user stories.

**Start with `AGILE_GUIDE.md`** — that's the main content, and it links
out to every other file in this module at the point where each becomes
relevant.

This module is deliberately built as one continuous worked example
rather than disconnected snippets: a real product backlog, for the same
"Library Catalog" domain used in Modules 5-8, run through an actual
Planning Poker estimation session, a full sprint planning walkthrough
using real velocity and capacity numbers, and - genuinely generated, not
mocked up - burndown and velocity charts showing exactly how that sprint
played out, including a real mid-sprint blocker.

## Contents

```
AGILE_GUIDE.md                          main guide, all 4 handbook topics
product-backlog/
  product-backlog.md                    4 epics, 10 full user stories with acceptance criteria
  sprint-planning-example.md            velocity, Planning Poker, capacity, sprint selection
templates/
  user-story-template.md                As a/I want/So that + full INVEST checklist
  acceptance-criteria-template.md       Given-When-Then + edge-case coverage
  definition-of-done.md                 the team's DoD, and why it matters for velocity
charts/
  burndown-data.csv                     the data behind the burndown chart
  velocity-data.csv                     the data behind the velocity chart
  generate_charts.py                    regenerates both PNGs from the CSVs - verified working
  sprint-burndown-chart.png             actual generated chart
  velocity-chart.png                    actual generated chart
exercises/
  01-writing-user-stories.md
  02-story-point-estimation.md
  03-sprint-planning.md
  04-burndown-chart.md
```

## Verification

This module is process/methodology, not code, so "verification" looks
different here than in earlier modules:

- The charts are real, generated images - I ran
  `charts/generate_charts.py` in this sandbox and viewed both resulting
  PNGs to confirm they render correctly before including them.
- The worked example is internally consistent by construction: Sprint
  3's selected backlog (`sprint-planning-example.md` section 4) sums to
  exactly 9 points, which is exactly what the burndown chart shows
  reaching zero by Day 10; the 8-point Planning Poker outcome in section
  2 matches the 8 points already listed on US-301 in the product
  backlog.
- Every acceptance criteria example was written to actually follow the
  edge-case categories described in
  `templates/acceptance-criteria-template.md` (empty states, validation
  failures, permission boundaries, not-found cases) rather than only
  happy-path scenarios - you can check this by counting the
  Given-When-Then blocks per story in the backlog.
