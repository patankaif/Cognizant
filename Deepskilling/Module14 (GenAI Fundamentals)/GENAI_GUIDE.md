# Module 14 – Gen AI Fundamentals

Covers every learning objective in **Module 14** of the DN 5.0 Deep
Skilling handbook: Generative AI fundamentals, prompt engineering, and
GitHub Copilot — setup, core features, and responsible/secure use.

This module is unusual compared to the rest of the program: since it's
*about* an AI assistant's capabilities, I was able to actually
demonstrate the techniques live rather than just describe them — writing
real prompts, generating real outputs, and verifying the results by
executing logic in Python and by grepping the actual codebases built in
earlier modules. Two genuine mistakes surfaced and got caught this way
while building it (an invented error-message string, and a discount-tier
bug) — see `prompt-engineering-lab/verification-notes.md` and the notes
at the end of this guide for specifics. That's not a coincidence; it's
the module's central point demonstrated on itself.

## 1. Introduction to Generative AI

**What is Generative AI?** A class of AI models that *generate* new
content — text, code, images, audio — rather than just classifying or
predicting a label for existing input. Ask it for a poem, a function, an
image, or a summary, and it produces new content matching the request,
rather than sorting existing content into categories.

**Generative vs. traditional (discriminative) AI:**

| | Discriminative / traditional AI | Generative AI |
|---|---|---|
| Task | Classify, predict, or score existing input | Produce new content |
| Example | "Is this email spam?" (yes/no) | "Write an email" (new text) |
| Output | A label, a number, a category | Text, code, images, audio |
| Typical use here | A spam filter, a fraud-detection model, a recommendation ranking | GitHub Copilot generating a method body from a comment |

Both are legitimate, widely-used branches of AI — generative models
often have a discriminative model embedded inside them too (a
code-completion model is implicitly "classifying" what token is most
likely next); the distinction is really about what the *output* is used
for.

**Applications of Generative AI:**
- **Text generation** — drafting, summarizing, translating.
- **Code completion and generation** — GitHub Copilot, the focus of the
  rest of this module.
- **Image creation** — text-to-image models (Midjourney, DALL-E,
  Stable Diffusion).
- **Conversational assistants** — chatbots, customer support, coding
  assistants like Copilot Chat (see `copilot-setup/`).

**A brief history/evolution:**
- **1960s** — early rule-based chatbots (ELIZA) — no real "generation",
  just pattern-matched canned responses.
- **2014** — Generative Adversarial Networks (GANs) — two neural
  networks (a generator and a discriminator) trained against each other,
  a major leap for generating realistic images.
- **2020** — GPT-3 — a large language model demonstrating that scaling
  up transformer-based models produced dramatically more coherent,
  general-purpose text generation.
- **2022** — ChatGPT — brought conversational, instruction-following
  generative AI to a mass consumer audience for the first time.
- **GitHub Copilot and beyond** — code-specialized generative models
  integrated directly into the developer's editor, moving generative AI
  from "a separate tool you visit" to "part of the environment you
  already work in" — the specific focus of the rest of this module.

## 2. Prompt Engineering

Prompt engineering is the practice of crafting the input you give an AI
model to reliably get the output you actually want. This module has a
full, hands-on lab with real, verified examples of each core technique —
this section is the map to it, not a repeat of it.

- **Zero-shot prompting** — asking directly, with no examples. Good for
  unambiguous, well-known tasks. See
  `prompt-engineering-lab/01-zero-shot-examples.md` for a real, verified
  example (an ISBN-13 checksum validator, tested against 5 cases) and a
  real example of where zero-shot falls short (a discount calculation
  with no domain context).
- **Few-shot prompting** — providing 1+ examples of the exact pattern you
  want before asking for a new one. See
  `prompt-engineering-lab/02-few-shot-examples.md` — includes a real
  example generating a test matching Module 4's exact NUnit style, and
  another matching a real, grep-verified error-message convention from
  Module 6/7's actual controllers.
- **Chain-of-thought prompting** — asking the model to reason through a
  problem step by step before writing code. See
  `prompt-engineering-lab/03-chain-of-thought-examples.md` — a real,
  Python-verified example of a discount-tiering bug that a direct prompt
  produces and a chain-of-thought prompt avoids.
- **Putting it together: prompt refinement** — see
  `prompt-engineering-lab/04-prompt-refinement-case-study.md`, a single
  scenario refined across four attempts, showing exactly what changed
  and why at each step, converging on code that matches Module 7's real
  `OrdersController.Create` method.

**Best practices, in one place:**
- Give clear, specific instructions — vague prompts get vague (or
  wrongly-guessed) results, as Attempt 1 in the refinement case study
  demonstrates directly.
