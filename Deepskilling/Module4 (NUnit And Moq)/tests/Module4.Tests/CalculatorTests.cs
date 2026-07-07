using Module4.Core;
using Module4.Core.Interfaces;
using Moq;
using NUnit.Framework;

namespace Module4.Tests;

[TestFixture]
public class CalculatorTests
{
    private Mock<IHistoryLogger> _loggerMock = null!;
    private Calculator _calculator = null!;

    [SetUp]
    public void SetUp()
    {
        _loggerMock = new Mock<IHistoryLogger>();
        _calculator = new Calculator(_loggerMock.Object);
    }

    [TearDown]
    public void TearDown()
    {
        _loggerMock = null!;
        _calculator = null!;
    }

    [TestCase(2, 3, 5)]
    [TestCase(-2, 2, 0)]
    [TestCase(0, 0, 0)]
    public void Add_ReturnsSum(decimal a, decimal b, decimal expected)
    {
        var result = _calculator.Add(a, b);
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void Add_LogsOperation()
    {
        _calculator.Add(2, 3);
        _loggerMock.Verify(l => l.Log(It.Is<string>(s => s.Contains("Add"))), Times.Once);
    }

    [TestCase(10, 2, 5)]
    [TestCase(9, 3, 3)]
    public void Divide_ReturnsQuotient(decimal a, decimal b, decimal expected)
    {
        Assert.That(_calculator.Divide(a, b), Is.EqualTo(expected));
    }

    [Test]
    public void Divide_ByZero_ThrowsDivideByZeroException()
    {
        Assert.Throws<DivideByZeroException>(() => _calculator.Divide(10, 0));
    }

    [Test]
    public void Divide_ByZero_LogsFailure()
    {
        Assert.Throws<DivideByZeroException>(() => _calculator.Divide(10, 0));
        _loggerMock.Verify(l => l.Log(It.Is<string>(s => s.Contains("failed"))), Times.Once);
    }

    [Test]
    public void ClearHistory_CallsLoggerOnce()
    {
        _calculator.ClearHistory();
        _loggerMock.Verify(l => l.Log(It.IsAny<string>()), Times.Once);
    }
}
