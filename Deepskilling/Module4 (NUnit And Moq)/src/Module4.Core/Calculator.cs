using Module4.Core.Interfaces;

namespace Module4.Core;

public class Calculator
{
    private readonly IHistoryLogger _logger;

    public Calculator(IHistoryLogger logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public decimal Add(decimal a, decimal b)
    {
        var result = a + b;
        _logger.Log($"Add({a}, {b}) = {result}");
        return result;
    }

    public decimal Subtract(decimal a, decimal b)
    {
        var result = a - b;
        _logger.Log($"Subtract({a}, {b}) = {result}");
        return result;
    }

    public decimal Multiply(decimal a, decimal b)
    {
        var result = a * b;
        _logger.Log($"Multiply({a}, {b}) = {result}");
        return result;
    }

    public decimal Divide(decimal a, decimal b)
    {
        if (b == 0)
        {
            _logger.Log($"Divide({a}, {b}) failed: division by zero");
            throw new DivideByZeroException("Cannot divide by zero.");
        }

        var result = a / b;
        _logger.Log($"Divide({a}, {b}) = {result}");
        return result;
    }

    public void ClearHistory()
    {
        _logger.Log("History cleared");
    }
}
