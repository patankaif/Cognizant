using Microsoft.EntityFrameworkCore;
using Module5.Data;
using Module5.Data.Entities;

namespace Module5.ConsoleApp.Demos;

public static class CrudDemo
{
    public static async Task RunAsync(DbContextOptions<AppDbContext> options)
    {
        await CreateAsync(options);
        await ReadAsync(options);
        await UpdateAsync(options);
        await DeleteAsync(options);
    }

    private static async Task CreateAsync(DbContextOptions<AppDbContext> options)
    {
        await using var context = new AppDbContext(options);

        var existing = await context.Authors
            .FirstOrDefaultAsync(a => a.LastName == "Orwell");

        if (existing is not null)
            return;

        var orwell = new Author
        {
            FirstName = "George",
            LastName = "Orwell",
            DateOfBirth = new DateOnly(1903, 6, 25)
        };

        var nineteenEightyFour = new Book
        {
            Title = "1984",
            PublishedYear = 1949,
            Price = 8.99m,
            Author = orwell
        };

        await context.Authors.AddAsync(orwell);
        await context.Books.AddAsync(nineteenEightyFour);
        await context.SaveChangesAsync();

        Console.WriteLine($"Created Author '{orwell.FirstName} {orwell.LastName}' with Book '{nineteenEightyFour.Title}'.");
    }

    private static async Task ReadAsync(DbContextOptions<AppDbContext> options)
    {
        await using var context = new AppDbContext(options);

        var book = await context.Books
            .Include(b => b.Author)
            .FirstOrDefaultAsync(b => b.Title == "1984");

        Console.WriteLine(book is null
            ? "Book '1984' not found."
            : $"Found: {book.Title} ({book.PublishedYear}) by {book.Author.FirstName} {book.Author.LastName}, {book.Price:C}");

        var allBooks = await context.Books.ToListAsync();
        Console.WriteLine($"Total books in database: {allBooks.Count}");
    }

    private static async Task UpdateAsync(DbContextOptions<AppDbContext> options)
    {
        await using var context = new AppDbContext(options);

        var book = await context.Books.FirstOrDefaultAsync(b => b.Title == "1984");
        if (book is null)
        {
            Console.WriteLine("Nothing to update; '1984' not found.");
            return;
        }

        var oldPrice = book.Price;
        book.Price = 6.99m;
        await context.SaveChangesAsync();

        Console.WriteLine($"Updated '{book.Title}' price from {oldPrice:C} to {book.Price:C}.");
    }

    private static async Task DeleteAsync(DbContextOptions<AppDbContext> options)
    {
        await using var context = new AppDbContext(options);

        var author = await context.Authors
            .Include(a => a.Books)
            .FirstOrDefaultAsync(a => a.LastName == "Orwell");

        if (author is null)
        {
            Console.WriteLine("Nothing to delete; Orwell not found.");
            return;
        }

        context.Books.RemoveRange(author.Books);
        context.Authors.Remove(author);
        await context.SaveChangesAsync();

        Console.WriteLine($"Deleted Author '{author.FirstName} {author.LastName}' and their books.");
    }
}
