namespace Module2DataStructures.Searching;


public static class LinearSearch
{
    public static int Search(int[] array, int target)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == target)
                return i;
        }
        return -1;
    }
}

public static class BinarySearch
{
    public static int Search(int[] sortedArray, int target)
    {
        int low = 0;
        int high = sortedArray.Length - 1;

        while (low <= high)
        {
            int mid = low + (high - low) / 2;

            if (sortedArray[mid] == target)
                return mid;

            if (sortedArray[mid] < target)
                low = mid + 1;   
            else
                high = mid - 1;  
        }

        return -1; 
    }
    public static int SearchRecursive(int[] sortedArray, int target, int low, int high)
    {
        if (low > high) return -1;

        int mid = low + (high - low) / 2;

        if (sortedArray[mid] == target) return mid;

        return sortedArray[mid] < target
            ? SearchRecursive(sortedArray, target, mid + 1, high)
            : SearchRecursive(sortedArray, target, low, mid - 1);
    }
}

public static class SearchingDemo
{
    public static void Run()
    {
        Console.WriteLine("--- Searching: Linear vs Binary ---");

        int[] unsorted = { 42, 17, 8, 99, 23, 4, 56 };
        int target = 23;
        int linearIndex = LinearSearch.Search(unsorted, target);
        Console.WriteLine($"LinearSearch on unsorted array for {target} => index {linearIndex} " +
                           $"(comparisons could be up to n = {unsorted.Length})");

        int[] sorted = { 4, 8, 17, 23, 42, 56, 99 };
        int binaryIndex = BinarySearch.Search(sorted, target);
        Console.WriteLine($"BinarySearch on sorted array for {target}   => index {binaryIndex} " +
                           $"(comparisons at most log2(n) ≈ {Math.Ceiling(Math.Log2(sorted.Length))})");

        int recursiveIndex = BinarySearch.SearchRecursive(sorted, target, 0, sorted.Length - 1);
        Console.WriteLine($"BinarySearch (recursive) for {target}      => index {recursiveIndex}");

        int missing = 100;
        Console.WriteLine($"BinarySearch for missing value {missing}   => index {BinarySearch.Search(sorted, missing)} (not found)");
    }
}
