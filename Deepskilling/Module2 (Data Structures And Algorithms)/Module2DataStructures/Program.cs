using Module2DataStructures.Complexity;
using Module2DataStructures.Arrays;
using Module2DataStructures.LinkedLists;
using Module2DataStructures.Sorting;
using Module2DataStructures.Searching;

// Module 2 - Data Structures and Algorithms
// Run with `dotnet run` and pick a number, or pass an argument, e.g.:
//   dotnet run -- bubblesort
//   dotnet run -- all

var demos = new (string Key, string Title, Action Run)[]
{
    ("analysis",   "Analysis of Algorithms - Fibonacci (naive vs memoized vs iterative)", AlgorithmAnalysisDemo.Run),
    ("arrays",     "Arrays - traversal, search, insert, remove",                          ArrayOperationsDemo.Run),
    ("sll",        "Linked List - Singly Linked List",                                    SinglyLinkedListDemo.Run),
    ("csll",       "Linked List - Circular Singly Linked List",                           CircularSinglyLinkedListDemo.Run),
    ("dll",        "Linked List - Doubly Linked List",                                    DoublyLinkedListDemo.Run),
    ("cdll",       "Linked List - Circular Doubly Linked List",                           CircularDoublyLinkedListDemo.Run),
    ("bubble",     "Sorting - Bubble Sort",                                               BubbleSort.Run),
    ("insertion",  "Sorting - Insertion Sort",                                            InsertionSort.Run),
    ("merge",      "Sorting - Merge Sort",                                                MergeSort.Run),
    ("quick",      "Sorting - Quick Sort",                                                QuickSort.Run),
    ("heap",       "Sorting - Heap Sort",                                                 HeapSort.Run),
    ("search",     "Searching - Linear Search vs Binary Search",                          SearchingDemo.Run),
};

void RunAll()
{
    foreach (var demo in demos)
    {
        PrintHeader(demo.Title);
        demo.Run();
        Console.WriteLine();
    }
}

void PrintHeader(string title)
{
    Console.WriteLine(new string('=', 70));
    Console.WriteLine(title);
    Console.WriteLine(new string('=', 70));
}

if (args.Length > 0)
{
    var key = args[0].Trim().ToLowerInvariant();
    if (key == "all")
    {
        RunAll();
        return;
    }

    var match = demos.FirstOrDefault(d => d.Key == key);
    if (match.Run != null)
    {
        PrintHeader(match.Title);
        match.Run();
        return;
    }

    Console.WriteLine($"Unknown demo key '{key}'. Valid keys: all, {string.Join(", ", demos.Select(d => d.Key))}");
    return;
}

while (true)
{
    Console.WriteLine();
    Console.WriteLine("Module 2 - Data Structures and Algorithms");
    Console.WriteLine("-------------------------------------------");
    for (int i = 0; i < demos.Length; i++)
        Console.WriteLine($" {i + 1,2}. {demos[i].Title}");
    Console.WriteLine($" {demos.Length + 1,2}. Run ALL demos");
    Console.WriteLine("  0. Exit");
    Console.Write("Choose an option: ");

    var input = Console.ReadLine();
    if (!int.TryParse(input, out int choice))
    {
        Console.WriteLine("Please enter a valid number.");
        continue;
    }

    if (choice == 0) break;

    if (choice == demos.Length + 1)
    {
        RunAll();
        continue;
    }

    if (choice >= 1 && choice <= demos.Length)
    {
        var demo = demos[choice - 1];
        Console.WriteLine();
        PrintHeader(demo.Title);
        demo.Run();
    }
    else
    {
        Console.WriteLine("Choice out of range.");
    }
}
