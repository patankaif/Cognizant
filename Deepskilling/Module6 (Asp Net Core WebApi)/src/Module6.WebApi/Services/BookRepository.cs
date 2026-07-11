using Microsoft.EntityFrameworkCore;
using Module6.WebApi.Data;
using Module6.WebApi.Models;

namespace Module6.WebApi.Services;

public interface IBookRepository
{
    Task<List<Book>> GetAllAsync(decimal? minPrice, decimal? maxPrice);
    Task<Book?> GetByIdAsync(int id);
    Task<Book> AddAsync(Book book);
    Task<bool> UpdateAsync(int id, Book updated);
    Task<bool> DeleteAsync(int id);
}

public class BookRepository : IBookRepository
{
    private readonly AppDbContext _context;

    public BookRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Book>> GetAllAsync(decimal? minPrice, decimal? maxPrice)
    {
        var query = _context.Books.AsNoTracking().AsQueryable();

        if (minPrice.HasValue)
            query = query.Where(b => b.Price >= minPrice.Value);

        if (maxPrice.HasValue)
            query = query.Where(b => b.Price <= maxPrice.Value);

        return await query.OrderBy(b => b.Title).ToListAsync();
    }

    public async Task<Book?> GetByIdAsync(int id)
    {
        return await _context.Books.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<Book> AddAsync(Book book)
    {
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        return book;
    }

    public async Task<bool> UpdateAsync(int id, Book updated)
    {
        var existing = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
        if (existing is null) return false;

        existing.Title = updated.Title;
        existing.Author = updated.Author;
        existing.PublishedYear = updated.PublishedYear;
        existing.Price = updated.Price;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
        if (existing is null) return false;

        _context.Books.Remove(existing);
        await _context.SaveChangesAsync();
        return true;
    }
}
