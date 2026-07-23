# Module 13 – Agile Methodology

Covers every learning objective in **Module 13** of the DN 5.0 Deep
Skilling handbook: the Agile Manifesto, Scrum roles/ceremonies/artifacts,
estimation and planning techniques, and writing effective user stories —
paired with a real, worked example: a full product backlog for the
"Library Catalog" system used throughout Modules 5–8, a sprint planning
walkthrough with genuine Planning Poker back-and-forth, and real
generated burndown/velocity charts (not mockups — see
`charts/generate_charts.py`, which I actually ran to produce them).

## 1. Introduction to Agile & the Agile Manifesto

Agile is a family of iterative approaches to software delivery, built
around adapting to change and delivering working software frequently,
rather than trying to plan an entire project upfront and executing that
plan without deviation.

### The four values of the Agile Manifesto

> **Individuals and interactions** over processes and tools
> **Working software** over comprehensive documentation
> **Customer collaboration** over contract negotiation
> **Responding to change** over following a plan

Each value keeps the item on the right — process, documentation,
contracts, planning still matter — but says the item on the *left* is
valued *more* when the two are in tension. A useful, concrete example
from the product backlog in this module: US-104 (Add a new book) has a
"Notes" field explaining a real dependency and a scope boundary decided
during Planning Poker (see §5, US-301 discussion). That short note is
more valuable to the team than a lengthy up-front specification document
would have been — it's just enough documentation to unblock the
conversation, favoring "working software" and "customer collaboration"
over "comprehensive documentation."

### The twelve principles

1. Highest priority is satisfying the customer through early and
   continuous delivery of valuable software.
2. Welcome changing requirements, even late in development.
3. Deliver working software frequently (weeks, not months).
4. Business people and developers must work together daily.
5. Build projects around motivated individuals; give them the
   environment and support they need, and trust them to get the job
   done.
6. Face-to-face conversation is the most efficient way to convey
   information.
7. Working software is the primary measure of progress.
8. Agile processes promote sustainable development — the team should be
   able to maintain a constant pace indefinitely.
9. Continuous attention to technical excellence and good design
   enhances agility.
10. Simplicity — maximizing the amount of work *not* done — is
    essential.
11. The best architectures, requirements, and designs emerge from
    self-organizing teams.
12. At regular intervals, the team reflects on how to become more
    effective, then tunes and adjusts accordingly.

Principle 10 ("simplicity — maximizing work *not* done") is exactly
what the Planning Poker discussion in `product-backlog/sprint-planning-example.md`
§2 demonstrates: the team explicitly decided real concurrent-order race
handling was *not* needed for US-301 in this sprint, cutting the
estimate from 13 down to 8 points by deliberately not building something
that wasn't yet proven necessary.

### Agile vs. Waterfall

| | Waterfall | Agile |
|---|---|---|
| Phases | Sequential: requirements -> design -> build -> test -> deploy, each fully finished before the next starts | Iterative: small slices of all phases, repeated every sprint |
| Requirements | Fixed upfront, changes are costly | Expected to evolve; the backlog is re-prioritized continuously |
| Feedback | Mostly at the end, once it's "done" | Every sprint (Sprint Review), often earlier |
| Risk | Discovered late (integration/testing phase) | Discovered early (small increments surface problems fast) |
| Best fit | Well-understood, stable requirements (e.g. regulatory/compliance systems with fixed specs) | Evolving requirements, unclear or changing customer needs — most product software |

Neither is universally "better" — Waterfall's predictability is a real
advantage when requirements genuinely are fixed and well understood
(e.g., building to a fixed regulatory spec). Agile's advantage is
adapting cheaply when they aren't, which is the far more common
situation for real product development.

## 2. Scrum Framework — Roles, Ceremonies, Artifacts

Scrum is the most widely used way to actually *do* Agile — a lightweight
framework of roles, events, and artifacts.

### Roles

- **Product Owner** — owns the Product Backlog and its priority order;
  the single voice for "what matters most right now" (see the
  `product-backlog.md` priority column, and the Product Owner's role in
  Sprint Review, §6 of the planning example).
- **Scrum Master** — a facilitator/coach who removes impediments and
  protects the team's process (e.g., pairing with someone within the
  hour when a blocker is raised — the retro action item in
  `sprint-planning-example.md` §6).
- **Development Team** — the people who actually build the increment
  (in the worked example: Dev A, Dev B, Dev C, QA). Cross-functional and
  self-organizing — the team decides *how* to do the work, not just
  *what* work to do.

### Ceremonies (events)

- **Sprint Planning** — the team selects backlog items for the sprint
  and forms a plan for delivering them. Fully worked example:
  `product-backlog/sprint-planning-example.md`.
