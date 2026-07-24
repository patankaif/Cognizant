# Exercise 1 — Writing Effective Prompts

Requires GitHub Copilot (or any AI coding assistant) installed and
signed in — see `../copilot-setup/installation-guide.md`.

## Part A — zero-shot

Ask your assistant (Copilot Chat, or as a comment in a new file):
> Write a C# method that checks whether a given year is a leap year.

1. Read the generated code carefully. Does it handle the actual leap
   year rule correctly (divisible by 4, except centuries, except every
   400th year — e.g. 1900 was *not* a leap year, but 2000 *was*)?
2. Test it yourself against at least these 4 years: 2024, 1900, 2000,
   2023. (You can do this in a scratch file, or even just by tracing
   through the logic by hand — the point is to actually check, not just
   read and assume.)

## Part B — few-shot

1. Find (or write) two existing, real functions in a project you have
   access to that follow a consistent style (validation methods, DTO
   mapping methods, anything with an established pattern).
2. Write a few-shot prompt providing both as examples, then ask for a
   third, following `../prompt-engineering-lab/02-few-shot-examples.md`
   as a model for how to structure the prompt.
3. Compare the result against your two examples — does it genuinely
   match their style, or did it default back to a more "generic" pattern
   in some detail? If so, what part of your examples was the model
   apparently *not* picking up on?

## Part C — chain-of-thought

Pick a piece of logic with a real edge case or ordering dependency (a
tiered pricing rule, a validation with multiple conditions, a date-range
overlap check). Ask for it two ways:

1. Directly: "Write a method that does X."
2. With chain-of-thought: "Before writing the code, walk through the
   edge cases/ordering step by step, specifically covering [name the
   specific boundary conditions you're worried about]. Then write the
   method."

Compare the two outputs. Did the chain-of-thought version actually
change the resulting code, or just add narration around the same
answer? If they're identical, your task may not have had a real
ordering/edge-case trap in it to begin with — try a trickier one.

## Check yourself

- For each of the three parts, could you explain *why* the prompt was
  structured the way it was, not just what it asked for?
- Did you catch anything wrong in a generated response before reading
  this far? If your answer is "no, everything looked fine" — did you
  actually test it, or just read it? (Re-read
  `../security-and-ethics/ai-code-risks-checklist.md` item 1 if you're
  not sure of the difference.)

## Stretch goal

Take a real prompt that produced a mediocre result and run the
refinement process from
`../prompt-engineering-lab/04-prompt-refinement-case-study.md`: identify
specifically what's missing from the output, add exactly that to the
prompt, and repeat until the output is genuinely usable. Keep a record
of each attempt — this is the most useful habit in this entire module to
actually practice, since it's the one you'll use constantly.
