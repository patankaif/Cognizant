using System;

namespace CSharpExercises
{
    /// <summary>
    /// Exercise 12: Use Auto-Properties and Backing Fields
    /// </summary>
    public static class Exercise12
    {
        public class Product
        {
            // Auto-implemented property
            public string Name { get; set; } = string.Empty;

            // Property with explicit backing field and validation
            private decimal _price;
            public decimal Price
            {
                get => _price;
                set
                {
                    if (value < 0)
                        throw new ArgumentOutOfRangeException(nameof(value), "Price cannot be negative.");
                    _price = value;
                }
            }
        }

        public static void Run()
        {
            var product = new Product { Name = "Event Ticket", Price = 49.99m };
            Console.WriteLine($"Product: {product.Name}, Price: {product.Price:C}");

            Console.WriteLine("\nAttempting to set a negative price...");
            try
            {
                product.Price = -10;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"Validation triggered: {ex.Message}");
            }

            Console.WriteLine($"\nFinal Product: {product.Name}, Price: {product.Price:C} (unchanged)");
        }
    }
}
