namespace Module2DataStructures.Sorting;

public static class MergeSort
{
    public static void Sort(int[] array)
    {
        if (array.Length <= 1) return;
        SortRange(array, 0, array.Length - 1);
    }

    private static void SortRange(int[] array, int left, int right)
    {
        if (left >= right) return;

        int middle = left + (right - left) / 2;
        SortRange(array, left, middle);       
        SortRange(array, middle + 1, right);  
        Merge(array, left, middle, right);   
    }

    private static void Merge(int[] array, int left, int middle, int right)
    {
        int leftLength = middle - left + 1;
        int rightLength = right - middle;

        int[] leftArray = new int[leftLength];
        int[] rightArray = new int[rightLength];

        Array.Copy(array, left, leftArray, 0, leftLength);
        Array.Copy(array, middle + 1, rightArray, 0, rightLength);

        int i = 0, j = 0, k = left;
        while (i < leftLength && j < rightLength)
        {
            if (leftArray[i] <= rightArray[j])
                array[k++] = leftArray[i++];
            else
                array[k++] = rightArray[j++];
        }

        while (i < leftLength) array[k++] = leftArray[i++];
        while (j < rightLength) array[k++] = rightArray[j++];
    }

    public static void Run()
    {
        Console.WriteLine("--- Merge Sort O(n log n) ---");
        int[] array = { 64, 34, 25, 12, 22, 11, 90 };
        Console.WriteLine($"Before: {string.Join(", ", array)}");
        Sort(array);
        Console.WriteLine($"After:  {string.Join(", ", array)}");
    }
}
