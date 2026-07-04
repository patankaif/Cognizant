namespace Module1DesignPatterns.Patterns.Behavioral.Observer;

public interface IObserver
{
    void Update(string stockSymbol, decimal price);
}

public interface ISubject
{
    void Subscribe(IObserver observer);
    void Unsubscribe(IObserver observer);
    void NotifyObservers();
}

public class StockTicker : ISubject
{
    private readonly List<IObserver> _observers = new();
    private string _symbol = string.Empty;
    private decimal _price;

    public void Subscribe(IObserver observer) => _observers.Add(observer);
    public void Unsubscribe(IObserver observer) => _observers.Remove(observer);

    public void NotifyObservers()
    {
        foreach (var observer in _observers)
            observer.Update(_symbol, _price);
    }

    public void SetPrice(string symbol, decimal price)
    {
        _symbol = symbol;
        _price = price;
        Console.WriteLine($"[StockTicker] {symbol} price updated to {price:C}");
        NotifyObservers();
    }
}

public class PriceDisplay : IObserver
{
    private readonly string _name;

    public PriceDisplay(string name)
    {
        _name = name;
    }

    public void Update(string stockSymbol, decimal price)
        => Console.WriteLine($"  [{_name}] sees {stockSymbol} = {price:C}");
}

public class PriceAlertService : IObserver
{
    private readonly decimal _threshold;

    public PriceAlertService(decimal threshold)
    {
        _threshold = threshold;
    }

    public void Update(string stockSymbol, decimal price)
    {
        if (price >= _threshold)
            Console.WriteLine($"  [ALERT] {stockSymbol} crossed threshold of {_threshold:C}! (now {price:C})");
    }
}

public static class ObserverDemo
{
    public static void Run()
    {
        Console.WriteLine("--- Observer Pattern ---");

        var ticker = new StockTicker();

        var mobileDisplay = new PriceDisplay("Mobile App");
        var webDisplay = new PriceDisplay("Web Dashboard");
        var alertService = new PriceAlertService(150m);

        ticker.Subscribe(mobileDisplay);
        ticker.Subscribe(webDisplay);
        ticker.Subscribe(alertService);

        ticker.SetPrice("MSFT", 140m);
        ticker.SetPrice("MSFT", 155m); 

        ticker.Unsubscribe(webDisplay);
        Console.WriteLine("(Web Dashboard unsubscribed)");
        ticker.SetPrice("MSFT", 160m);
    }
}
