using System;

namespace CSharpExercises
{
    /// <summary>
    /// Exercise 21: Use Pattern Matching with is and switch
    /// </summary>
    public static class Exercise21
    {
        private static void DescribeWithIs(object obj)
        {
            if (obj is int number && number > 100)
            {
                Console.WriteLine($"  [is] Large integer: {number}");
            }
            else if (obj is int smallNumber)
            {
                Console.WriteLine($"  [is] Integer: {smallNumber}");
            }
            else if (obj is string text)
            {
                Console.WriteLine($"  [is] String of length {text.Length}: \"{text}\"");
            }
            else if (obj is double d)
            {
                Console.WriteLine($"  [is] Double: {d}");
            }
            else
            {
                Console.WriteLine($"  [is] Unknown type: {obj.GetType().Name}");
            }
        }

        // Enhanced switch statement / expression with pattern matching
        private static string DescribeWithSwitch(object obj) => obj switch
        {
            int n when n > 100 => $"Large integer: {n}",
            int n              => $"Integer: {n}",
            string s           => $"String of length {s.Length}: \"{s}\"",
            double d           => $"Double: {d}",
            null               => "null value",
            _                  => $"Unknown type: {obj.GetType().Name}"
        };

        public static void Run()
        {
            object[] items = { 42, 250, "Hello", 3.14, true };

            Console.WriteLine("Using 'is' pattern matching:");
            foreach (var item in items)
            {
                DescribeWithIs(item);
            }

            Console.WriteLine("\nUsing enhanced 'switch' pattern matching:");
            foreach (var item in items)
            {
                Console.WriteLine($"  [switch] {DescribeWithSwitch(item)}");
            }
        }
    }
}
