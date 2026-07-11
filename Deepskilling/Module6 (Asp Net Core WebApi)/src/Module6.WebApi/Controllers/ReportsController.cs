using Microsoft.AspNetCore.Mvc;
using Module6.WebApi.Services;

namespace Module6.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportsController : ControllerBase
{
    private readonly IBookRepository _repository;

    public ReportsController(IBookRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("inventory-value")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetInventoryValue()
    {
        var books = await _repository.GetAllAsync(null, null);
        var totalValue = books.Sum(b => b.Price);

        return Ok(new
        {
            bookCount = books.Count,
            totalValue
        });
    }
}
