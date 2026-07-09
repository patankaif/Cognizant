using Microsoft.EntityFrameworkCore;
using Module5.Data;

namespace Module5.ConsoleApp.Demos;

public static class RelationshipsAndLoadingDemo
{
    public static async Task RunAsync(DbContextOptions<AppDbContext> options)
    {
        await EagerLoadingAsync(options);
        await ExplicitLoadingAsync(options);
        await LazyLoadingAsync(options);
    }

    private static async Task EagerLoadingAsync(DbContextOptions<AppDbContext> options)
    {
        await using var context = new AppDbContext(options);

        var authors = await context.Authors
            .Include(a => a.Books)
                .ThenInclude(b => b.BookGenres)
                    .ThenInclude(bg => bg.Genre)
            .Include(a => a.Profile)
            .ToListAsync();

        Console.WriteLine("Eager loading (single query with Include/ThenInclude):");
        foreach (var author in authors)
        {
            Console.WriteLine($"  {author.FirstName} {author.LastName} - {author.Books.Count} book(s)");
            foreach (var book in author.Books)
            {
                var genres = string.Join(", ", book.BookGenres.Select(bg => bg.Genre.Name));
                Console.WriteLine($"    {book.Title} [{genres}]");
            }
        }
    }

    private static async Task ExplicitLoadingAsync(DbContextOptions<AppDbContext> options)
    {
        await using var context = new AppDbContext(options);

        var author = await context.Authors.FirstAsync(a => a.LastName == "Asimov");

        Console.WriteLine();
        Console.WriteLine("Explicit loading (loading related data on demand, after the initial query):");
        Console.WriteLine($"  Loaded author {author.FirstName} {author.LastName}, Books not yet loaded.");

        await context.Entry(author)
            .Collection(a => a.Books)
            .LoadAsync();

        Console.WriteLine($"  After Entry().Collection().LoadAsync(): {author.Books.Count} book(s) now loaded.");

        await context.Entry(author)
            .Reference(a => a.Profile)
            .LoadAsync();

        Console.WriteLine($"  After Entry().Reference().LoadAsync(): Profile loaded = {author.Profile is not null}");
    }

    private static async Task LazyLoadingAsync(DbContextOptions<AppDbContext> options)
    {
        await using var context = new LazyAppDbContext(options);

        var author = await context.Authors.FirstAsync(a => a.LastName == "Austen");

        Console.WriteLine();
        Console.WriteLine("Lazy loading (proxies automatically query the database on first access):");
        Console.WriteLine($"  Loaded author {author.FirstName} {author.LastName} without touching Books yet.");

        var bookCount = author.Books.Count;

        Console.WriteLine($"  Accessing author.Books.Count triggered a query behind the scenes: {bookCount} book(s).");
    }
}
