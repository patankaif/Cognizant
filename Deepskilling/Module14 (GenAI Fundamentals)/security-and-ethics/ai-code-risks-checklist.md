# AI-Generated Code — Risks Checklist

Use this as an actual review checklist for any AI-suggested code before
merging it — not just for Copilot, for output from any AI coding
assistant.

## 1. Hallucinated correctness

AI assistants produce **confident, plausible-looking, wrong** answers
regularly — not just occasionally. This module's own drafts hit this
twice while being written: an invented error message string in
`prompt-engineering-lab/02-few-shot-examples.md` that looked completely
plausible but didn't exist anywhere in the real codebase, and a
discount-tiering bug in `prompt-engineering-lab/03-chain-of-thought-examples.md`
that compiled fine and looked correct on a skim. Both were only caught
by actually checking — grepping the real files in one case, executing
the logic against test cases in the other.

**Checklist:**
- [ ] Did you actually run this code, not just read it?
- [ ] Did you test the edge cases (empty input, zero, negative numbers,
      boundary values), not just the obvious happy path?
- [ ] If it references an existing API/library/method, did you confirm
      that method actually exists with that exact signature? (AI models
      will confidently invent plausible-sounding method names that don't
      exist — this is one of the most common and most dangerous failure
      modes.)

## 2. Security vulnerabilities

AI models are trained on huge amounts of public code, including code
with real security flaws — they can and do reproduce those patterns.

**Checklist:**
- [ ] SQL: parameterized queries / an ORM, never raw string
      concatenation of user input into a query.
- [ ] Secrets: no hardcoded API keys, passwords, or connection strings
      (AI-generated example code very often includes a *placeholder*
      secret that looks real enough to accidentally commit).
- [ ] Input validation: does it handle malformed/malicious input, or
      does it assume input is always well-formed?
- [ ] Authentication/authorization: does generated code around
      "protected" functionality actually check permissions, or did it
      just generate the happy path?
- [ ] Generic exception handling: does it swallow exceptions silently
      (`catch (Exception) { }`) in a way that could hide real security
      failures?

## 3. Licensing and attribution risk

**Checklist:**
- [ ] Does the suggestion closely resemble a specific, recognizable
      piece of code you might have seen elsewhere (a well-known
      algorithm implementation, a distinctive comment style)? Very close
      reproduction of copyrighted training data is a real (if
      statistically rare) risk with any code-generating model.
- [ ] Check your organization's policy — GitHub Copilot Business/
      Enterprise includes a "duplicate detection" filter that can block
      suggestions matching public code; know whether it's enabled for
      your account.
- [ ] See `licensing-and-attribution.md` for more detail on this
      specific risk.

## 4. Data privacy — what the assistant can see

**Checklist:**
- [ ] Is any proprietary/confidential code, credentials, or customer
      data present in the file(s) the AI assistant has open/indexed
      while you're using it? (Depending on your organization's Copilot
      plan and settings, file contents may be sent to the service as
      context.)
- [ ] Does your organization have content-exclusion rules configured for
      sensitive paths (see `../copilot-setup/installation-guide.md`)?
      If you're not sure, ask — don't assume.

## 5. Over-reliance / skill atrophy

Not a code-correctness risk, but a real one: accepting suggestions
without understanding them means you can't debug them later, and it
erodes the skill of writing that kind of code yourself over time. A
reasonable personal rule: **if you can't explain what a suggestion does
well enough to modify it without the AI's help, don't merge it yet** —
ask for an explanation first (`/explain` in Copilot Chat), or work
through it manually.

## The single most important habit

**Treat every AI suggestion as a first draft from a fast, well-read, but
occasionally-wrong junior collaborator — not as a finished answer.**
Every example in this module that claims to be "verified" earned that
label by being checked against something authoritative (real code,
executed logic, real test output) — not by looking correct.
