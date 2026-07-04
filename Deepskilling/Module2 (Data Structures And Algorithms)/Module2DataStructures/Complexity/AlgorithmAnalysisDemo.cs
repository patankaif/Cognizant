using System.Diagnostics;

namespace Module2DataStructures.Complexity;

public static class AlgorithmAnalysisDemo
{
    public static long FibonacciNaiveRecursive(int n)
    {
        if (n <= 1) return n;
        return FibonacciNaiveRecursive(n - 1) + FibonacciNaiveRecursive(n - 2);
    }

    public static long FibonacciMemoized(int n, Dictionary<int, long>? cache = null)
    {
        cache ??= new Dictionary<int, long>();
        if (n <= 1) return n;
        if (cache.TryGetValue(n, out var cached)) return cached;

        var result = FibonacciMemoized(n - 1, cache) + FibonacciMemoized(n - 2, cache);
        cache[n] = result;
        return result;
    }

    public static long FibonacciIterative(int n)
    {
        if (n <= 1) return n;
        long previous = 0, current = 1;
        for (int i = 2; i <= n; i++)
        {
            long next = previous + current;
            previous = current;
            current = next;
        }
        return current;
    }

    public static void Run()
    {
        Console.WriteLine("--- Analysis of Algorithms: three Fibonacci implementations ---");
        Console.WriteLine("(Same correct answer, very different time complexity)");
        Console.WriteLine();

        foreach (var n in new[] { 10, 20, 30 })
        {
            var sw = Stopwatch.StartNew();
            var naive = FibonacciNaiveRecursive(n);
            sw.Stop();
            var naiveMs = sw.Elapsed.TotalMilliseconds;

            sw.Restart();
            var memoized = FibonacciMemoized(n);
            sw.Stop();
            var memoMs = sw.Elapsed.TotalMilliseconds;

            sw.Restart();
            var iterative = FibonacciIterative(n);
            sw.Stop();
            var iterMs = sw.Elapsed.TotalMilliseconds;

            Console.WriteLine($"n={n,2}: naive O(2^n)={naive,7} ({naiveMs:F4} ms) | " +
                               $"memoized O(n)={memoized,7} ({memoMs:F4} ms) | " +
                               $"iterative O(n)={iterative,7} ({iterMs:F4} ms)");
        }

        Console.WriteLine();
        Console.WriteLine("Notice how the naive version's time grows dramatically as n increases,");
        Console.WriteLine("while the memoized and iterative versions barely change -- that's the");
        Console.WriteLine("practical difference between O(2^n) and O(n).");
    }
}
