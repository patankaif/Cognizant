using System;

namespace CSharpExercises
{
    /// <summary>
    /// Exercise 7: Implement Method Overloading
    /// </summary>
    public static class Exercise07
    {
        private static int CalculateTotal(int a, int b)
        {
            return a + b;
        }

        private static double CalculateTotal(double a, double b, double c)
        {
            return a + b + c;
        }

        private static int CalculateTotal(params int[] values)
        {
            int total = 0;
            foreach (var v in values) total += v;
            return total;
        }

        private static string CalculateTotal(string itemName, int quantity, double unitPrice)
        {
            double total = quantity * unitPrice;
            return $"{quantity} x {itemName} @ {unitPrice:C} = {total:C}";
        }

        public static void Run()
        {
            Console.WriteLine($"CalculateTotal(2, 3)             = {CalculateTotal(2, 3)}");
            Console.WriteLine($"CalculateTotal(1.5, 2.5, 3.0)     = {CalculateTotal(1.5, 2.5, 3.0)}");
            Console.WriteLine($"CalculateTotal(1, 2, 3, 4, 5)     = {CalculateTotal(1, 2, 3, 4, 5)}");
            Console.WriteLine($"CalculateTotal(\"Ticket\", 3, 10.0) = {CalculateTotal("Ticket", 3, 10.0)}");
        }
    }
}
