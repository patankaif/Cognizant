using CatalogService.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Data;

public class CatalogDbContext : DbContext
{
    public DbSet<Book> Books => Set<Book>();

    public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().HasData(
            new Book { Id = 1, Title = "Pride and Prejudice", Author = "Jane Austen", Price = 9.99m, StockQuantity = 25 },
            new Book { Id = 2, Title = "Foundation", Author = "Isaac Asimov", Price = 8.75m, StockQuantity = 12 },
            new Book { Id = 3, Title = "1984", Author = "George Orwell", Price = 8.99m, StockQuantity = 30 }
        );
    }
}
