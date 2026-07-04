namespace Module2DataStructures.Sorting;


public static class InsertionSort
{
    public static void Sort(int[] array)
    {
        for (int i = 1; i < array.Length; i++)
        {
            int key = array[i];
            int j = i - 1;
            while (j >= 0 && array[j] > key)
            {
                array[j + 1] = array[j];
                j--;
            }
            array[j + 1] = key;
        }
    }

    public static void Run()
    {
        Console.WriteLine("--- Insertion Sort O(n^2) ---");
        int[] array = { 64, 34, 25, 12, 22, 11, 90 };
        Console.WriteLine($"Before: {string.Join(", ", array)}");
        Sort(array);
        Console.WriteLine($"After:  {string.Join(", ", array)}");
    }
}
