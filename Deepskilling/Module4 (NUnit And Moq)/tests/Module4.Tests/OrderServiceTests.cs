using Module4.Core;
using Module4.Core.Interfaces;
using Moq;
using NUnit.Framework;

namespace Module4.Tests;

[TestFixture]
public class OrderServiceTests
{
    private Mock<IDiscountService> _discountServiceMock = null!;
    private Mock<IInventoryRepository> _inventoryRepositoryMock = null!;
    private Mock<INotificationService> _notificationServiceMock = null!;
    private OrderService _orderService = null!;

    [SetUp]
    public void SetUp()
    {
        _discountServiceMock = new Mock<IDiscountService>();
        _inventoryRepositoryMock = new Mock<IInventoryRepository>();
        _notificationServiceMock = new Mock<INotificationService>();

        _orderService = new OrderService(
            _discountServiceMock.Object,
            _inventoryRepositoryMock.Object,
            _notificationServiceMock.Object);
    }

    [Test]
    public void PlaceOrder_SufficientStock_ReturnsCorrectTotal_StateBasedTest()
    {
        _inventoryRepositoryMock.Setup(r => r.GetStock("SKU-1")).Returns(10);
        _discountServiceMock.Setup(d => d.GetDiscountPercentage("Regular", 100m)).Returns(10m);

        var result = _orderService.PlaceOrder("SKU-1", 2, 50m, "Regular", "test@example.com");

        Assert.That(result.TotalAmount, Is.EqualTo(90m));
    }

    [Test]
    public void PlaceOrder_SufficientStock_ReducesStock_InteractionTest()
    {
        _inventoryRepositoryMock.Setup(r => r.GetStock("SKU-1")).Returns(10);
        _discountServiceMock
            .Setup(d => d.GetDiscountPercentage(It.IsAny<string>(), It.IsAny<decimal>()))
            .Returns(0m);

        _orderService.PlaceOrder("SKU-1", 3, 20m, "Regular", "test@example.com");

        _inventoryRepositoryMock.Verify(r => r.ReduceStock("SKU-1", 3), Times.Once);
    }

    [Test]
    public void PlaceOrder_SufficientStock_SendsConfirmation_InteractionTest()
    {
        _inventoryRepositoryMock.Setup(r => r.GetStock("SKU-1")).Returns(5);
        _discountServiceMock
            .Setup(d => d.GetDiscountPercentage(It.IsAny<string>(), It.IsAny<decimal>()))
            .Returns(0m);

        _orderService.PlaceOrder("SKU-1", 1, 15m, "Regular", "customer@example.com");

        _notificationServiceMock.Verify(
            n => n.SendOrderConfirmation("customer@example.com", It.IsAny<string>()),
            Times.Once);
    }

    [Test]
    public void PlaceOrder_InsufficientStock_ThrowsInvalidOperationException()
    {
        _inventoryRepositoryMock.Setup(r => r.GetStock("SKU-1")).Returns(1);

        Assert.Throws<InvalidOperationException>(() =>
            _orderService.PlaceOrder("SKU-1", 5, 10m, "Regular", "test@example.com"));

        _inventoryRepositoryMock.Verify(r => r.ReduceStock(It.IsAny<string>(), It.IsAny<int>()), Times.Never);
    }

    [Test]
    public void PlaceOrder_InvalidQuantity_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            _orderService.PlaceOrder("SKU-1", 0, 10m, "Regular", "test@example.com"));
    }

    [Test]
    public void Constructor_NullDiscountService_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            new OrderService(null!, _inventoryRepositoryMock.Object, _notificationServiceMock.Object));
    }
}
