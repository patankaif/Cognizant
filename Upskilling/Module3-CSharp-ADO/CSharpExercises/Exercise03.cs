using System;

namespace CSharpExercises
{
    /// <summary>
    /// Exercise 3: Use Primary Constructors in C# 12
    /// </summary>
    public static class Exercise03
    {
        // Primary constructor syntax (C# 12)
        public class Person(string firstName, string lastName, int age)
        {
            public string FirstName { get; } = firstName;
            public string LastName { get; } = lastName;
            public int Age { get; } = age;

            public void DisplayFullInfo()
            {
                Console.WriteLine($"Full Name: {FirstName} {LastName}");
                Console.WriteLine($"Age: {Age}");
            }
        }

        public static void Run()
        {
            var person = new Person("Alice", "Johnson", 30);

            Console.WriteLine($"Name: {person.FirstName} {person.LastName}, Age: {person.Age}");
            Console.WriteLine();
            person.DisplayFullInfo();
        }
    }
}
