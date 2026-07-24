# Responsible Use of AI Coding Assistants — Policy Template

A starting template a team could adapt — not a legal document, and not a
substitute for your organization's actual policy if one exists.

## 1. Review standard

All AI-suggested code goes through the same code review process as
human-written code — no exceptions for "the AI wrote it, so it's
probably fine." See `ai-code-risks-checklist.md` for what reviewers
should specifically watch for.

## 2. Data handling

- Do not paste customer data, credentials, API keys, or confidential
  business logic into a prompt unless you've confirmed your
  organization's specific AI tool contract covers that data
  appropriately.
- Be aware of what files/context an AI assistant can automatically see
  in your IDE (see `../copilot-setup/installation-guide.md` for content
  exclusion settings) — "I didn't paste it in manually" doesn't mean it
  wasn't sent as context.

## 3. Testing

Every AI-suggested piece of logic gets the same test coverage
expectation as hand-written code (see Module 4's testing patterns) — a
suggestion "looking right" is not evidence it *is* right; see the
discount-tier bug in `../prompt-engineering-lab/03-chain-of-thought-examples.md`
for a concrete, verified example of confident-looking, wrong code.

## 4. Attribution and licensing

Follow `licensing-and-attribution.md`. When in doubt about a
suggestion's originality, ask rather than assume.

## 5. Accountability

The developer who commits AI-suggested code is responsible for it, in
exactly the same way as if they'd typed it themselves. "The AI
suggested it" is not an acceptable explanation for a bug or a security
issue in review or in a postmortem.

## 6. Disclosure

If your organization or client requires disclosure of AI-assisted
development (increasingly common in contracts and some regulated
industries), follow that requirement — check before assuming it doesn't
apply to your project.

## 7. Encouraged uses

To be clear about what this policy is *for*, not just what it restricts
— explicitly encouraged uses include: boilerplate generation, test
generation, explaining unfamiliar code, drafting documentation,
brainstorming approaches, and accelerating well-understood, low-risk
tasks. The goal of a responsible-use policy is safe adoption, not
avoidance.
