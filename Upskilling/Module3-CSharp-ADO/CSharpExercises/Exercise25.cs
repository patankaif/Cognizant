using System;
using System.IO;
using System.Text;

namespace CSharpExercises
{
    /// <summary>
    /// Exercise 25: Use FileStream and MemoryStream
    /// </summary>
    public static class Exercise25
    {
        public static void Run()
        {
            string filePath = Path.Combine(Path.GetTempPath(), "sample.txt");
            string content = "Hello from FileStream! This is a sample community event note.";

            // Write content to file first using FileStream
            using (FileStream writeStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                byte[] bytes = Encoding.UTF8.GetBytes(content);
                writeStream.Write(bytes, 0, bytes.Length);
            }

            // Read text from the file using FileStream
            Console.WriteLine("Reading from FileStream:");
            using (FileStream readStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                byte[] buffer = new byte[readStream.Length];
                readStream.Read(buffer, 0, buffer.Length);
                string fileContent = Encoding.UTF8.GetString(buffer);
                Console.WriteLine($"  {fileContent}");
            }

            // Write data to MemoryStream and display number of bytes written
            Console.WriteLine("\nWriting to MemoryStream:");
            using (MemoryStream memoryStream = new MemoryStream())
            {
                byte[] data = Encoding.UTF8.GetBytes("In-memory event data");
                memoryStream.Write(data, 0, data.Length);
                Console.WriteLine($"  Bytes written to MemoryStream: {memoryStream.Length}");

                memoryStream.Position = 0;
                byte[] readBuffer = new byte[memoryStream.Length];
                memoryStream.Read(readBuffer, 0, readBuffer.Length);
                Console.WriteLine($"  MemoryStream content: {Encoding.UTF8.GetString(readBuffer)}");
            }
        }
    }
}
