using System;

namespace CSharpExercises
{
    /// <summary>
    /// Exercise 15: Differentiate Abstract Classes and Interfaces
    /// </summary>
    public static class Exercise15
    {
        // Abstract class - can have implementation, fields, constructors
        public abstract class Vehicle
        {
            public string Name { get; }

            protected Vehicle(string name) => Name = name;

            public abstract void Drive(); // must be implemented by derived classes

            public void Honk() // concrete shared implementation
            {
                Console.WriteLine($"{Name} says: Beep beep!");
            }
        }

        // Interface - only a contract (method signatures)
        public interface IDrivable
        {
            void Start();
        }

        public class Car : Vehicle, IDrivable
        {
            public Car(string name) : base(name) { }

            public void Start()
            {
                Console.WriteLine($"{Name}: Engine started.");
            }

            public override void Drive()
            {
                Console.WriteLine($"{Name}: Driving on the road.");
            }
        }

        public static void Run()
        {
            Car myCar = new Car("Tesla Model 3");

            // Polymorphism: treat Car as a Vehicle
            Vehicle vehicle = myCar;
            vehicle.Drive();   // overridden abstract method
            vehicle.Honk();    // shared concrete method from abstract class

            // Treat Car as an IDrivable
            IDrivable drivable = myCar;
            drivable.Start();  // interface method implementation

            Console.WriteLine("\nKey differences:");
            Console.WriteLine("- Abstract class (Vehicle): can have fields, constructors, and " +
                               "concrete (shared) methods like Honk(), plus abstract methods like Drive() " +
                               "that derived classes MUST implement. A class can inherit only ONE abstract class.");
            Console.WriteLine("- Interface (IDrivable): pure contract (Start()) with no fields or " +
                               "constructors (by default). A class can implement MULTIPLE interfaces.");
        }
    }
}
