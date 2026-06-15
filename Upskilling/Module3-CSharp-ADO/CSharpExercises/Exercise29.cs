using System;
using System.Net;

namespace CSharpExercises
{
    /// <summary>
    /// Exercise 29: Sanitize Input and Prevent XSS
    /// </summary>
    public static class Exercise29
    {
        // Simulates rendering user input back to an HTML page safely
        private static string SanitizeForHtml(string userInput)
        {
            // HTML-encode special characters so <, >, ", ', & become entities
            return WebUtility.HtmlEncode(userInput);
        }

        public static void Run()
        {
            string[] userInputs =
            {
                "Great event!",
                "<script>alert('XSS Attack!')</script>",
                "Nice job <b>team</b> & thanks!",
                "\"Quoted\" feedback with 'apostrophes'"
            };

            Console.WriteLine("Simulating a feedback form that displays user input:\n");

            foreach (var input in userInputs)
            {
                string safeOutput = SanitizeForHtml(input);
                Console.WriteLine($"Raw input:      {input}");
                Console.WriteLine($"Sanitized HTML: {safeOutput}");
                Console.WriteLine();
            }

            Console.WriteLine("By HTML-encoding user input before rendering it, characters like " +
                               "<, >, \", ', and & are converted to HTML entities (e.g. &lt;script&gt;), " +
                               "so browsers display them as text instead of executing them as code.");
        }
    }
}
