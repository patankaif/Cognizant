using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpExercises
{
    /// <summary>
    /// Exercise 20: Use LINQ for Filtering and Projection
    /// </summary>
    public static class Exercise20
    {
        public class Order
        {
            public int OrderId { get; set; }
            public string CustomerName { get; set; } = string.Empty;
            public decimal TotalAmount { get; set; }
        }

        public static void Run()
        {
            List<Order> orders = new()
            {
                new Order { OrderId = 1, CustomerName = "Alice Johnson", TotalAmount = 25.00m },
                new Order { OrderId = 2, CustomerName = "Bob Smith",     TotalAmount = 10.00m },
                new Order { OrderId = 3, CustomerName = "Charlie Lee",   TotalAmount = 50.00m },
                new Order { OrderId = 4, CustomerName = "Diana King",    TotalAmount = 5.00m },
            };

            // Filter orders with TotalAmount > 15, project into anonymous type
            var highValueOrders = orders
                .Where(o => o.TotalAmount > 15)
                .Select(o => new { o.OrderId, o.CustomerName, Amount = o.TotalAmount });

            Console.WriteLine("Orders with TotalAmount > 15:");
            foreach (var order in highValueOrders)
            {
                Console.WriteLine($"  Order #{order.OrderId} - {order.CustomerName}: {order.Amount:C}");
            }

            // Additional LINQ examples
            decimal total = orders.Sum(o => o.TotalAmount);
            Console.WriteLine($"\nTotal of all orders: {total:C}");

            var sortedNames = orders.OrderBy(o => o.CustomerName).Select(o => o.CustomerName);
            Console.WriteLine($"Customers (sorted): {string.Join(", ", sortedNames)}");
        }
    }
}