- Provide context — language, framework, and existing conventions (few-
  shot examples are the strongest form of this).
- Specify the desired output format explicitly when it matters (JSON
  shape, a specific test framework's assertion style).
- Iterate — treat the first response as a draft, not a final answer; see
  the refinement case study for what iterating in response to specific
  gaps actually looks like in practice.

**Ethical considerations in prompting:**
- Don't prompt for content designed to bypass licensing, security, or
  organizational policy.
- Be mindful of what proprietary/sensitive context you include in a
  prompt (see `security-and-ethics/`).
- Avoid prompts that would generate biased or discriminatory logic (e.g.
  scoring/filtering people based on protected characteristics) — the
  same standards that apply to code you'd write yourself apply to code
  you prompt for.

## 3. Introduction to GitHub Copilot

GitHub Copilot is an AI pair programmer, built on a code-specialized
generative language model, integrated directly into your IDE.

**How it works, at a high level:** as you type, Copilot sends relevant
context (the current file, nearby files, sometimes your whole open
workspace) to a code-generation model, which predicts likely
continuations — shown to you as inline "ghost text" you can accept,
reject, or cycle through alternatives for. Copilot Chat, a companion
extension, adds a conversational interface on top of the same underlying
model for more involved, multi-turn requests.

**Supported IDEs and languages:** Visual Studio Code, Visual Studio 2022,
JetBrains IDEs (Rider, IntelliJ, PyCharm, etc.), and Neovim; broad
language support including C#, TypeScript/JavaScript, Python, SQL, and
most other mainstream languages — exactly the stack used throughout this
program.

## 4. Setup and Configuration

Full step-by-step installation for VS Code, Visual Studio, and JetBrains
IDEs, plus first-prompt walkthrough and key configuration options
(per-language enable/disable, content exclusions), lives in
`copilot-setup/installation-guide.md`.

## 5. Core Features and Capabilities

- **Code suggestions and completions** — the everyday "ghost text" as you
  type; accept with Tab.
- **Writing functions/boilerplate from comments** — write a descriptive
  comment, let Copilot draft the implementation (see the zero-shot
  example).
- **Generating documentation** — select code, run `/doc` in Chat.
- **Refactoring and optimizing existing code** — select code, describe
  the goal (readability, performance); see
  `exercises/02-refactor-with-ai-assistance.md`.
- **Creating test cases** — `/tests` in Chat, ideally combined with
  few-shot examples from your existing test suite for consistent style;
  see `exercises/03-generate-tests-with-ai.md`.

Full keyboard shortcuts and slash commands:
`copilot-setup/keyboard-shortcuts-cheatsheet.md`. When to use inline
suggestions vs. the Chat panel for each of these capabilities:
`copilot-setup/copilot-vs-chat-comparison.md`.

## 6. Security and Ethical Considerations

Three focused documents, not just a warning paragraph:
- `security-and-ethics/ai-code-risks-checklist.md` — hallucinated
  correctness, security vulnerabilities, and over-reliance, with a real
  checklist to apply during code review.
- `security-and-ethics/licensing-and-attribution.md` — the licensing
  risk of AI-reproduced training data, and what Copilot does (and
  doesn't) do about it automatically.
- `security-and-ethics/responsible-use-policy-template.md` — a starting
  policy template a real team could adapt.

Hands-on practice applying all of this:
`exercises/04-security-review-checklist.md`.

## Notes

- Every claim in this guide and the linked lab files that references
  *this program's actual code* (Module 4's test style, Module 6/7's
  exact error messages, Module 7's `OrdersController.Create` logic) was
  checked against the real files extracted from the already-delivered
  zips — not written from memory. This module's drafts genuinely
  contained two inaccuracies before that checking happened: an invented
  error message in the few-shot example, and a subtle logic bug
  presented as "correct" C# in an earlier version of the chain-of-thought
  example. Both are documented, corrected, and left visible (rather than
  quietly fixed and hidden) in
  `prompt-engineering-lab/verification-notes.md` and the relevant lab
  files, because catching exactly this kind of mistake is the entire
  point of the module.
- I don't have GitHub Copilot itself (or any live AI coding assistant
  API) available to call from inside this sandbox, so the "generated
  outputs" shown throughout this module are examples I produced myself
  (as the language model writing this module) rather than literal
  Copilot output — but since Copilot is built on the same class of
  code-generating language model this guide describes, the techniques,
  failure modes, and verification habits demonstrated transfer directly.
  Please try the actual prompts shown against real GitHub Copilot
  yourself — see `copilot-setup/installation-guide.md` to get started —
  and expect broadly similar results, though not necessarily
  word-for-word identical ones.
