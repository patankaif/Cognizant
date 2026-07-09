using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Module5.ConsoleApp.Demos;
using Module5.Data;

var configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

var connectionString = configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' was not found in appsettings.json.");

var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
optionsBuilder.UseSqlServer(connectionString);
var options = optionsBuilder.Options;

var demos = new (string Key, string Title, Func<DbContextOptions<AppDbContext>, Task> Run)[]
{
    ("seed",         "Seed reference data",                                  SeedDataDemo.EnsureSeededAsync),
    ("crud",         "Basic CRUD Operations (Create/Read/Update/Delete)",    CrudDemo.RunAsync),
    ("linq",         "LINQ Queries (Where, Select, OrderBy, aggregates)",    LinqQueriesDemo.RunAsync),
    ("loading",      "Relationships & Data Loading (Eager/Explicit/Lazy)",   RelationshipsAndLoadingDemo.RunAsync),
    ("performance",  "Performance (AsNoTracking, compiled query, RowVersion concurrency, ExecuteUpdate)", PerformanceDemo.RunAsync),
};

async Task RunAll()
{
    foreach (var demo in demos)
    {
        PrintHeader(demo.Title);
        await demo.Run(options);
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
        await RunAll();
        return;
    }

    var match = demos.FirstOrDefault(d => d.Key == key);
    if (match.Run != null)
    {
        PrintHeader(match.Title);
        await match.Run(options);
        return;
    }

    Console.WriteLine($"Unknown demo key '{key}'. Valid keys: all, {string.Join(", ", demos.Select(d => d.Key))}");
    return;
}

while (true)
{
    Console.WriteLine();
    Console.WriteLine("Module 5 - Entity Framework Core 8.0");
    Console.WriteLine("---------------------------------------");
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
        await RunAll();
        continue;
    }

    if (choice >= 1 && choice <= demos.Length)
    {
        var demo = demos[choice - 1];
        Console.WriteLine();
        PrintHeader(demo.Title);
        await demo.Run(options);
    }
    else
    {
        Console.WriteLine("Choice out of range.");
    }
}
