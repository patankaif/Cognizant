namespace Module1DesignPatterns.Patterns.Structural.Decorator;


public interface ICoffee
{
    string Description { get; }
    decimal Cost { get; }
}

public class PlainCoffee : ICoffee
{
    public string Description => "Coffee";
    public decimal Cost => 2.00m;
}

public abstract class CoffeeDecorator : ICoffee
{
    protected readonly ICoffee Coffee;

    protected CoffeeDecorator(ICoffee coffee)
    {
        Coffee = coffee;
    }

    public virtual string Description => Coffee.Description;
    public virtual decimal Cost => Coffee.Cost;
}

public class MilkDecorator : CoffeeDecorator
{
    public MilkDecorator(ICoffee coffee) : base(coffee) { }

    public override string Description => $"{Coffee.Description} + Milk";
    public override decimal Cost => Coffee.Cost + 0.50m;
}

public class SugarDecorator : CoffeeDecorator
{
    public SugarDecorator(ICoffee coffee) : base(coffee) { }

    public override string Description => $"{Coffee.Description} + Sugar";
    public override decimal Cost => Coffee.Cost + 0.25m;
}

public class WhippedCreamDecorator : CoffeeDecorator
{
    public WhippedCreamDecorator(ICoffee coffee) : base(coffee) { }

    public override string Description => $"{Coffee.Description} + Whipped Cream";
    public override decimal Cost => Coffee.Cost + 0.75m;
}

public static class DecoratorDemo
{
    public static void Run()
    {
        Console.WriteLine("--- Decorator Pattern ---");

        ICoffee coffee = new PlainCoffee();
        Console.WriteLine($"{coffee.Description}: {coffee.Cost:C}");

        ICoffee fancyCoffee = new WhippedCreamDecorator(
                                  new MilkDecorator(
                                      new SugarDecorator(
                                          new PlainCoffee())));

        Console.WriteLine($"{fancyCoffee.Description}: {fancyCoffee.Cost:C}");
    }
}
