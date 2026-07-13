namespace OrdersService.Models;

public class Order
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public string BookTitle { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice => UnitPrice * Quantity;
    public DateTime CreatedAtUtc { get; set; }
}

public class CreateOrderRequest
{
    public int BookId { get; set; }
    public int Quantity { get; set; }
}

public class BookLookupResult
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
}
