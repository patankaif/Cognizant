# Module 2 – Data Structures and Algorithms (C#)

A runnable .NET 8 console application covering every learning objective in
**Module 2** of the DN 5.0 Deep Skilling handbook.

## Topics covered

- **Analysis of Algorithms** — Big-O intuition via three Fibonacci
  implementations (naive recursive O(2^n), memoized O(n), iterative O(n))
  with actual timing comparisons.
- **Arrays** — traversal, linear search, insert, remove, with Big-O notes
  on why insert/remove are O(n) for arrays.
- **Linked Lists** — all four variants from the handbook, each with
  insert (head/tail), search, delete, and traverse:
  - Singly Linked List
  - Circular Singly Linked List
  - Doubly Linked List
  - Circular Doubly Linked List
- **Sorting** — Bubble, Insertion, Merge, Quick, and Heap Sort, each with
  a comment block explaining the algorithm, its time/space complexity,
  and whether it's stable.
- **Searching** — Linear Search vs. Binary Search, with a comparison of
  how many comparisons each needs.

## Project layout

```
Module2DataStructures/
  Complexity/
    AlgorithmAnalysisDemo.cs
  Arrays/
    ArrayOperationsDemo.cs
  LinkedLists/
    SinglyLinkedList.cs
    CircularSinglyLinkedList.cs
    DoublyLinkedList.cs
    CircularDoublyLinkedList.cs
  Sorting/
    BubbleSort.cs
    InsertionSort.cs
    MergeSort.cs
    QuickSort.cs
    HeapSort.cs
  Searching/
    SearchAlgorithms.cs
  Program.cs
  Module2DataStructures.csproj
```

## How to run

**Option 1 – Visual Studio**
1. Open `Module2DataStructures.csproj` in Visual Studio.
2. Press `F5` / `Ctrl+F5` to run.
3. Pick a number from the interactive menu, or choose "Run ALL demos".

**Option 2 – .NET CLI**
```bash
cd Module2DataStructures
dotnet run                 # interactive menu
dotnet run -- all          # run every demo in sequence
dotnet run -- quick        # run just the Quick Sort demo
dotnet run -- cdll         # run just the Circular Doubly Linked List demo
```

Valid keys: `analysis, arrays, sll, csll, dll, cdll, bubble, insertion,
merge, quick, heap, search, all`

Requires the **.NET 8 SDK**.

## Practice ideas for self-evaluation

- Add a `RemoveDuplicates()` method to `SinglyLinkedList<T>`.
- Add a `Reverse()` method to `DoublyLinkedList<T>` that reverses the list
  in place (swap Next/Previous on every node, O(n) time, O(1) space).
- Modify `QuickSort` to use a "median of three" pivot selection to reduce
  the chance of hitting the O(n^2) worst case on nearly-sorted input.
- Time all five sorting algorithms against a large randomly generated
  array (e.g. 100,000 elements) and compare their actual running times
  to confirm the theoretical complexity differences.
- Implement `BinarySearch` to find the *first* occurrence of a duplicate
  value in a sorted array (a common interview variant).
