using System;

namespace CSharpExercises
{
    /// <summary>
    /// Exercise 6: Loop Through an Array with Different Loop Types
    /// </summary>
    public static class Exercise06
    {
        public static void Run()
        {
            int[] numbers = { 5, 12, 8, 23, 17 };

            // for loop - stop if we encounter a value greater than 20
            Console.WriteLine("for loop:");
            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i] > 20)
                {
                    Console.WriteLine($"  Stopping early, found {numbers[i]} > 20");
                    break;
                }
                Console.WriteLine($"  numbers[{i}] = {numbers[i]}");
            }

            // foreach loop - skip even numbers
            Console.WriteLine("\nforeach loop (skip even numbers):");
            foreach (int n in numbers)
            {
                if (n % 2 == 0)
                {
                    continue;
                }
                Console.WriteLine($"  {n} is odd");
            }

            // while loop
            Console.WriteLine("\nwhile loop:");
            int idx = 0;
            while (idx < numbers.Length)
            {
                Console.WriteLine($"  numbers[{idx}] = {numbers[idx]}");
                idx++;
            }

            // do-while loop
            Console.WriteLine("\ndo-while loop:");
            int j = 0;
            do
            {
                Console.WriteLine($"  numbers[{j}] = {numbers[j]}");
                j++;
            } while (j < numbers.Length);
        }
    }
}
