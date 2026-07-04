# Module 1 – Design Patterns and Principles (C#)

This is a runnable .NET console application covering every learning objective
in **Module 1** of the DN 5.0 Deep Skilling handbook:

- SOLID Principles: SRP, OCP, LSP, ISP, DIP
- Creational Patterns: Singleton, Factory Method, Builder
- Structural Patterns: Adapter, Decorator, Proxy
- Behavioral Patterns: Observer, Strategy, Command

Each topic has its own file with a **"Bad"** example (violating the
principle) and a **"Good"** example (fixed), plus comments explaining why.

## Project layout

```
Module1DesignPatterns/
  SOLID/
    SRP.cs
    OCP.cs
    LSP.cs
    ISP.cs
    DIP.cs
  Patterns/
    Creational/
      Singleton.cs
      FactoryMethod.cs
      Builder.cs
    Structural/
      Adapter.cs
      Decorator.cs
      Proxy.cs
    Behavioral/
      Observer.cs
      Strategy.cs
      Command.cs
  Program.cs
  Module1DesignPatterns.csproj
```

## How to run

**Option 1 – Visual Studio**
1. Open `Module1DesignPatterns.csproj` (or double-click it) in Visual Studio.
2. Press `F5` / `Ctrl+F5` to run.
3. Pick a number from the interactive menu, or choose "Run ALL demos".

**Option 2 – .NET CLI**
```bash
cd Module1DesignPatterns
dotnet run                 # interactive menu
dotnet run -- all          # run every demo in sequence
dotnet run -- srp          # run just the SRP demo
dotnet run -- singleton    # run just the Singleton demo
```

Valid keys for the CLI shortcut: `srp, ocp, lsp, isp, dip, singleton, factory,
builder, adapter, decorator, proxy, observer, strategy, command, all`

Requires the **.NET 8 SDK**.

## Notes for self-evaluation

- Each "Bad" example is intentionally written to compile and run, but to
  demonstrate the *design* problem at runtime (e.g. the LSP demo shows the
  Square/Rectangle area bug actually happening; the ISP demo shows a
  `NotSupportedException` being thrown).
- Read the comment block at the top of each file first — it explains the
  principle/pattern, then walks through why the "Bad" version breaks it and
  how the "Good" version fixes it.
- Try extending each "Good" example yourself as practice, e.g.:
  - Add a `LoyaltyDiscount` to the OCP example.
  - Add a `PushNotificationSender` to the DIP example.
  - Add a `CaramelDecorator` to the Decorator example.
  - Add a `RedoLastCommand` feature to the Command example.
