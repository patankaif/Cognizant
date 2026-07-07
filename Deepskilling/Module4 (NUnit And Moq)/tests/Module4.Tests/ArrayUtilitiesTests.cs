using Module4.Core;
using NUnit.Framework;

namespace Module4.Tests;

[TestFixture]
public class ArrayUtilitiesTests
{
    [Test]
    public void GetEvenNumbers_ReturnsOnlyEvens()
    {
        var input = new[] { 1, 2, 3, 4, 5, 6 };
        var result = ArrayUtilities.GetEvenNumbers(input);
        Assert.That(result, Is.EqualTo(new[] { 2, 4, 6 }));
    }

    [Test]
    public void RemoveDuplicates_ReturnsDistinctValues()
    {
        var input = new[] { 1, 1, 2, 2, 3 };
        var result = ArrayUtilities.RemoveDuplicates(input);
        Assert.That(result, Is.EquivalentTo(new[] { 1, 2, 3 }));
    }

    [Test]
    public void SortDescending_ReturnsElementsInDescendingOrder()
    {
        var input = new List<int> { 3, 1, 4, 1, 5, 9, 2, 6 };
        var result = ArrayUtilities.SortDescending(input);
        Assert.That(result, Is.Ordered.Descending);
    }

    [Test]
    public void GetEvenNumbers_EmptyArray_ReturnsEmptyArray()
    {
        var result = ArrayUtilities.GetEvenNumbers(Array.Empty<int>());
        Assert.That(result, Is.Empty);
    }
}
