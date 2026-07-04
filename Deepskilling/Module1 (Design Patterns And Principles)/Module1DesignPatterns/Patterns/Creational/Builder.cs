namespace Module1DesignPatterns.Patterns.Creational.Builder;


public class Computer
{
    public string Cpu { get; set; } = string.Empty;
    public int RamGb { get; set; }
    public int StorageGb { get; set; }
    public bool HasGraphicsCard { get; set; }
    public bool HasWifi { get; set; }

    public override string ToString()
        => $"Computer[CPU={Cpu}, RAM={RamGb}GB, Storage={StorageGb}GB, " +
           $"GPU={HasGraphicsCard}, WiFi={HasWifi}]";
}

public interface IComputerBuilder
{
    IComputerBuilder SetCpu(string cpu);
    IComputerBuilder SetRam(int ramGb);
    IComputerBuilder SetStorage(int storageGb);
    IComputerBuilder AddGraphicsCard();
    IComputerBuilder AddWifi();
    Computer Build();
}

public class ComputerBuilder : IComputerBuilder
{
    private readonly Computer _computer = new();

    public IComputerBuilder SetCpu(string cpu)
    {
        _computer.Cpu = cpu;
        return this;
    }

    public IComputerBuilder SetRam(int ramGb)
    {
        _computer.RamGb = ramGb;
        return this;
    }

    public IComputerBuilder SetStorage(int storageGb)
    {
        _computer.StorageGb = storageGb;
        return this;
    }

    public IComputerBuilder AddGraphicsCard()
    {
        _computer.HasGraphicsCard = true;
        return this;
    }

    public IComputerBuilder AddWifi()
    {
        _computer.HasWifi = true;
        return this;
    }

    public Computer Build() => _computer;
}

public class ComputerDirector
{
    public Computer BuildGamingPc(IComputerBuilder builder) =>
        builder.SetCpu("Intel i9")
               .SetRam(32)
               .SetStorage(2000)
               .AddGraphicsCard()
               .AddWifi()
               .Build();

    public Computer BuildOfficePc(IComputerBuilder builder) =>
        builder.SetCpu("Intel i5")
               .SetRam(16)
               .SetStorage(512)
               .AddWifi()
               .Build();
}

public static class BuilderDemo
{
    public static void Run()
    {
        Console.WriteLine("--- Builder Pattern ---");

        var customPc = new ComputerBuilder()
            .SetCpu("AMD Ryzen 7")
            .SetRam(64)
            .SetStorage(1000)
            .AddGraphicsCard()
            .Build();
        Console.WriteLine($"Custom PC: {customPc}");

        var director = new ComputerDirector();
        var gamingPc = director.BuildGamingPc(new ComputerBuilder());
        var officePc = director.BuildOfficePc(new ComputerBuilder());

        Console.WriteLine($"Gaming PC: {gamingPc}");
        Console.WriteLine($"Office PC: {officePc}");
    }
}