- **Daily Scrum** (standup) — ~15 minutes, every day, three questions
  (what did I do, what will I do, what's blocking me). See
  `sprint-planning-example.md` §5.
- **Sprint Review** — demo the working increment to stakeholders,
  gather feedback. See §6.
- **Sprint Retrospective** — the team reflects on *how* they worked and
  commits to one or two concrete improvements. See §6 — this is
  Principle 12 from the Manifesto, above, in practice.

### Artifacts

- **Product Backlog** — the full, ordered list of everything that might
  be built. `product-backlog/product-backlog.md` is a complete, realistic
  example: 4 epics, 10 stories, priorities, and story points.
- **Sprint Backlog** — the subset of the Product Backlog the team
  committed to for the current sprint, plus their plan for delivering
  it. See the Sprint 3 selection in `sprint-planning-example.md` §4.
- **Increment** — the sum of everything completed so far, which must
  meet the team's Definition of Done (`templates/definition-of-done.md`)
  to count.

## 3. Agile Estimation & Planning Techniques

- **Story points** — a relative, unitless measure of effort/complexity/
  uncertainty for a story, not a time estimate. "8 points" doesn't mean
  "8 hours" — it means "roughly as much effort as another 8-point story
  the team has already agreed on," which is why relative sizing (see
  Exercise 2, Part C) is often easier and more accurate than estimating
  from nothing.
- **Planning Poker** — the estimation technique itself: everyone
  privately picks a card, reveals simultaneously, and *discusses*
  disagreements rather than averaging them away. A full, realistic
  two-round example — including the exact reasoning that closed a 5-to-13
  point gap — is in `product-backlog/sprint-planning-example.md` §2.
- **Sprint planning** — turning an estimated, prioritized backlog into a
  committed sprint backlog, informed by both **velocity** (historical
  throughput) and **capacity** (actual availability this specific
  sprint, accounting for holidays/leave). Both are computed for real,
  with real numbers, in `sprint-planning-example.md` §1 and §3.
- **Velocity** — story points *completed* per sprint, averaged over
  recent sprints. See `charts/velocity-chart.png` (generated from
  `charts/velocity-data.csv`) for a real 3-sprint trend, including the
  overcommitment in Sprint 1 that's a very typical first-sprint outcome.
- **Burndown charts** — remaining work (in story points) tracked day by
  day across a single sprint, compared against an "ideal" straight-line
  pace. `charts/sprint-burndown-chart.png` is a real chart, generated
  from `charts/burndown-data.csv`, showing an actual blocker (flagged in
  Daily Scrum) as a flat stretch and annotating exactly where it
  happened — this is precisely the kind of pattern a Scrum Master
  watches a burndown chart for.

## 4. Agile User Stories

Covered in full, with a working template and 10 real, complete examples
(not fragments) in:

- `templates/user-story-template.md` — the "As a / I want / So that"
  format, why each clause matters, and the full INVEST checklist.
- `templates/acceptance-criteria-template.md` — the Given-When-Then
  format, why each clause matters, and how to cover edge cases (not just
  the happy path) — with real examples pulled directly from the backlog.
- `product-backlog/product-backlog.md` — 10 fully-written user stories
  across 4 epics, each with acceptance criteria, story points, priority,
  and (where relevant) explicit dependency notes.

## Exercises

Hands-on practice for every topic above lives in `exercises/`:

1. `exercises/01-writing-user-stories.md` — write and INVEST-check new
   stories, critique a deliberately bad one.
2. `exercises/02-story-point-estimation.md` — run a real Planning Poker
   session, understand why estimates diverge.
3. `exercises/03-sprint-planning.md` — plan a full next sprint using
   velocity, capacity, and backlog priority.
4. `exercises/04-burndown-chart.md` — interpret the real chart, then
   build and generate your own from scratch.

## Notes

- The "Library Catalog" domain used throughout this module is the same
  one built out in Modules 5 (EF Core), 6 (Web API), 7 (Microservices),
  and 8 (Angular) — the backlog here is written as if it were the actual
  requirements process that preceded that code, so if you've been
  through those modules, every story should feel familiar rather than
  abstract.
- The burndown and velocity charts are real, generated images — I ran
  `charts/generate_charts.py` against `charts/burndown-data.csv` and
  `charts/velocity-data.csv` in this sandbox to produce them, and viewed
  both to confirm they render correctly (the blocker annotation lands on
  the correct flat segment, axis labels are correct, etc.) before
  including them. The script is fully reproducible — rerun it yourself
  any time, or edit the CSV data and rerun to see how the chart changes.
- Everything in this module is process/methodology rather than code, so
  there's no build or test step to run — the "verification" here is that
  the worked example (backlog -> estimation -> sprint plan -> burndown)
  is internally consistent: the 9-point Sprint 3 backlog in
  `sprint-planning-example.md` §4 sums to exactly the 9 points that
  burn down to zero by Day 10 in the chart, and the 8-point estimate
  reached in the Planning Poker walkthrough in §2 is the same number
  already sitting on US-301 in the product backlog.
