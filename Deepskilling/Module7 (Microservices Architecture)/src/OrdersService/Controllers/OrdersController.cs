using Microsoft.AspNetCore.Mvc;
using OrdersService.Models;
using OrdersService.Services;

namespace OrdersService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private static readonly List<Order> Orders = new();
    private static int _nextId = 1;

    private readonly ICatalogServiceClient _catalogServiceClient;
    private readonly ILogger<OrdersController> _logger;

    public OrdersController(ICatalogServiceClient catalogServiceClient, ILogger<OrdersController> logger)
    {
        _catalogServiceClient = catalogServiceClient;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(Orders);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var order = Orders.FirstOrDefault(o => o.Id == id);
        return order is null
            ? NotFound(new { message = $"Order with id {id} was not found." })
            : Ok(order);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderRequest request, CancellationToken cancellationToken)
    {
        if (request.Quantity <= 0)
            return BadRequest(new { message = "Quantity must be greater than zero." });

        var book = await _catalogServiceClient.GetBookAsync(request.BookId, cancellationToken);
        if (book is null)
        {
            _logger.LogWarning("Order rejected: Book {BookId} not found in CatalogService", request.BookId);
            return BadRequest(new { message = $"Book with id {request.BookId} does not exist." });
        }

        var reserved = await _catalogServiceClient.ReserveStockAsync(request.BookId, request.Quantity, cancellationToken);
        if (!reserved)
            return Conflict(new { message = $"Insufficient stock for '{book.Title}'." });

        var order = new Order
        {
            Id = _nextId++,
            BookId = book.Id,
            BookTitle = book.Title,
            Quantity = request.Quantity,
            UnitPrice = book.Price,
            CreatedAtUtc = DateTime.UtcNow
        };

        Orders.Add(order);

        _logger.LogInformation("Order {OrderId} created for Book {BookId} ({BookTitle}), quantity {Quantity}",
            order.Id, order.BookId, order.BookTitle, order.Quantity);

        return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
    }
}
