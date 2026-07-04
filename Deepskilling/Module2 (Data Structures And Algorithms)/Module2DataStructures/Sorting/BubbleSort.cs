namespace Module2DataStructures.Sorting;

 
public static class BubbleSort
{
    public static void Sort(int[] array)
    {
        int n = array.Length;
        for (int pass = 0; pass < n - 1; pass++)
        {
            bool swapped = false;
            for (int i = 0; i < n - pass - 1; i++)
            {
                if (array[i] > array[i + 1])
                {
                    (array[i], array[i + 1]) = (array[i + 1], array[i]);
                    swapped = true;
                }
            }
            if (!swapped) break;
        }
    }

    public static void Run()
    {
        Console.WriteLine("--- Bubble Sort O(n^2) ---");
        int[] array = { 64, 34, 25, 12, 22, 11, 90 };
        Console.WriteLine($"Before: {string.Join(", ", array)}");
        Sort(array);
        Console.WriteLine($"After:  {string.Join(", ", array)}");
    }
}
