namespace Module4.Core;

public class PricingCalculator
{
    public decimal CalculateFinalPrice(decimal basePrice, string region)
    {
        var tax = CalculateTax(basePrice, region);
        return basePrice + tax;
    }

    internal decimal CalculateTax(decimal basePrice, string region)
    {
        return region switch
        {
            "US" => basePrice * 0.07m,
            "EU" => basePrice * 0.20m,
            "UK" => basePrice * 0.20m,
            _ => 0m
        };
    }
}
