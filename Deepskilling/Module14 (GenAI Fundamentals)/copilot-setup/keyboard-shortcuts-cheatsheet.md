# GitHub Copilot — Keyboard Shortcuts (VS Code defaults)

| Action | Windows/Linux | Mac |
|---|---|---|
| Accept the current suggestion | `Tab` | `Tab` |
| Dismiss the current suggestion | `Esc` | `Esc` |
| See the next alternative suggestion | `Alt+]` | `Option+]` |
| See the previous alternative suggestion | `Alt+[` | `Option+[` |
| Open a panel with multiple full suggestions | `Ctrl+Enter` | `Cmd+Enter` |
| Trigger a suggestion manually (if none showing) | `Alt+\` | `Option+\` |
| Open Copilot Chat | `Ctrl+Alt+I` | `Cmd+Ctrl+I` |
| Inline chat at the cursor | `Ctrl+I` | `Cmd+I` |

## Useful Copilot Chat slash commands

Typed inside the Chat panel:

| Command | What it does |
|---|---|
| `/explain` | Explains the selected code |
| `/fix` | Proposes a fix for a problem in the selected code |
| `/tests` | Generates unit tests for the selected code |
| `/doc` | Generates documentation comments for the selected code |
| `/optimize` | Suggests a more efficient version of the selected code |
| `/new` | Scaffolds a new file or project from a description |

## Chat context references

Typed inside a Chat prompt, to give it more precise context than "the
currently open file":

| Reference | What it includes |
|---|---|
| `#file:path/to/file.cs` | The full contents of a specific file |
| `#selection` | Just your current text selection |
| `#editor` | The full contents of the active editor |
| `@workspace` | Lets Copilot search across your whole open project, not just the current file |

**Example, tying two of these together** — the kind of prompt that
produces the few-shot-quality results shown in
`../prompt-engineering-lab/02-few-shot-examples.md`:
```
@workspace Looking at #file:Module4-NUnitAndMoq/tests/Module4.Tests/CalculatorTests.cs,
write a test for the Multiply method following the exact same style.
```
