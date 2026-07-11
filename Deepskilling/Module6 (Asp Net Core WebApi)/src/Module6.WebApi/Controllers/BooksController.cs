using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Module6.WebApi.Models;
using Module6.WebApi.Models.Dtos;
using Module6.WebApi.Services;

namespace Module6.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookRepository _repository;

    public BooksController(IBookRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<BookDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice)
    {
        var books = await _repository.GetAllAsync(minPrice, maxPrice);
        return Ok(books.Select(ToDto));
    }

    [HttpGet("{id:int}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(BookDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var book = await _repository.GetByIdAsync(id);
        if (book is null)
            return NotFound(new { message = $"Book with id {id} was not found." });

        return Ok(ToDto(book));
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(BookDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Create([FromBody] CreateBookDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var book = new Book
        {
            Title = dto.Title,
            Author = dto.Author,
            PublishedYear = dto.PublishedYear,
            Price = dto.Price
        };

        var created = await _repository.AddAsync(book);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, ToDto(created));
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateBookDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updated = new Book
        {
            Title = dto.Title,
            Author = dto.Author,
            PublishedYear = dto.PublishedYear,
            Price = dto.Price
        };

        var success = await _repository.UpdateAsync(id, updated);
        if (!success)
            return NotFound(new { message = $"Book with id {id} was not found." });

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _repository.DeleteAsync(id);
        if (!success)
            return NotFound(new { message = $"Book with id {id} was not found." });

        return NoContent();
    }

    private static BookDto ToDto(Book book) => new()
    {
        Id = book.Id,
        Title = book.Title,
        Author = book.Author,
        PublishedYear = book.PublishedYear,
        Price = book.Price
    };
}
