using System;

namespace CSharpExercises
{
    /// <summary>
    /// Exercise 22: Create and Deconstruct Tuples
    /// </summary>
    public static class Exercise22
    {
        // Method returning a tuple with named elements
        private static (int Count, string Message) GetRegistrationSummary()
        {
            int count = 42;
            string message = "Registrations are open";
            return (count, message);
        }

        public static void Run()
        {
            // Deconstruct the tuple
            (int count, string message) = GetRegistrationSummary();

            Console.WriteLine($"Count: {count}");
            Console.WriteLine($"Message: {message}");

            // Accessing without deconstruction
            var result = GetRegistrationSummary();
            Console.WriteLine($"\nVia named fields -> result.Count = {result.Count}, result.Message = {result.Message}");

            // Discard one of the values during deconstruction
            (_, string onlyMessage) = GetRegistrationSummary();
            Console.WriteLine($"\nUsing discard '_' -> onlyMessage = {onlyMessage}");
        }
    }
}
