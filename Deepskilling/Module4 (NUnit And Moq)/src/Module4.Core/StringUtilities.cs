namespace Module4.Core;

public static class StringUtilities
{
    public static string Reverse(string value)
    {
        if (value is null) throw new ArgumentNullException(nameof(value));
        var chars = value.ToCharArray();
        Array.Reverse(chars);
        return new string(chars);
    }

    public static bool IsPalindrome(string value)
    {
        if (value is null) throw new ArgumentNullException(nameof(value));
        var normalized = new string(value.Where(char.IsLetterOrDigit).ToArray()).ToLowerInvariant();
        return normalized == Reverse(normalized);
    }

    public static int CountVowels(string value)
    {
        if (value is null) throw new ArgumentNullException(nameof(value));
        return value.Count(c => "aeiouAEIOU".IndexOf(c) >= 0);
    }

    public static string Capitalize(string value)
    {
        if (string.IsNullOrEmpty(value)) return value;
        return char.ToUpperInvariant(value[0]) + value.Substring(1).ToLowerInvariant();
    }
}
