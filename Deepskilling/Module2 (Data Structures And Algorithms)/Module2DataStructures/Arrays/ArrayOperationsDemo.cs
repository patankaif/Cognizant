namespace Module2DataStructures.Arrays;


public static class ArrayOperationsDemo
{
    public static void Traverse(int[] array)
    {
        Console.Write("Traversal: ");
        foreach (var item in array)
            Console.Write($"{item} ");
        Console.WriteLine();
    }

    public static int LinearSearch(int[] array, int target)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == target)
                return i;
        }
        return -1;
    }

    public static int[] InsertAt(int[] array, int index, int value)
    {
        if (index < 0 || index > array.Length)
            throw new ArgumentOutOfRangeException(nameof(index));

        var result = new int[array.Length + 1];
        System.Array.Copy(array, 0, result, 0, index);
        result[index] = value;
        System.Array.Copy(array, index, result, index + 1, array.Length - index);
        return result;
    }

    public static int[] RemoveAt(int[] array, int index)
    {
        if (index < 0 || index >= array.Length)
            throw new ArgumentOutOfRangeException(nameof(index));

        var result = new int[array.Length - 1];
        System.Array.Copy(array, 0, result, 0, index);
        System.Array.Copy(array, index + 1, result, index, array.Length - index - 1);
        return result;
    }

    public static void Run()
    {
        Console.WriteLine("--- Arrays: traversal, search, insert, remove ---");

        int[] numbers = { 10, 20, 30, 40, 50 };
        Traverse(numbers);

        int target = 30;
        int foundIndex = LinearSearch(numbers, target);
        Console.WriteLine($"LinearSearch({target}) => index {foundIndex}");

        var afterInsert = InsertAt(numbers, 2, 25);
        Console.Write("After InsertAt(index=2, value=25): ");
        Traverse(afterInsert);

        var afterRemove = RemoveAt(afterInsert, 0);
        Console.Write("After RemoveAt(index=0): ");
        Traverse(afterRemove);
    }
}
