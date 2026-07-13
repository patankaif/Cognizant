using CatalogService.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly CatalogDbContext _context;
    private readonly ILogger<BooksController> _logger;

    public BooksController(CatalogDbContext context, ILogger<BooksController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var books = await _context.Books.AsNoTracking().ToListAsync();
        return Ok(books);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var book = await _context.Books.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
        if (book is null)
        {
            _logger.LogWarning("Book {BookId} was requested but does not exist", id);
            return NotFound(new { message = $"Book with id {id} was not found." });
        }

        return Ok(book);
    }

    [HttpPost("{id:int}/reserve-stock")]
    public async Task<IActionResult> ReserveStock(int id, [FromQuery] int quantity)
    {
        var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
        if (book is null)
            return NotFound(new { message = $"Book with id {id} was not found." });

        if (book.StockQuantity < quantity)
        {
            _logger.LogWarning("Insufficient stock for Book {BookId}: requested {Requested}, available {Available}",
                id, quantity, book.StockQuantity);
            return Conflict(new { message = "Insufficient stock." });
        }

        book.StockQuantity -= quantity;
        await _context.SaveChangesAsync();

        return Ok(book);
    }
}
