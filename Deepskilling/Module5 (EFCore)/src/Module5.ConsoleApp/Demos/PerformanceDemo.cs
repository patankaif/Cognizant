using Microsoft.EntityFrameworkCore;
using Module5.Data;
using Module5.Data.Entities;

namespace Module5.ConsoleApp.Demos;

public static class PerformanceDemo
{
    private static readonly Func<AppDbContext, string, Task<Book?>> GetBookByTitleCompiled =
        EF.CompileAsyncQuery((AppDbContext context, string title) =>
            context.Books.FirstOrDefault(b => b.Title == title));

    public static async Task RunAsync(DbContextOptions<AppDbContext> options)
    {
        await AsNoTrackingAsync(options);
        await CompiledQueryAsync(options);
        await ConcurrencyTokenAsync(options);
        await BulkOperationsAsync(options);
    }

    private static async Task AsNoTrackingAsync(DbContextOptions<AppDbContext> options)
    {
        await using var context = new AppDbContext(options);

        var books = await context.Books
            .AsNoTracking()
            .Where(b => b.PublishedYear < 2000)
            .ToListAsync();

        Console.WriteLine($"AsNoTracking(): read {books.Count} book(s) with no change-tracking overhead (read-only scenario).");
    }

    private static async Task CompiledQueryAsync(DbContextOptions<AppDbContext> options)
    {
        await using var context = new AppDbContext(options);

        var book = await GetBookByTitleCompiled(context, "Foundation");

        Console.WriteLine(book is null
            ? "Compiled query: 'Foundation' not found."
            : $"Compiled query: found '{book.Title}' without re-parsing the LINQ expression tree each call.");
    }

    private static async Task ConcurrencyTokenAsync(DbContextOptions<AppDbContext> options)
    {
        await using var context1 = new AppDbContext(options);
        await using var context2 = new AppDbContext(options);

        var bookInContext1 = await context1.Books.FirstAsync(b => b.Title == "Emma");
        var bookInContext2 = await context2.Books.FirstAsync(b => b.Title == "Emma");

        bookInContext1.Price += 1.00m;
        await context1.SaveChangesAsync();

        bookInContext2.Price += 2.00m;

        try
        {
            await context2.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            Console.WriteLine("Concurrency token (RowVersion): detected that 'Emma' was already modified by another context; caught DbUpdateConcurrencyException instead of silently overwriting.");
        }
    }

    private static async Task BulkOperationsAsync(DbContextOptions<AppDbContext> options)
    {
        await using var context = new AppDbContext(options);

        var updatedRows = await context.Books
            .Where(b => b.PublishedYear < 1960)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(b => b.Price, b => b.Price + 0.50m));

        Console.WriteLine($"ExecuteUpdateAsync: bumped price on {updatedRows} classic book(s) directly in the database, with no entities loaded into memory.");
    }
}
