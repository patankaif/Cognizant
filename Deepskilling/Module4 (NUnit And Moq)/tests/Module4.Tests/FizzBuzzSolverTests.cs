using Module4.Core;
using NUnit.Framework;

namespace Module4.Tests;

[TestFixture]
public class FizzBuzzSolverTests
{
    private FizzBuzzSolver _solver = null!;

    [SetUp]
    public void SetUp()
    {
        _solver = new FizzBuzzSolver();
    }

    [TestCase(1, "1")]
    [TestCase(2, "2")]
    [TestCase(3, "Fizz")]
    [TestCase(5, "Buzz")]
    [TestCase(6, "Fizz")]
    [TestCase(10, "Buzz")]
    [TestCase(15, "FizzBuzz")]
    [TestCase(30, "FizzBuzz")]
    public void Convert_ReturnsExpectedValue(int number, string expected)
    {
        Assert.That(_solver.Convert(number), Is.EqualTo(expected));
    }

    [Test]
    public void Convert_ZeroOrNegative_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _solver.Convert(0));
        Assert.Throws<ArgumentOutOfRangeException>(() => _solver.Convert(-5));
    }
}
