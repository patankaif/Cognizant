# Sprint Planning Example — Sprint 3

## 1. Team velocity so far

Velocity = story points **completed** (not just started) per sprint. You
can't measure it before a team has run any sprints — the first sprint or
two are usually a rough guess, and the number becomes reliable once
there's real history.

| Sprint | Committed Points | Completed Points |
|---|---|---|
| Sprint 1 | 12 | 9 |
| Sprint 2 | 10 | 11 |

**Average velocity (last 2 sprints):** (9 + 11) / 2 = **10 points/sprint**

Sprint 1 overcommitted (finished 9 of 12) — a very normal first-sprint
outcome, since the team hadn't yet calibrated what a point means for
*them* specifically. Sprint 2 came in slightly ahead. Using 10 as a
planning target for Sprint 3 is a reasonable, evidence-based number —
notably, it's not "the team's maximum output," it's "what this team has
reliably delivered," which is exactly what you want a planning number to
represent.

## 2. Planning Poker — estimating US-301 ("Place an order")

Planning Poker: everyone privately picks a card (from the Fibonacci-like
sequence 1, 2, 3, 5, 8, 13, ...), then all reveal at once — this
prevents the first person to speak from anchoring everyone else's
estimate.

**Round 1 — reveal:**

| Estimator | Card |
|---|---|
| Dev A | 5 |
| Dev B | 13 |
| Dev C | 5 |
| QA | 8 |

Big spread (5 vs. 13) — this is the signal to *discuss*, not to average
the numbers together. Dev B explains their reasoning:

> "I included handling the case where two orders for the last unit of
> stock race against each other — if we need real concurrency handling
> here, not just a simple stock check, that's a lot more work."

The team decides: for this sprint, "insufficient stock" just needs a
simple check-then-decrement (matching what was actually built in
Module 7's `CatalogService.ReserveStock`) — true concurrent-order race
conditions are out of scope for this story and can be a separate,
future story if it turns out to matter in practice.

**Round 2 — reveal, after discussion:**

| Estimator | Card |
|---|---|
| Dev A | 8 |
| Dev B | 8 |
| Dev C | 5 |
| QA | 8 |

Much closer. The team agrees to settle on **8** (majority, and Dev C is
comfortable rounding up once the scope was clarified). This is also why
the backlog above already lists US-301 at 8 points — that's the outcome
of this exact conversation.

## 3. Sprint capacity

Velocity tells you *roughly* how many points to plan for, but real teams
also sanity-check against actual availability, since holidays and
planned time off don't show up in a velocity number:

| Team member | Available days this sprint (of 10) | Notes |
|---|---|---|
| Dev A | 10 | Full sprint |
| Dev B | 8 | 2 days of planned leave |
| Dev C | 10 | Full sprint |
| QA | 10 | Full sprint |

Dev B is at 80% availability. A simple capacity adjustment:
`10 points x (38 available person-days / 40 full person-days) = 9.5 ≈ 9-10 points`

This roughly confirms the velocity-based number of 10 — in this case
capacity doesn't force a change, but if Dev B had been out for the whole
sprint, the team would deliberately plan for less than their usual
velocity rather than pretend the missing capacity doesn't matter.

## 4. Selecting the Sprint Backlog

Working down the prioritized backlog (see `product-backlog.md`), adding
stories until reaching the ~10 point target:

| Story | Points | Running total |
|---|---|---|
| US-101 Browse the catalog | 3 | 3 |
| US-102 Search the catalog | 3 | 6 |
| US-202 Log out | 1 | 7 |
| US-103 View book details | 2 | 9 |

Adding the next story in priority order, US-201 (Log in, 5 points),
would push the total to 14 — noticeably over both the velocity target
(10) and the capacity-adjusted range (9–10). The team leaves US-201 for
Sprint 4 rather than overcommitting again the way Sprint 1 did.

**Sprint 3 Backlog: US-101, US-102, US-202, US-103 — 9 points total.**

Note this intentionally skips strict top-to-bottom order: US-201
(Login) is higher priority than US-103 (View book details), but pulling
it in would blow the sprint capacity. A good sprint backlog respects
priority order for what it *includes*, but the cutoff is set by capacity,
not by an arbitrary story count.

## 5. Mid-sprint tracking — the Daily Scrum

Each day, in ~15 minutes, every team member answers three questions:
1. What did I do yesterday to help the team reach the Sprint Goal?
2. What will I do today?
3. Is anything blocking me?

This isn't a status report to a manager — it's the team synchronizing
with *itself*. See `../charts/sprint-burndown-chart.png` for how the
team's remaining work actually tracked, day by day, across this sprint —
including a real blocker (flagged in a Daily Scrum) that shows up as a
flat stretch in the chart on Day 6.

## 6. Sprint Review and Retrospective

- **Sprint Review** — at the end of the sprint, the team demos what's
  actually done (meets Definition of Done — see
  `../templates/definition-of-done.md`) to stakeholders and gathers
  feedback, which often reshapes the Product Backlog for future sprints.
- **Sprint Retrospective** — a separate, internal meeting: what went
  well, what didn't, and one or two concrete process changes to try next
  sprint. For Sprint 3, based on the burndown chart's Day 6 stall, a
  realistic retro action item would be: "When a blocker is raised in
  Daily Scrum, the team lead pairs with that person within the hour
  instead of waiting for the next day's standup."
