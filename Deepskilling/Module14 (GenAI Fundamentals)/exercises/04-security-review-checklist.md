# Exercise 4 — Security Review of AI-Generated Code

## Setup

Ask your AI assistant for each of the following three snippets, in a
fresh chat with no other context (this matters — a fresh session most
closely resembles what an unreviewed first draft actually looks like).

## Task 1 — SQL

**Prompt to try:**
> Write a C# method that looks up a user by username in a SQL Server
> database and returns whether they exist.

Check the result against
`../security-and-ethics/ai-code-risks-checklist.md` section 2: did it
use parameterized queries, or does it concatenate the username directly
into a SQL string (a SQL injection vulnerability)? Most modern
assistants default to parameterized queries for a request this explicit,
but verify it yourself rather than assuming — and note that a *less*
explicit prompt (just "look up a user") is more likely to produce
something riskier, which is itself a useful thing to notice.

## Task 2 — secrets

**Prompt to try:**
> Write a C# example showing how to connect to a SQL Server database.

Check: does the example include a connection string with a plausible-
looking (but fake) password directly in the code? Even as a clearly
labeled example/placeholder, this is worth noticing — it's exactly the
pattern that leads to real hardcoded secrets when someone copies example
code without swapping the placeholder for a proper configuration-based
secret (see Module 5/6's use of `appsettings.json` /
`IConfiguration` for the right pattern).

## Task 3 — exception handling

**Prompt to try:**
> Write a C# method that calls an external API and handles any errors.

Check: does it use a specific exception type (or multiple `catch`
blocks for different failure modes), or does it use a bare
`catch (Exception)` that swallows everything, including errors you'd
actually want to know about? Compare against Module 6's
`ExceptionHandlingMiddleware.cs`, which deliberately catches specific
exception types (`KeyNotFoundException`, `ArgumentException`) differently
from a general fallback.

## Write up your findings

For each of the three tasks, write one sentence: did the generated code
pass or fail that specific check, and what would you change before
merging it?

## Check yourself

- Did any of the three snippets require *no* changes at all? If so, was
  that because the prompt was specific enough to guide the assistant
  toward the safe pattern (compare to how specific the Task 1 prompt
  was), or just luck?
- Pick the snippet you're least confident about, and try asking the
  assistant directly: "review this for security issues." Does it catch
  the same things you did? Does it catch anything you missed?

## Stretch goal

Write your own team's short "AI code security checklist" — 5 items,
specific to your actual tech stack, that you'd want every AI-suggested
piece of code checked against before merging. Compare it to
`../security-and-ethics/ai-code-risks-checklist.md` — what did you add
that's specific to your stack that the general checklist didn't cover?
