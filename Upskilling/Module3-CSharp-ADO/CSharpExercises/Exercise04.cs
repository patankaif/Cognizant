using System;
using System.Collections.Generic;

namespace CSharpExercises
{
    /// <summary>
    /// Exercise 4: Demonstrate Type Inference with var and new()
    /// </summary>
    public static class Exercise04
    {
        private class Product
        {
            public string Name { get; set; } = string.Empty;
            public decimal Price { get; set; }
        }

        public static void Run()
        {
            var number = 42;                 // inferred as int
            var name = "Community Portal";   // inferred as string
            var price = 19.99;                // inferred as double

            // target-typed new() (C# 9+)
            Product product = new() { Name = "Event Ticket", Price = 25.00m };
            List<int> ids = new() { 1, 2, 3 };

            Console.WriteLine($"number  -> type: {number.GetType().Name}, value: {number}");
            Console.WriteLine($"name    -> type: {name.GetType().Name}, value: {name}");
            Console.WriteLine($"price   -> type: {price.GetType().Name}, value: {price}");
            Console.WriteLine($"product -> type: {product.GetType().Name}, value: {product.Name} (${product.Price})");
            Console.WriteLine($"ids     -> type: {ids.GetType().Name}, value: [{string.Join(", ", ids)}]");

            Console.WriteLine("\nDiscussion:");
            Console.WriteLine("- 'var' is beneficial when the type is obvious from the right-hand " +
                               "side (e.g. 'var name = \"text\";') or when the type name is long/generic.");
            Console.WriteLine("- It can hurt readability when the right-hand side doesn't make the " +
                               "type obvious (e.g. 'var result = GetData();'), so an explicit type " +
                               "or a descriptive variable name is preferred in those cases.");
            Console.WriteLine("- 'new()' (target-typed new) reduces repetition when the declared " +
                               "type is already known on the left-hand side.");
        }
    }
}
