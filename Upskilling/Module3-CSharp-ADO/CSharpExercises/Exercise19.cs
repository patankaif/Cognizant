using System;
using System.Collections.Generic;

namespace CSharpExercises
{
    /// <summary>
    /// Exercise 19: Work with Lists and Dictionaries
    /// </summary>
    public static class Exercise19
    {
        public static void Run()
        {
            // List<T>
            List<string> eventNames = new()
            {
                "Tech Innovators Meetup",
                "AI & ML Conference",
                "Frontend Development Bootcamp"
            };

            Console.WriteLine("Events (List<string>):");
            foreach (var name in eventNames)
            {
                Console.WriteLine($"  - {name}");
            }

            eventNames.Add("Community Choir Meetup");
            eventNames.Remove("AI & ML Conference");

            Console.WriteLine("\nAfter Add('Community Choir Meetup') and Remove('AI & ML Conference'):");
            foreach (var name in eventNames)
            {
                Console.WriteLine($"  - {name}");
            }

            // Dictionary<K, V>
            Dictionary<int, string> eventsById = new()
            {
                { 1, "Tech Innovators Meetup" },
                { 2, "AI & ML Conference" },
                { 3, "Frontend Development Bootcamp" }
            };

            Console.WriteLine("\nEvents (Dictionary<int, string>):");
            foreach (var kvp in eventsById)
            {
                Console.WriteLine($"  ID {kvp.Key}: {kvp.Value}");
            }

            eventsById.Add(4, "Community Choir Meetup");
            eventsById.Remove(2);

            Console.WriteLine("\nAfter Add(4, ...) and Remove(2):");
            foreach (var kvp in eventsById)
            {
                Console.WriteLine($"  ID {kvp.Key}: {kvp.Value}");
            }
        }
    }
}
