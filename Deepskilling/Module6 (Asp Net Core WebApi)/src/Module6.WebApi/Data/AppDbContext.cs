using Microsoft.EntityFrameworkCore;
using Module6.WebApi.Models;

namespace Module6.WebApi.Data;

public class AppDbContext : DbContext
{
    public DbSet<Book> Books => Set<Book>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().HasData(
            new Book { Id = 1, Title = "Pride and Prejudice", Author = "Jane Austen", PublishedYear = 1813, Price = 9.99m },
            new Book { Id = 2, Title = "Foundation", Author = "Isaac Asimov", PublishedYear = 1951, Price = 8.75m },
            new Book { Id = 3, Title = "1984", Author = "George Orwell", PublishedYear = 1949, Price = 8.99m }
        );
    }
}
