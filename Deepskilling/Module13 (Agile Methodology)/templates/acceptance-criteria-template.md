# Acceptance Criteria Template — Given-When-Then

```
Given [the starting context / precondition]
When  [the action the user takes]
Then  [the expected, observable outcome]
```

This format (also called "Gherkin", from the Cucumber testing tool that
popularized it) turns a fuzzy requirement into something concrete enough
to actually test against — which is exactly what the "T" (Testable) in
INVEST is asking for.

## Why each clause matters

- **Given** sets up the state of the world *before* anything happens.
  Skipping this is the most common mistake — "When I click login, Then
  I'm logged in" doesn't say whether the credentials were valid. Compare
  the two real acceptance criteria on US-201 in the product backlog:
  one **Given** has valid credentials, the other has an incorrect
  password — same **When** (submit the login form), completely
  different, and both necessary, **Then**.
- **When** should usually be a single action. If you find yourself
  writing "When I do X and then Y and then Z", that's often a sign you
  actually have two or three separate scenarios that each deserve their
  own Given-When-Then block — like US-104 in the backlog, which has
  three separate blocks (valid submission, missing title, wrong role)
  instead of cramming all three into one.
- **Then** must be something you can actually *observe* and check —
  "the book appears in the catalog list" is testable; "the system
  handles it correctly" is not.

## Covering the edges, not just the happy path

A story with only one Given-When-Then block almost always only covers
the "happy path" (everything goes right). Good acceptance criteria also
cover:
- **Validation failures** — e.g. US-104's "missing title" scenario.
- **Empty states** — e.g. US-101's "catalog has no books yet" scenario,
  US-302's "never placed an order" scenario.
- **Permission/access boundaries** — e.g. US-104's "logged in as a
  reader, not an admin" scenario, US-401's "not logged in" scenario.
- **Not-found cases** — e.g. US-103's "book doesn't exist" scenario,
  US-301's "book doesn't exist" scenario.

Look back through `../product-backlog/product-backlog.md` — every
story with more than one Given-When-Then block is covering one of these
categories in addition to its happy path. That's not an accident; it's
the actual discipline of writing acceptance criteria well.

## A quick self-check before calling a story "ready"

- [ ] Is there at least one happy-path scenario?
- [ ] Is there at least one "what if this goes wrong" scenario, if the
      story has any way to fail (bad input, no permission, not found)?
- [ ] Could someone who has never seen the UI read these and know
      exactly what to build and how to verify it?
- [ ] Could a tester turn each block directly into a test case with no
      further clarification needed?
