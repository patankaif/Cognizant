# Inline Suggestions vs. Copilot Chat — When to Use Each

Copilot actually offers two quite different interaction styles, and
picking the right one for the task saves real time.

## Inline "ghost text" suggestions

Copilot silently suggests code as you type, right where your cursor is.

**Best for:**
- Completing a line or block you've already started (it's extremely
  good at "finish the pattern" once you've written the first line or
  two of a loop, a switch statement, or a repetitive block).
- Writing a function from a well-named signature or a one-line comment
  directly above it (see the zero-shot example in
  `../prompt-engineering-lab/01-zero-shot-examples.md` — that's exactly
  this workflow: write the doc comment, let Copilot suggest the body).
- Boilerplate: constructors, DTOs, repetitive test cases once the first
  one or two exist.

**Weaker for:**
- Anything needing back-and-forth clarification.
- Multi-file changes.
- Explaining *why*, not just producing code.

## Copilot Chat

A conversational panel — you type a request, optionally reference
specific files (`#file:...`) or your whole workspace (`@workspace`), and
get a full response with explanation, not just code.

**Best for:**
- Few-shot and chain-of-thought prompting (see the lab files in
  `../prompt-engineering-lab/`) — these need room to write a multi-part
  prompt, which doesn't fit naturally into "write a comment and wait."
- "Explain this code to me" / "why is this failing" — genuinely
  conversational tasks.
- Generating something new from a description with no existing code to
  pattern-match against yet (a new test file, a new component).
- Multi-step tasks via slash commands (`/tests`, `/fix`, `/doc` — see
  `keyboard-shortcuts-cheatsheet.md`).

**Weaker for:**
- Fast, in-flow completion while actively writing code — switching to a
  chat panel and back has real context-switching cost for small,
  obvious completions.

## A realistic workflow combining both

1. Start writing a new method with a descriptive name and a doc comment
   — let **inline suggestions** draft the body.
2. Select the generated method, open **Chat**, and run `/tests` to
   generate a test file for it.
3. If the generated code doesn't match your project's conventions, open
   **Chat** again with a few-shot prompt (paste 1-2 existing examples)
   rather than trying to coax inline suggestions into the right style
   through trial and error.
4. Before committing, select the new code and ask Chat to `/explain` it
   back to you — if the explanation doesn't match what you intended it
   to do, that's a strong signal to re-read it carefully rather than
   trust it (see `../security-and-ethics/ai-code-risks-checklist.md`).
