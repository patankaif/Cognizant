using System;
using System.Threading.Tasks;

namespace CSharpExercises
{
    /// <summary>
    /// Exercise 23: Simulate Async File Upload with Exception Handling
    /// </summary>
    public static class Exercise23
    {
        private static async Task<string> UploadFileAsync(string fileName, bool simulateFailure = false)
        {
            Console.WriteLine($"Uploading '{fileName}'...");
            await Task.Delay(3000); // simulate a 3-second upload

            if (simulateFailure)
            {
                throw new InvalidOperationException($"Upload of '{fileName}' failed: network error.");
            }

            return $"'{fileName}' uploaded successfully.";
        }

        public static async Task RunAsync()
        {
            // Successful upload
            try
            {
                string result = await UploadFileAsync("agenda.pdf");
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            // Failed upload
            try
            {
                string result = await UploadFileAsync("poster.jpg", simulateFailure: true);
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
