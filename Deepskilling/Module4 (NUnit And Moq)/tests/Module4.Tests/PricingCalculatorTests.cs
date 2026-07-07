using System.Reflection;
using Module4.Core;
using NUnit.Framework;

namespace Module4.Tests;

[TestFixture]
public class PricingCalculatorTests
{
    private PricingCalculator _calculator = null!;

    [SetUp]
    public void SetUp()
    {
        _calculator = new PricingCalculator();
    }

    [TestCase(100, "US", 107)]
    [TestCase(100, "EU", 120)]
    [TestCase(100, "XX", 100)]
    public void CalculateFinalPrice_ReturnsExpectedTotal(decimal basePrice, string region, decimal expected)
    {
        var result = _calculator.CalculateFinalPrice(basePrice, region);
        Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase(100, "US", 7)]
    [TestCase(100, "UK", 20)]
    public void CalculateTax_InternalMethod_DirectlyTestable_ViaInternalsVisibleTo(decimal basePrice, string region, decimal expected)
    {
        var tax = _calculator.CalculateTax(basePrice, region);
        Assert.That(tax, Is.EqualTo(expected));
    }

    [Test]
    public void CalculateTax_InvokedViaReflection_AlternativeForTrulyPrivateMembers()
    {
        var method = typeof(PricingCalculator).GetMethod(
            "CalculateTax",
            BindingFlags.NonPublic | BindingFlags.Instance);

        var result = method!.Invoke(_calculator, new object[] { 100m, "EU" });

        Assert.That(result, Is.EqualTo(20m));
    }
}
