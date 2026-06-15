using System;
using System.IO;
using System.Text.Json;

namespace CSharpExercises
{
    /// <summary>
    /// Exercise 24: Serialize and Deserialize JSON Files
    /// </summary>
    public static class Exercise24
    {
        public class User
        {
            public string Name { get; set; } = string.Empty;
            public int Age { get; set; }
            public string Email { get; set; } = string.Empty;
        }

        public static void Run()
        {
            var user = new User { Name = "Alice Johnson", Age = 29, Email = "alice@example.com" };

            string filePath = Path.Combine(Path.GetTempPath(), "user.json");

            // Serialize to JSON string and save to file
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(user, options);
            File.WriteAllText(filePath, json);

            Console.WriteLine($"Serialized JSON written to: {filePath}");
            Console.WriteLine(json);

            // Deserialize back into an object
            string jsonFromFile = File.ReadAllText(filePath);
            User? loadedUser = JsonSerializer.Deserialize<User>(jsonFromFile);

            Console.WriteLine("\nDeserialized object:");
            Console.WriteLine($"  Name:  {loadedUser?.Name}");
            Console.WriteLine($"  Age:   {loadedUser?.Age}");
            Console.WriteLine($"  Email: {loadedUser?.Email}");
        }
    }
}
