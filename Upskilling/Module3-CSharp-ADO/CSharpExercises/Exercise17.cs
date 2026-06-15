using System;

namespace CSharpExercises
{
    /// <summary>
    /// Exercise 17: Use Null-Conditional Chaining in a Contact App
    /// </summary>
    public static class Exercise17
    {
        public class Contact
        {
            public string? Name { get; set; }
            public string? PhoneNumber { get; set; }
        }

        private static void DisplayContactName(Contact? contact)
        {
            // Null-conditional chaining: if 'contact' is null OR 'contact.Name' is null,
            // the whole expression short-circuits to null.
            string? name = contact?.Name;

            if (name is not null)
            {
                Console.WriteLine($"Contact name: {name}");
            }
            else
            {
                Console.WriteLine("Contact name is not available.");
            }
        }

        public static void Run()
        {
            Contact fullContact = new Contact { Name = "Charlie Lee", PhoneNumber = "555-1234" };
            Contact noNameContact = new Contact { Name = null, PhoneNumber = "555-5678" };
            Contact? nullContact = null;

            DisplayContactName(fullContact);
            DisplayContactName(noNameContact);
            DisplayContactName(nullContact);

            // Chaining further: contact?.PhoneNumber?.Length
            int? phoneLength = fullContact.PhoneNumber?.Length;
            Console.WriteLine($"\nPhone number length: {phoneLength ?? 0}");
        }
    }
}
