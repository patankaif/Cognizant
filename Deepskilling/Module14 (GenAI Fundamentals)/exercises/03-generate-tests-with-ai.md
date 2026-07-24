# Exercise 3 — Generating Tests with AI

## Setup

Pick a method you've already written in this program with no test
coverage yet, or use `Calculator.Divide` from Module 4 as a stand-in if
you don't have one handy.

## Tasks

1. Select the method, and use the `/tests` slash command (or just ask
   "write unit tests for this") in Copilot Chat.
2. Before running the generated tests, read through them and predict:
   which ones will pass, and are there any inputs you'd have tested that
   the AI didn't think of?
3. Run them for real. Compare the actual results to your prediction.
4. Specifically check: did it generate a test for the *failure* case
   (e.g. `Divide` by zero throwing `DivideByZeroException`), or only
   happy-path cases? AI-generated tests, like AI-generated code, tend to
   default toward the obvious happy path unless prompted otherwise — the
   same tendency you saw in the "what a direct prompt can produce"
   section of
   `../prompt-engineering-lab/03-chain-of-thought-examples.md`.
5. If any edge case is missing, use a **few-shot** prompt: show it one
   of the edge-case tests already in Module 4's `CalculatorTests.cs`
   (e.g. `Divide_ByZero_ThrowsDivideByZeroException`) as an example of
   the kind of case you want covered, and ask it to generate more like
   that one for your method.

## Check yourself

- Did the AI-generated tests actually increase your confidence in the
  code, or did they just restate the code's own logic back as
  assertions (a common failure mode — a test that just mirrors the
  implementation will pass even if the implementation is wrong, since it
  never checks against an independently-known-correct expected value)?
- For each generated test, could you explain what specific behavior it's
  protecting against regressing?

## Stretch goal

Deliberately introduce a bug into the method you tested (e.g. change a
`<` to a `<=`), then re-run the AI-generated test suite. Do the tests
actually catch it? If not, that's valuable information about the test
suite's real coverage — and a good illustration of why "AI generated
tests for it" isn't automatically the same as "it's well tested."
