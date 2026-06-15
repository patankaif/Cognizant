using System;

namespace CSharpExercises
{
    /// <summary>
    /// Exercise 16: Handle Null References Safely
    /// </summary>
    public static class Exercise16
    {
        public class Person
        {
            public string Name { get; set; } = string.Empty;
            public Address? HomeAddress { get; set; } // nullable reference type
        }

        public class Address
        {
            public string? City { get; set; } // nullable reference type
        }

        public static void Run()
        {
            Person personWithAddress = new Person
            {
                Name = "Alice",
                HomeAddress = new Address { City = "New York" }
            };

            Person personWithoutAddress = new Person
            {
                Name = "Bob",
                HomeAddress = null
            };

            foreach (var person in new[] { personWithAddress, personWithoutAddress })
            {
                // Null-conditional operator (?.) and null-coalescing operator (??)
                string city = person.HomeAddress?.City ?? "Unknown city";
                Console.WriteLine($"{person.Name} lives in: {city}");
            }

            // Explicit null checking
            Person? maybeNull = null;
            if (maybeNull is null)
            {
                Console.WriteLine("\n'maybeNull' is null - safely handled with an explicit check.");
            }

            // Null-coalescing assignment (??=)
            string? nickname = null;
            nickname ??= "Guest";
            Console.WriteLine($"Nickname (after ??=): {nickname}");
        }
    }
}
