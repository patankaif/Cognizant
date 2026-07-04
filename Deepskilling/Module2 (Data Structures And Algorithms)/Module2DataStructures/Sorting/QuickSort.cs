namespace Module2DataStructures.Sorting;


public static class QuickSort
{
    public static void Sort(int[] array)
    {
        if (array.Length <= 1) return;
        SortRange(array, 0, array.Length - 1);
    }

    private static void SortRange(int[] array, int low, int high)
    {
        if (low >= high) return;

        int pivotIndex = Partition(array, low, high);
        SortRange(array, low, pivotIndex - 1);
        SortRange(array, pivotIndex + 1, high);
    }

    private static int Partition(int[] array, int low, int high)
    {
        int pivot = array[high];
        int i = low - 1; 

        for (int j = low; j < high; j++)
        {
            if (array[j] < pivot)
            {
                i++;
                (array[i], array[j]) = (array[j], array[i]);
            }
        }

        (array[i + 1], array[high]) = (array[high], array[i + 1]);
        return i + 1;
    }

    public static void Run()
    {
        Console.WriteLine("--- Quick Sort O(n log n) average ---");
        int[] array = { 64, 34, 25, 12, 22, 11, 90 };
        Console.WriteLine($"Before: {string.Join(", ", array)}");
        Sort(array);
        Console.WriteLine($"After:  {string.Join(", ", array)}");
    }
}
