namespace Module1DesignPatterns.Patterns.Structural.Proxy;


public interface IImageService
{
    string LoadImage(string fileName);
}

public class RealImageService : IImageService
{
    public string LoadImage(string fileName)
    {
        Console.WriteLine($"[RealImageService] Loading '{fileName}' from disk (expensive operation)...");
        return $"<bytes of {fileName}>";
    }
}

public class CachingImageServiceProxy : IImageService
{
    private readonly IImageService _realService;
    private readonly Dictionary<string, string> _cache = new();

    public CachingImageServiceProxy(IImageService realService)
    {
        _realService = realService;
    }

    public string LoadImage(string fileName)
    {
        if (_cache.TryGetValue(fileName, out var cached))
        {
            Console.WriteLine($"[Proxy] Returning cached '{fileName}'.");
            return cached;
        }

        var result = _realService.LoadImage(fileName);
        _cache[fileName] = result;
        return result;
    }
}

public static class ProxyDemo
{
    public static void Run()
    {
        Console.WriteLine("--- Proxy Pattern ---");

        IImageService imageService = new CachingImageServiceProxy(new RealImageService());

        imageService.LoadImage("photo1.png"); 
        imageService.LoadImage("photo1.png"); 
        imageService.LoadImage("photo2.png"); 
    }
}
