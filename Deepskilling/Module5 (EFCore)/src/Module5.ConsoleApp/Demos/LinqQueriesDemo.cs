using Microsoft.EntityFrameworkCore;
using Module5.Data;
using Module5.Data.Dtos;

namespace Module5.ConsoleApp.Demos;

public static class LinqQueriesDemo
{
    public static async Task RunAsync(DbContextOptions<AppDbContext> options)
    {
        await using var context = new AppDbContext(options);

        var cheapBooks = await context.Books
            .Where(b => b.Price < 9.00m)
            .OrderBy(b => b.Price)
            .Select(b => new BookSummaryDto
            {
                BookId = b.BookId,
                Title = b.Title,
                AuthorFullName = b.Author.FirstName + " " + b.Author.LastName,
                Price = b.Price,
                PublishedYear = b.PublishedYear
            })
            .ToListAsync();

        Console.WriteLine("Books under $9.00, cheapest first:");
        foreach (var book in cheapBooks)
            Console.WriteLine($"  {book.Title} by {book.AuthorFullName} - {book.Price:C} ({book.PublishedYear})");

        var booksByGenre = await context.Genres
            .Select(g => new
            {
                g.Name,
                BookCount = g.BookGenres.Count
            })
            .OrderByDescending(g => g.BookCount)
            .ToListAsync();

        Console.WriteLine();
        Console.WriteLine("Book count by genre:");
        foreach (var genre in booksByGenre)
            Console.WriteLine($"  {genre.Name}: {genre.BookCount}");

        var averagePriceByAuthor = await context.Authors
            .Select(a => new
            {
                AuthorName = a.FirstName + " " + a.LastName,
                AveragePrice = a.Books.Average(b => (decimal?)b.Price) ?? 0m,
                BookCount = a.Books.Count
            })
            .Where(a => a.BookCount > 0)
            .ToListAsync();

        Console.WriteLine();
        Console.WriteLine("Average book price by author:");
        foreach (var author in averagePriceByAuthor)
            Console.WriteLine($"  {author.AuthorName}: {author.AveragePrice:C} across {author.BookCount} book(s)");

        var recentBooksTitleOnly = await context.Books
            .Where(b => b.PublishedYear >= 1900)
            .OrderByDescending(b => b.PublishedYear)
            .Select(b => b.Title)
            .ToListAsync();

        Console.WriteLine();
        Console.WriteLine("Books published from 1900 onward (titles only, newest first):");
        foreach (var title in recentBooksTitleOnly)
            Console.WriteLine($"  {title}");
    }
}
