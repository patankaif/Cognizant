namespace Module2DataStructures.Sorting;


public static class HeapSort
{
    public static void Sort(int[] array)
    {
        int n = array.Length;
        for (int i = n / 2 - 1; i >= 0; i--)
            SiftDown(array, n, i);
        for (int end = n - 1; end > 0; end--)
        {
            (array[0], array[end]) = (array[end], array[0]);
            SiftDown(array, end, 0);
        }
    }

    private static void SiftDown(int[] array, int heapSize, int index)
    {
        int largest = index;
        int left = 2 * index + 1;
        int right = 2 * index + 2;

        if (left < heapSize && array[left] > array[largest])
            largest = left;

        if (right < heapSize && array[right] > array[largest])
            largest = right;

        if (largest != index)
        {
            (array[index], array[largest]) = (array[largest], array[index]);
            SiftDown(array, heapSize, largest);
        }
    }

    public static void Run()
    {
        Console.WriteLine("--- Heap Sort O(n log n) ---");
        int[] array = { 64, 34, 25, 12, 22, 11, 90 };
        Console.WriteLine($"Before: {string.Join(", ", array)}");
        Sort(array);
        Console.WriteLine($"After:  {string.Join(", ", array)}");
    }
}
