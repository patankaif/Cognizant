using System;

namespace CSharpExercises
{
    /// <summary>
    /// Exercise 18: Use the required Modifier in C# 12
    /// </summary>
    public static class Exercise18
    {
        public class Student
        {
            public required string Name { get; set; }
            public required string Email { get; set; }
            public int Age { get; set; } // not required
        }

        public static void Run()
        {
            // Valid - all required properties are set
            var student = new Student { Name = "Ethan Hunt", Email = "ethan@example.com", Age = 21 };
            Console.WriteLine($"Created student: {student.Name}, {student.Email}, Age {student.Age}");

            Console.WriteLine("\nThe following would NOT compile, because 'Email' (a required " +
                               "property) is missing from the object initializer:");
            Console.WriteLine("    var invalid = new Student { Name = \"Diana King\" };");
            Console.WriteLine("\nCompiler error (CS9035): " +
                               "Required member 'Student.Email' must be set in the object initializer " +
                               "or attribute. This enforces that callers cannot forget mandatory properties.");
        }
    }
}
