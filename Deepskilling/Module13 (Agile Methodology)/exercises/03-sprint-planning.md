# Exercise 3 — Sprint Planning

## Setup

Use the product backlog in `product-backlog/product-backlog.md`. Assume
Sprint 3 (from `sprint-planning-example.md`) has just finished, exactly
as described there: 9 of 9 committed points completed.

## Part A — plan Sprint 4

1. Recompute the average velocity using **all three** completed sprints
   now (Sprint 1: 9, Sprint 2: 11, Sprint 3: 9) — is the 3-sprint average
   different from the 2-sprint average used when planning Sprint 3? By
   how much, and does that change what target you'd plan Sprint 4
   against?
2. The team has the following availability for Sprint 4 (a 10-day
   sprint):

   | Team member | Available days |
   |---|---|
   | Dev A | 10 |
   | Dev B | 10 |
   | Dev C | 6 (new hire, only joining mid-sprint, and still ramping up) |
   | QA | 9 |

   Using the same capacity-adjustment approach as
   `sprint-planning-example.md` §3, and using your judgment about a
   still-ramping-up new hire's *effective* contribution (not just their
   raw days), what target would you plan Sprint 4 against?
3. Starting from where the backlog summary table left off (Sprint 3 took
   US-101, US-102, US-202, US-103 — 9 points), select stories for
   Sprint 4 from the remaining backlog, respecting priority order and
   your capacity target from step 2. Show your running total, the same
   way `sprint-planning-example.md` §4 does.

## Part B — handle a mid-planning surprise

While planning Sprint 4, the Product Owner says: "Actually, US-104 (Add
a new book) needs to also let admins upload a cover image — I forgot to
mention that earlier."

1. Does this change the story's INVEST properties? Specifically: is it
   still **Small**, and is it still **Estimable** with the information
   you have right now?
2. Write down what you'd actually do in this situation — re-estimate on
   the spot, split into two stories (text fields now, image upload
   later), or defer the whole story to a future sprint pending more
   details? There's no single right answer — the point is to practice
   recognizing when a story that seemed ready during backlog refinement
   turns out not to be, and having a plan for it instead of just
   guessing.

## Check yourself

- Did your Sprint 4 selection end up including any story out of strict
  priority order, the way the real Sprint 3 example skipped US-201 to
  stay within capacity? If so, can you justify why?
- If Dev C's new-hire ramp-up meant you planned for less than your raw
  velocity number suggested, and the team still hits exactly their
  target — was the velocity number "wrong," or did the capacity
  adjustment do its job?

## Stretch goal

Write the **Sprint Goal** for your Sprint 4 — a single sentence
describing the overall outcome of the sprint, not just a list of ticket
IDs (e.g. "By the end of this sprint, a logged-in reader can log in,
browse, and view individual book details end-to-end"). A good Sprint
Goal is what lets the team make a judgment call if one story turns out
to be harder than expected — do they cut scope on a lower-priority story
to protect the goal, or is the goal itself now at risk?
