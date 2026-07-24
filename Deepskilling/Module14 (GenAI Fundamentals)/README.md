# Module 14 – Gen AI Fundamentals

The final module in the DN 5.0 Deep Skilling handbook. Covers Generative
AI fundamentals, prompt engineering (zero-shot, few-shot, chain-of-
thought), and GitHub Copilot — setup, core features, and secure/
responsible use.

**Start with `GENAI_GUIDE.md`** — it links to every other file at the
point each becomes relevant.

## What makes this module different

Because this module is *about* an AI coding assistant's capabilities, I
could demonstrate the techniques directly rather than only describe
them — real prompts, real generated outputs, checked for correctness by
actually executing logic (Python) and by grepping the real files from
every earlier module's already-delivered zip, rather than trusting
memory. That process caught two genuine mistakes while building this
module — an invented error-message string and a subtle discount-tier
bug — both documented rather than quietly erased, since catching exactly
that kind of mistake is the whole point of the module. See
`prompt-engineering-lab/verification-notes.md` for specifics.

## Contents

```
GENAI_GUIDE.md                                    main guide, all handbook topics
prompt-engineering-lab/
  01-zero-shot-examples.md                        verified ISBN-13 example + a shortfall example
  02-few-shot-examples.md                          NUnit style-matching + grep-verified error messages
  03-chain-of-thought-examples.md                  a real bug, verified by execution, that CoT catches
  04-prompt-refinement-case-study.md               one scenario, 4 refined attempts, verified against real code
  verification-notes.md                            what was actually executed/checked, and how
copilot-setup/
  installation-guide.md                            VS Code / Visual Studio / JetBrains setup
  keyboard-shortcuts-cheatsheet.md                  shortcuts + Chat slash commands + context references
  copilot-vs-chat-comparison.md                     when to use inline suggestions vs. Chat
security-and-ethics/
  ai-code-risks-checklist.md                        hallucination, security, over-reliance
  licensing-and-attribution.md                      training-data licensing risk
  responsible-use-policy-template.md                adaptable team policy
exercises/
  01-write-effective-prompts.md
  02-refactor-with-ai-assistance.md
  03-generate-tests-with-ai.md
  04-security-review-checklist.md
```

## Verification

- The ISBN-13 checksum algorithm and the discount-tier bug were both
  actually executed in Python in this sandbox, with real test output,
  before being presented as C# examples — see `verification-notes.md`.
- Every specific claim about this program's own codebase (Module 4's
  exact test code, Module 6/7's exact error-message strings, Module 7's
  `OrdersController.Create` logic) was checked by extracting the real,
  already-delivered zip files and grepping/reading the actual files —
  not written from memory. This caught and corrected two real mistakes
  during drafting.
- I don't have GitHub Copilot or any live AI assistant API available in
  this sandbox, so the example "generated outputs" throughout are my own
  output rather than literal Copilot output. Please try the same prompts
  against real Copilot and expect broadly similar (not necessarily
  identical) results.

This completes all 14 modules of the DN 5.0 Deep Skilling handbook.
