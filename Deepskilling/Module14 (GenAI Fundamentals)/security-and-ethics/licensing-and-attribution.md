# Licensing and Attribution

## The core concern

Code-generating AI models are trained on enormous amounts of publicly
available code, much of it under open-source licenses (MIT, Apache 2.0,
GPL, and others) that carry real legal terms — some require attribution,
some (like GPL/copyleft licenses) can impose requirements on code that
*uses* them. If a model reproduces a large, distinctive chunk of
training data closely enough, using that output in your own codebase can
carry the same licensing obligations the original code had — potentially
without you realizing it, since the suggestion just appears as new text
in your editor with no license notice attached.

In practice this is a genuine but statistically rare risk for typical
everyday completions (a short, generic function has no meaningfully
"copyrightable" originality to it in the first place) — it becomes a
real concern mainly for larger, more distinctive blocks: a whole
algorithm implementation, a large chunk of boilerplate with a
recognizable structure, or anything that looks suspiciously like it
could be copy-pasted from a specific known project.

## What GitHub Copilot does about this

- **Public code filtering (Business/Enterprise)** — an optional setting
  that blocks suggestions matching public code on GitHub above a certain
  length/similarity threshold. Individual-plan users should check
  whether this applies to their account.
- **Code referencing** — Copilot can be configured to show *matches* to
  public repositories alongside a suggestion when the filter is off,
  including which repository and what license it's under, so you can
  make an informed choice rather than an accidental one.

## What you're responsible for regardless

- Your organization's policy on AI-generated code and open-source
  license compliance — if you don't know it, ask; it's not a reasonable
  thing to guess about.
- For any suggestion that looks like it might be a well-known,
  distinctive implementation (a named published algorithm, a
  recognizable library's exact structure) — treat it the same way you'd
  treat manually copy-pasting from Stack Overflow or a GitHub repo: check
  the license, and attribute if the license requires it.
- Don't assume "the AI wrote it" means "it's automatically fine to use
  commercially" — the AI is not a legal shield, and licensing
  obligations attach to the *code*, not to how it was produced.

## A practical rule of thumb

Routine, generic code (a getter/setter, a standard loop, a common
validation check, boilerplate matching a pattern you already established
elsewhere in your own codebase — like the few-shot test-generation
example in `../prompt-engineering-lab/02-few-shot-examples.md`) carries
essentially no realistic licensing risk. The risk rises specifically
when a suggestion is: (1) long, (2) unusually distinctive/recognizable
rather than generic, and (3) something you didn't guide with your own
project-specific context. When in doubt on something that fits that
profile, ask your organization's guidance, or just write it yourself.
