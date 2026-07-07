namespace Module4.Core.Interfaces;

public interface IDiscountService
{
    decimal GetDiscountPercentage(string customerType, decimal orderAmount);
}
