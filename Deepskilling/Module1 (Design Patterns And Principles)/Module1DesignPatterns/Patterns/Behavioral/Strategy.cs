namespace Module1DesignPatterns.Patterns.Behavioral.Strategy;


public interface IRouteStrategy
{
    string CalculateRoute(string origin, string destination);
}

public class DrivingStrategy : IRouteStrategy
{
    public string CalculateRoute(string origin, string destination)
        => $"Driving route from {origin} to {destination} via the highway.";
}

public class WalkingStrategy : IRouteStrategy
{
    public string CalculateRoute(string origin, string destination)
        => $"Walking route from {origin} to {destination} through the park.";
}

public class PublicTransitStrategy : IRouteStrategy
{
    public string CalculateRoute(string origin, string destination)
        => $"Transit route from {origin} to {destination} using bus line 42.";
}

public class NavigationContext
{
    private IRouteStrategy _strategy;

    public NavigationContext(IRouteStrategy strategy)
    {
        _strategy = strategy;
    }

    public void SetStrategy(IRouteStrategy strategy) => _strategy = strategy;

    public string BuildRoute(string origin, string destination)
        => _strategy.CalculateRoute(origin, destination);
}

public static class StrategyDemo
{
    public static void Run()
    {
        Console.WriteLine("--- Strategy Pattern ---");

        var navigation = new NavigationContext(new DrivingStrategy());
        Console.WriteLine(navigation.BuildRoute("Home", "Office"));

        navigation.SetStrategy(new WalkingStrategy());
        Console.WriteLine(navigation.BuildRoute("Home", "Office"));

        navigation.SetStrategy(new PublicTransitStrategy());
        Console.WriteLine(navigation.BuildRoute("Home", "Office"));
    }
}
