using Microsoft.EntityFrameworkCore;
using Module5.Data;
using Module5.Data.Entities;

namespace Module5.ConsoleApp.Demos;

public static class SeedDataDemo
{
    public static async Task EnsureSeededAsync(DbContextOptions<AppDbContext> options)
    {
        await using var context = new AppDbContext(options);

        if (await context.Authors.AnyAsync())
            return;

        var fiction = new Genre { Name = "Fiction" };
        var scienceFiction = new Genre { Name = "Science Fiction" };
        var nonFiction = new Genre { Name = "Non-Fiction" };

        var austen = new Author
        {
            FirstName = "Jane",
            LastName = "Austen",
            DateOfBirth = new DateOnly(1775, 12, 16),
            Profile = new AuthorProfile
            {
                Biography = "English novelist known for romantic fiction set among the landed gentry.",
                Website = null
            }
        };

        var asimov = new Author
        {
            FirstName = "Isaac",
            LastName = "Asimov",
            DateOfBirth = new DateOnly(1920, 1, 2),
            Profile = new AuthorProfile
            {
                Biography = "American writer and professor of biochemistry, known for works of science fiction.",
                Website = "https://example.com/asimov"
            }
        };

        var prideAndPrejudice = new Book
        {
            Title = "Pride and Prejudice",
            PublishedYear = 1813,
            Price = 9.99m,
            Author = austen
        };

        var emma = new Book
        {
            Title = "Emma",
            PublishedYear = 1815,
            Price = 11.50m,
            Author = austen
        };

        var foundation = new Book
        {
            Title = "Foundation",
            PublishedYear = 1951,
            Price = 8.75m,
            Author = asimov
        };

        var iRobot = new Book
        {
            Title = "I, Robot",
            PublishedYear = 1950,
            Price = 7.99m,
            Author = asimov
        };

        prideAndPrejudice.BookGenres.Add(new BookGenre { Genre = fiction });
        emma.BookGenres.Add(new BookGenre { Genre = fiction });
        foundation.BookGenres.Add(new BookGenre { Genre = scienceFiction });
        iRobot.BookGenres.Add(new BookGenre { Genre = scienceFiction });
        iRobot.BookGenres.Add(new BookGenre { Genre = nonFiction });

        context.Authors.AddRange(austen, asimov);
        context.Books.AddRange(prideAndPrejudice, emma, foundation, iRobot);

        await context.SaveChangesAsync();
    }
}
