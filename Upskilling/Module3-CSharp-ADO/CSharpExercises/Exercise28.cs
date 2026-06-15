using System;
using System.Diagnostics;
using System.IO;

namespace CSharpExercises
{
    /// <summary>
    /// Exercise 28: Log with System.Diagnostics.Trace
    /// </summary>
    public static class Logger
    {
        private static TextWriterTraceListener? fileListener;

        public static void Initialize(string logFilePath)
        {
            // Console listener (writes to console)
            Trace.Listeners.Add(new ConsoleTraceListener());

            // File listener (writes to a log file)
            fileListener = new TextWriterTraceListener(logFilePath);
            Trace.Listeners.Add(fileListener);

            Trace.AutoFlush = true;
        }

        public static void Log(string message)
        {
            Trace.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}");
        }

        public static void Shutdown()
        {
            fileListener?.Flush();
            fileListener?.Close();
        }
    }

    public static class Exercise28
    {
        public static void Run()
        {
            string logFilePath = Path.Combine(Path.GetTempPath(), "app.log");

            Logger.Initialize(logFilePath);

            Logger.Log("Application started.");
            Logger.Log("User 'Alice Johnson' registered for 'Tech Innovators Meetup'.");
            Logger.Log("Application finished.");

            Logger.Shutdown();

            Console.WriteLine($"\nLog file written to: {logFilePath}");
        }
    }
}
