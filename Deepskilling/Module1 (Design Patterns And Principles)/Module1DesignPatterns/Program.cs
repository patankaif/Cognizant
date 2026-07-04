using Module1DesignPatterns.SOLID.SRP;
using Module1DesignPatterns.SOLID.OCP;
using Module1DesignPatterns.SOLID.LSP;
using Module1DesignPatterns.SOLID.ISP;
using Module1DesignPatterns.SOLID.DIP;
using Module1DesignPatterns.Patterns.Creational.Singleton;
using Module1DesignPatterns.Patterns.Creational.FactoryMethod;
using Module1DesignPatterns.Patterns.Creational.Builder;
using Module1DesignPatterns.Patterns.Structural.Adapter;
using Module1DesignPatterns.Patterns.Structural.Decorator;
using Module1DesignPatterns.Patterns.Structural.Proxy;
using Module1DesignPatterns.Patterns.Behavioral.Observer;
using Module1DesignPatterns.Patterns.Behavioral.Strategy;
using Module1DesignPatterns.Patterns.Behavioral.Command;

// Module 1 - Design Patterns and Principles
// Run with `dotnet run` and pick a number, or pass an argument, e.g.:
//   dotnet run -- srp
//   dotnet run -- all

var demos = new (string Key, string Title, Action Run)[]
{
    ("srp",       "SOLID - Single Responsibility Principle", SrpDemo.Run),
    ("ocp",       "SOLID - Open/Closed Principle",            OcpDemo.Run),
    ("lsp",       "SOLID - Liskov Substitution Principle",    LspDemo.Run),
    ("isp",       "SOLID - Interface Segregation Principle",  IspDemo.Run),
    ("dip",       "SOLID - Dependency Inversion Principle",   DipDemo.Run),
    ("singleton", "Creational - Singleton",                   SingletonDemo.Run),
    ("factory",   "Creational - Factory Method",               FactoryMethodDemo.Run),
    ("builder",   "Creational - Builder",                      BuilderDemo.Run),
    ("adapter",   "Structural - Adapter",                      AdapterDemo.Run),
    ("decorator", "Structural - Decorator",                    DecoratorDemo.Run),
    ("proxy",     "Structural - Proxy",                        ProxyDemo.Run),
    ("observer",  "Behavioral - Observer",                     ObserverDemo.Run),
    ("strategy",  "Behavioral - Strategy",                     StrategyDemo.Run),
    ("command",   "Behavioral - Command",                      CommandDemo.Run),
};

void RunAll()
{
    foreach (var demo in demos)
    {
        PrintHeader(demo.Title);
        demo.Run();
        Console.WriteLine();
    }
}

void PrintHeader(string title)
{
    Console.WriteLine(new string('=', 70));
    Console.WriteLine(title);
    Console.WriteLine(new string('=', 70));
}

if (args.Length > 0)
{
    var key = args[0].Trim().ToLowerInvariant();
    if (key == "all")
    {
        RunAll();
        return;
    }

    var match = demos.FirstOrDefault(d => d.Key == key);
    if (match.Run != null)
    {
        PrintHeader(match.Title);
        match.Run();
        return;
    }

    Console.WriteLine($"Unknown demo key '{key}'. Valid keys: all, {string.Join(", ", demos.Select(d => d.Key))}");
    return;
}

while (true)
{
    Console.WriteLine();
    Console.WriteLine("Module 1 - Design Patterns and Principles");
    Console.WriteLine("------------------------------------------");
    for (int i = 0; i < demos.Length; i++)
        Console.WriteLine($" {i + 1,2}. {demos[i].Title}");
    Console.WriteLine($" {demos.Length + 1,2}. Run ALL demos");
    Console.WriteLine("  0. Exit");
    Console.Write("Choose an option: ");

    var input = Console.ReadLine();
    if (!int.TryParse(input, out int choice))
    {
        Console.WriteLine("Please enter a valid number.");
        continue;
    }

    if (choice == 0) break;

    if (choice == demos.Length + 1)
    {
        RunAll();
        continue;
    }

    if (choice >= 1 && choice <= demos.Length)
    {
        var demo = demos[choice - 1];
        Console.WriteLine();
        PrintHeader(demo.Title);
        demo.Run();
    }
    else
    {
        Console.WriteLine("Choice out of range.");
    }
}
