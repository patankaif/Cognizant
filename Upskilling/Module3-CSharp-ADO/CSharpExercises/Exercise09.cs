using System;

namespace CSharpExercises
{
    /// <summary>
    /// Exercise 9: Use Local Functions
    /// </summary>
    public static class Exercise09
    {
        private static long CalculateFactorial(int n)
        {
            if (n < 0)
                throw new ArgumentException("n must be non-negative", nameof(n));

            // Local function - only visible within CalculateFactorial
            long Factorial(int num)
            {
                if (num <= 1) return 1;
                return num * Factorial(num - 1);
            }

            return Factorial(n);
        }

        public static void Run()
        {
            foreach (int n in new[] { 0, 1, 5, 10 })
            {
                Console.WriteLine($"Factorial({n}) = {CalculateFactorial(n)}");
            }
        }
    }
}
