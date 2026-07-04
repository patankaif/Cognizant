namespace Module1DesignPatterns.Patterns.Structural.Adapter;

public interface IPaymentProcessor
{
    void ProcessPayment(decimal amount, string currency);
}

public class StandardPaymentProcessor : IPaymentProcessor
{
    public void ProcessPayment(decimal amount, string currency)
        => Console.WriteLine($"[Standard] Processed {amount} {currency}.");
}

public class LegacyGatewayApi
{
    public void MakeTransaction(long amountInCents, string currencyCode)
        => Console.WriteLine($"[Legacy Gateway] Transaction of {amountInCents} cents ({currencyCode}) submitted.");
}

public class LegacyGatewayAdapter : IPaymentProcessor
{
    private readonly LegacyGatewayApi _legacyApi;

    public LegacyGatewayAdapter(LegacyGatewayApi legacyApi)
    {
        _legacyApi = legacyApi;
    }

    public void ProcessPayment(decimal amount, string currency)
    {
        long amountInCents = (long)(amount * 100);
        _legacyApi.MakeTransaction(amountInCents, currency);
    }
}

public static class AdapterDemo
{
    public static void Run()
    {
        Console.WriteLine("--- Adapter Pattern ---");

        List<IPaymentProcessor> processors = new()
        {
            new StandardPaymentProcessor(),
            new LegacyGatewayAdapter(new LegacyGatewayApi())
        };

        foreach (var processor in processors)
        {
            processor.ProcessPayment(19.99m, "USD");
        }
    }
}
