using System;

namespace CSharpExercises
{
    /// <summary>
    /// Exercise 8: Use ref, out, and in Parameters
    /// </summary>
    public static class Exercise08
    {
        // ref: value must be initialized before call; method can modify it
        private static void DoubleValue(ref int number)
        {
            number *= 2;
        }

        // out: value need not be initialized before call; method must assign it
        private static void SplitFullName(string fullName, out string firstName, out string lastName)
        {
            var parts = fullName.Split(' ', 2);
            firstName = parts[0];
            lastName = parts.Length > 1 ? parts[1] : string.Empty;
        }

        // in: passed by reference but read-only inside the method (for large structs, avoids copying)
        private static double CalculateDiscount(in decimal price, double discountPercent)
        {
            // price cannot be modified here (compile error if attempted)
            return (double)price * (1 - discountPercent / 100);
        }

        public static void Run()
        {
            // ref
            int value = 10;
            Console.WriteLine($"Before DoubleValue: value = {value}");
            DoubleValue(ref value);
            Console.WriteLine($"After  DoubleValue: value = {value}");

            // out
            Console.WriteLine();
            SplitFullName("Alice Johnson", out string first, out string last);
            Console.WriteLine($"SplitFullName -> first = '{first}', last = '{last}'");

            // in
            Console.WriteLine();
            decimal ticketPrice = 100m;
            double finalPrice = CalculateDiscount(in ticketPrice, 15);
            Console.WriteLine($"Before/After CalculateDiscount: ticketPrice = {ticketPrice} (unchanged)");
            Console.WriteLine($"Discounted price (15% off): {finalPrice:C}");
        }
    }
}
