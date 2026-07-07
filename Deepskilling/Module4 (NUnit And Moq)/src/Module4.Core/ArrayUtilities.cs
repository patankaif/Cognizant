namespace Module4.Core;

public static class ArrayUtilities
{
    public static int[] GetEvenNumbers(int[] numbers)
    {
        return numbers.Where(n => n % 2 == 0).ToArray();
    }

    public static int[] RemoveDuplicates(int[] numbers)
    {
        return numbers.Distinct().ToArray();
    }

    public static List<int> SortDescending(IEnumerable<int> numbers)
    {
        return numbers.OrderByDescending(n => n).ToList();
    }
}
