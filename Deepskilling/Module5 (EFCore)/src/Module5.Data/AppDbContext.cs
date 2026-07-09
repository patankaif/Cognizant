using Microsoft.EntityFrameworkCore;
using Module5.Data.Entities;

namespace Module5.Data;

public class AppDbContext : DbContext
{
    public DbSet<Author> Authors => Set<Author>();
    public DbSet<AuthorProfile> AuthorProfiles => Set<AuthorProfile>();
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Genre> Genres => Set<Genre>();
    public DbSet<BookGenre> BookGenres => Set<BookGenre>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(a => a.AuthorId);
            entity.Property(a => a.FirstName).HasMaxLength(100).IsRequired();
            entity.Property(a => a.LastName).HasMaxLength(100).IsRequired();

            entity.HasOne(a => a.Profile)
                  .WithOne(p => p.Author)
                  .HasForeignKey<AuthorProfile>(p => p.AuthorId);

            entity.HasMany(a => a.Books)
                  .WithOne(b => b.Author)
                  .HasForeignKey(b => b.AuthorId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<AuthorProfile>(entity =>
        {
            entity.HasKey(p => p.AuthorId);
            entity.Property(p => p.Biography).HasMaxLength(2000);
            entity.Property(p => p.Website).HasMaxLength(300);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(b => b.BookId);
            entity.Property(b => b.Title).HasMaxLength(300).IsRequired();
            entity.Property(b => b.Price).HasColumnType("decimal(10,2)");
            entity.HasIndex(b => b.Title);
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(g => g.GenreId);
            entity.Property(g => g.Name).HasMaxLength(100).IsRequired();
            entity.HasIndex(g => g.Name).IsUnique();
        });

        modelBuilder.Entity<BookGenre>(entity =>
        {
            entity.HasKey(bg => new { bg.BookId, bg.GenreId });

            entity.HasOne(bg => bg.Book)
                  .WithMany(b => b.BookGenres)
                  .HasForeignKey(bg => bg.BookId);

            entity.HasOne(bg => bg.Genre)
                  .WithMany(g => g.BookGenres)
                  .HasForeignKey(bg => bg.GenreId);
        });
    }
}
