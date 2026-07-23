# Definition of Done — Library Catalog Team

A story is not "Done" just because the code was written. **Definition of
Done (DoD)** is the team's shared checklist for what "finished" actually
means — agreed on once, upfront, and applied to *every* story, so
"Done" means the same thing regardless of who worked on it.

This is different from a story's own **Acceptance Criteria** (which
describe what makes *that specific story* correct). DoD is the baseline
quality bar every story must also clear, on top of its own acceptance
criteria.

## This team's Definition of Done

A story is Done when **all** of the following are true:

- [ ] Code implements all of the story's acceptance criteria
- [ ] Unit tests are written and passing for the new/changed logic
      (see Module 4's NUnit/Moq patterns)
- [ ] Code has been reviewed and approved by at least one other
      developer
- [ ] The CI pipeline is green (build + tests pass — see Module 11)
- [ ] No new compiler warnings or linter errors were introduced
- [ ] Any new API endpoint is documented in Swagger (see Module 6)
- [ ] The feature has been manually verified in a running environment,
      not just covered by automated tests
- [ ] Product Owner has reviewed the working feature and accepts it
      meets the story's intent

## Why this matters for estimation and planning

A team's **velocity** (see `sprint-planning-example.md` §1) is only a
meaningful number if "completed" means the same thing every sprint. If
one story is called "done" with no tests and another is called "done"
with full test coverage and a code review, the story-point numbers stop
being comparable to each other — which quietly poisons every future
sprint planning session, since the whole point of tracking velocity is
being able to trust it.

This is also why a story can be "code complete" but not yet "Done" — if
a story is waiting on code review or CI is red, the honest answer in
Daily Scrum is "in progress," not "done," even if all the acceptance
criteria appear to work when you run it locally.
