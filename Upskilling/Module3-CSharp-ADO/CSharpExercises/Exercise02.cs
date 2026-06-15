using System;

namespace CSharpExercises
{
    /// <summary>
    /// Exercise 2: Explore Value vs Reference Types
    /// </summary>
    public static class Exercise02
    {
        private class Counter
        {
            public int Value;
        }

        private static void ModifyValueType(int number)
        {
            number = 999; // only changes the local copy
        }

        private static void ModifyReferenceType(Counter counter)
        {
            counter.Value = 999; // changes the original object
        }

        private static void ModifyStringReference(string text)
        {
            // Strings are immutable; this reassigns the local reference only
            text = "Changed inside method";
        }

        public static void Run()
        {
            // Value types
            int number = 10;
            Console.WriteLine($"Before ModifyValueType: number = {number}");
            ModifyValueType(number);
            Console.WriteLine($"After  ModifyValueType: number = {number}");

            double price = 19.99;
            Console.WriteLine($"\nValue type 'double' price = {price}");

            Console.WriteLine();

            // Reference types - custom class
            var counter = new Counter { Value = 10 };
            Console.WriteLine($"Before ModifyReferenceType: counter.Value = {counter.Value}");
            ModifyReferenceType(counter);
            Console.WriteLine($"After  ModifyReferenceType: counter.Value = {counter.Value}");

            // Reference type - string (immutable, behaves like value for callers)
            string text = "Original";
            Console.WriteLine($"\nBefore ModifyStringReference: text = {text}");
            ModifyStringReference(text);
            Console.WriteLine($"After  ModifyStringReference: text = {text}");

            Console.WriteLine("\nConclusion: Value types are copied when passed to methods, " +
                               "so changes inside the method do not affect the original. " +
                               "Reference types (like custom classes) share the same object, " +
                               "so changes to its members ARE visible to the caller. " +
                               "Strings, although reference types, are immutable, so reassigning " +
                               "them inside a method does not affect the caller's variable.");
        }
    }
}
