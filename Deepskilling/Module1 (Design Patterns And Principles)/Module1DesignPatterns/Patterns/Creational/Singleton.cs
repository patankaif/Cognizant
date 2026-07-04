namespace Module1DesignPatterns.Patterns.Creational.Singleton;


public sealed class AppConfiguration
{
    private static readonly Lazy<AppConfiguration> _instance =
        new(() => new AppConfiguration());

    public static AppConfiguration Instance => _instance.Value;

    public string Environment { get; private set; } = "Production";
    public int MaxRetryCount { get; private set; } = 3;

    private AppConfiguration()
    {
        Console.WriteLine("[Singleton] AppConfiguration instance created (only happens once).");
    }

    public void UpdateEnvironment(string environment) => Environment = environment;
}

public static class SingletonDemo
{
    public static void Run()
    {
        Console.WriteLine("--- Singleton Pattern ---");

        var config1 = AppConfiguration.Instance;
        Console.WriteLine($"config1.Environment = {config1.Environment}");

        config1.UpdateEnvironment("Staging");

        var config2 = AppConfiguration.Instance; 
        Console.WriteLine($"config2.Environment = {config2.Environment}");

        Console.WriteLine($"ReferenceEquals(config1, config2) = {ReferenceEquals(config1, config2)}");
    }
}
