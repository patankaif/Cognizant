using System;

namespace CSharpExercises
{
    /// <summary>
    /// Exercise 10: Demonstrate OOP Basics with Constructors
    /// </summary>
    public static class Exercise10
    {
        public class Car
        {
            public string Make { get; set; }
            public string Model { get; set; }
            public int Year { get; set; }

            // Default constructor
            public Car()
            {
                Make = "Unknown";
                Model = "Unknown";
                Year = DateTime.Now.Year;
            }

            // Parameterized constructor
            public Car(string make, string model, int year)
            {
                Make = make;
                Model = model;
                Year = year;
            }

            public override string ToString() => $"{Year} {Make} {Model}";
        }

        public static void Run()
        {
            var car1 = new Car(); // default constructor
            var car2 = new Car("Toyota", "Corolla", 2024); // parameterized constructor

            Console.WriteLine($"car1 (default constructor):       {car1}");
            Console.WriteLine($"car2 (parameterized constructor):  {car2}");
        }
    }
}
