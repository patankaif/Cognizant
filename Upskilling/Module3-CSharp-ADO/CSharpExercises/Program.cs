using System;

namespace CSharpExercises
{
    /// <summary>
    /// Entry point. Run with: dotnet run -- &lt;exercise number&gt;
    /// Example: dotnet run -- 5      (runs Exercise 5)
    ///          dotnet run -- all    (runs all exercises sequentially, skipping #1 & #30)
    /// </summary>
    public static class Program
    {
        public static async System.Threading.Tasks.Task Main(string[] args)
        {
            string choice = args.Length > 0 ? args[0] : "menu";

            switch (choice)
            {
                case "1": Exercise01.Run(); break;
                case "2": Exercise02.Run(); break;
                case "3": Exercise03.Run(); break;
                case "4": Exercise04.Run(); break;
                case "5": Exercise05.Run(); break;
                case "6": Exercise06.Run(); break;
                case "7": Exercise07.Run(); break;
                case "8": Exercise08.Run(); break;
                case "9": Exercise09.Run(); break;
                case "10": Exercise10.Run(); break;
                case "11": Exercise11.Run(); break;
                case "12": Exercise12.Run(); break;
                case "13": Exercise13.Run(); break;
                case "14": Exercise14.Run(); break;
                case "15": Exercise15.Run(); break;
                case "16": Exercise16.Run(); break;
                case "17": Exercise17.Run(); break;
                case "18": Exercise18.Run(); break;
                case "19": Exercise19.Run(); break;
                case "20": Exercise20.Run(); break;
                case "21": Exercise21.Run(); break;
                case "22": Exercise22.Run(); break;
                case "23": await Exercise23.RunAsync(); break;
                case "24": Exercise24.Run(); break;
                case "25": Exercise25.Run(); break;
                case "26": Exercise26.Run(); break;
                case "27": Exercise27.Run(); break;
                case "28": Exercise28.Run(); break;
                case "29": Exercise29.Run(); break;
                case "30": Exercise30.Run(); break;

                case "all":
                    Exercise02.Run();
                    Exercise03.Run();
                    Exercise04.Run();
                    Exercise05.Run();
                    Exercise06.Run();
                    Exercise07.Run();
                    Exercise08.Run();
                    Exercise09.Run();
                    Exercise10.Run();
                    Exercise11.Run();
                    Exercise12.Run();
                    Exercise13.Run();
                    Exercise14.Run();
                    Exercise15.Run();
                    Exercise16.Run();
                    Exercise17.Run();
                    Exercise18.Run();
                    Exercise19.Run();
                    Exercise20.Run();
                    Exercise21.Run();
                    Exercise22.Run();
                    await Exercise23.RunAsync();
                    Exercise24.Run();
                    Exercise25.Run();
                    Exercise26.Run();
                    Exercise27.Run();
                    Exercise28.Run();
                    Exercise29.Run();
                    break;

                default:
                    Console.WriteLine("C# / ADO.NET Exercises");
                    Console.WriteLine("Usage: dotnet run -- <exercise number 1-30>");
                    Console.WriteLine("       dotnet run -- all   (runs exercises 2-29)");
                    Console.WriteLine("Note: Exercise 1 is environment setup (no output).");
                    Console.WriteLine("      Exercise 30 requires a SQL Server connection string.");
                    break;
            }
        }
    }
}
