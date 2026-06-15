using System;

namespace CSharpExercises
{
    /// <summary>
    /// Exercise 13: Create and Use Records with init Properties
    /// </summary>
    public static class Exercise13
    {
        public record Employee
        {
            public required string Name { get; init; }
            public required string Department { get; init; }
            public decimal Salary { get; init; }
        }

        public static void Run()
        {
            var original = new Employee { Name = "Diana King", Department = "Engineering", Salary = 85000m };
            Console.WriteLine($"Original: {original}");

            // 'with' expression creates a modified copy; original remains unchanged
            var promoted = original with { Department = "Engineering Lead", Salary = 95000m };
            Console.WriteLine($"Promoted (copy): {promoted}");

            Console.WriteLine($"\nOriginal unchanged: {original}");

            // original.Salary = 100000; // compile error - init-only property cannot be set after construction
            Console.WriteLine("\nAttempting 'original.Salary = 100000;' after construction " +
                               "would be a COMPILE-TIME ERROR because Salary uses 'init'.");
        }
    }
}
