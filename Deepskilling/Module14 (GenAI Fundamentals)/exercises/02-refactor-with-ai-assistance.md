# Exercise 2 — Refactoring with AI Assistance

## Setup

Use the intentionally-buggy `book-counter.ts`/`buggy-utils.ts` files from
**Module 9's Debug Lab** (`Module9-ApplicationDebugging/src/app/features/debug-lab/`)
if you have them, or any code you already know has room for improvement.

## Tasks

1. Select a function with a real issue (from Module 9's debug lab, or
   any function you wrote earlier in this program that you know could
   be cleaner or has a subtle bug).
2. Ask your AI assistant to `/explain` it first, *before* asking it to
   fix anything. Does the explanation match your own understanding of
   what the code does? If it doesn't, that's worth investigating before
   you even get to the fix — either your understanding or the AI's is
   wrong, and you need to know which before trusting a refactor.
3. Now ask it to refactor or fix the issue. Don't accept the suggestion
   yet.
4. Before accepting: write out, in your own words, what specifically
   changed and why. If you can't explain the diff, don't merge it — go
   back and ask a follow-up question until you can (see
   `../security-and-ethics/ai-code-risks-checklist.md`, item 5).
5. Test the refactored version against the same test cases you'd use for
   the original (or write new ones if none exist — see Exercise 3).

## A specific, worthwhile refactor to try

Take the buggy discount-tier logic pattern from
`../prompt-engineering-lab/03-chain-of-thought-examples.md` (the version
that checks `>= 100` before `>= 250`) and ask your assistant to review it
for bugs *without* telling it what the bug is. Does it catch the
unreachable-branch issue on its own? This is a good calibration exercise
for how much you can trust "review this code for bugs" as a prompt by
itself, versus needing to point it at specific things to check.

## Check yourself

- Did you test the *before* and *after* versions against the same
  inputs, so you have real evidence the refactor didn't change behavior
  (or, if it was a bug fix, that it changed behavior in exactly the
  intended way)?
- Would you be comfortable explaining this refactor to a reviewer who
  asks "why did you make this change" — in your own words, not by
  quoting the AI's explanation back at them?

## Stretch goal

Ask your assistant to refactor the same piece of code twice, with
slightly different prompts (e.g. "make this more readable" vs. "make
this more performant") — compare the two results. Performance and
readability refactors sometimes pull in different directions; seeing
this happen concretely is a good reminder that "refactor this" is an
underspecified prompt unless you say what you're optimizing for.
