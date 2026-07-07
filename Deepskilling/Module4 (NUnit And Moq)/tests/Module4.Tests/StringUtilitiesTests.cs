using Module4.Core;
using NUnit.Framework;

namespace Module4.Tests;

[TestFixture]
public class StringUtilitiesTests
{
    [TestCase("hello", "olleh")]
    [TestCase("A", "A")]
    [TestCase("", "")]
    public void Reverse_ReturnsReversedString(string input, string expected)
    {
        var result = StringUtilities.Reverse(input);
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void Reverse_NullInput_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => StringUtilities.Reverse(null!));
    }

    [TestCase("racecar", true)]
    [TestCase("A man a plan a canal Panama", true)]
    [TestCase("hello", false)]
    public void IsPalindrome_ReturnsExpectedResult(string input, bool expected)
    {
        var result = StringUtilities.IsPalindrome(input);
        Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase("Hello World", 3)]
    [TestCase("xyz", 0)]
    [TestCase("AEIOU", 5)]
    public void CountVowels_ReturnsExpectedCount(string input, int expected)
    {
        Assert.That(StringUtilities.CountVowels(input), Is.EqualTo(expected));
    }

    [TestCase("hELLO", "Hello")]
    [TestCase("", "")]
    public void Capitalize_ReturnsExpectedResult(string input, string expected)
    {
        Assert.That(StringUtilities.Capitalize(input), Is.EqualTo(expected));
    }
}
