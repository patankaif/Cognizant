using Module4.Core.Interfaces;

namespace Module4.Core;

public class OrderResult
{
    public string OrderId { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
}

public class OrderService
{
    private readonly IDiscountService _discountService;
    private readonly IInventoryRepository _inventoryRepository;
    private readonly INotificationService _notificationService;

    public OrderService(
        IDiscountService discountService,
        IInventoryRepository inventoryRepository,
        INotificationService notificationService)
    {
        _discountService = discountService ?? throw new ArgumentNullException(nameof(discountService));
        _inventoryRepository = inventoryRepository ?? throw new ArgumentNullException(nameof(inventoryRepository));
        _notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
    }

    public OrderResult PlaceOrder(string sku, int quantity, decimal unitPrice, string customerType, string customerEmail)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));

        var availableStock = _inventoryRepository.GetStock(sku);
        if (availableStock < quantity)
            throw new InvalidOperationException(
                $"Insufficient stock for SKU '{sku}'. Available: {availableStock}, Requested: {quantity}.");

        var subtotal = unitPrice * quantity;
        var discountPercentage = _discountService.GetDiscountPercentage(customerType, subtotal);
        var total = subtotal - (subtotal * discountPercentage / 100m);

        _inventoryRepository.ReduceStock(sku, quantity);

        var orderId = Guid.NewGuid().ToString("N").Substring(0, 8).ToUpperInvariant();
        _notificationService.SendOrderConfirmation(customerEmail, orderId);

        return new OrderResult { OrderId = orderId, TotalAmount = total };
    }
}
