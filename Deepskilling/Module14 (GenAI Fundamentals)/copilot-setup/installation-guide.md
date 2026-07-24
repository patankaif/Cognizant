# GitHub Copilot — Installation and Setup

## Prerequisites

- A GitHub account with an active Copilot subscription (Individual,
  Business, or Enterprise — Copilot is not free, though GitHub has
  offered free tiers/trials at various times; check your current
  account status at github.com/settings/copilot).
- A supported IDE: Visual Studio Code, Visual Studio 2022, the
  JetBrains IDEs (Rider, IntelliJ, etc.), or Neovim.

## Setup in Visual Studio Code

1. Open the Extensions panel (`Ctrl+Shift+X` / `Cmd+Shift+X`).
2. Search for **"GitHub Copilot"** and install it (installing it also
   pulls in the **"GitHub Copilot Chat"** extension, which is a separate
   but companion extension for the chat-based interface).
3. VS Code will prompt you to sign in to GitHub — follow the browser
   authorization flow.
4. Confirm it's active: open any code file and start typing — you should
   see faint, greyed-out "ghost text" suggestions appear as you type.
5. Open the Copilot Chat panel (the chat icon in the sidebar, or
   `Ctrl+Alt+I` / `Cmd+Ctrl+I`) to confirm chat access as well.

## Setup in Visual Studio 2022

1. **Extensions -> Manage Extensions**, search for "GitHub Copilot",
   install, and restart Visual Studio.
2. Sign in via **Tools -> Options -> GitHub -> Copilot**, or via the
   Copilot icon that appears in the top toolbar after installation.
3. Confirm it's active the same way as VS Code: ghost-text suggestions
   while typing, plus a Copilot Chat panel accessible from the sidebar.

## Setup in JetBrains IDEs (Rider, IntelliJ, etc.)

1. **Settings/Preferences -> Plugins -> Marketplace**, search "GitHub
   Copilot", install, and restart the IDE.
2. **Tools -> GitHub Copilot -> Login to GitHub**, follow the device
   authorization flow shown (a code to enter on github.com).
3. Confirm via ghost-text suggestions and the Copilot tool window.

## Your first prompt

Once installed, try this in any file, as a comment:
```csharp
// Write a method that reverses a string
```
Press Enter on a new line below the comment and wait briefly — Copilot
should suggest a complete method. Press `Tab` to accept it, or keep
typing to ignore it.

## Configuration worth knowing about

- **Enable/disable per language** — via the Copilot status icon in the
  status bar, you can turn suggestions off for specific file types (e.g.
  if you don't want suggestions while editing `.json` config files).
- **Content exclusions (Business/Enterprise only)** — administrators can
  configure specific files/paths (e.g. anything containing secrets or
  proprietary algorithms) to be excluded from what Copilot can read as
  context, which matters for the data-privacy considerations covered in
  `../security-and-ethics/`.
- **Copilot in the CLI** — a separate `gh copilot` extension for the
  GitHub CLI, giving command-line suggestions/explanations outside an
  IDE entirely (`gh extension install github/gh-copilot`).

See `keyboard-shortcuts-cheatsheet.md` for the day-to-day shortcuts once
it's installed, and `copilot-vs-chat-comparison.md` for when to reach
for inline suggestions vs. the Chat panel.
