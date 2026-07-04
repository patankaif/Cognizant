namespace Module1DesignPatterns.SOLID.OCP
{
    
    namespace Bad
    {
        public enum CustomerType { Regular, Premium, Vip }

        public class DiscountCalculator
        {
            public decimal ApplyDiscount(decimal amount, CustomerType type)
            {
                if (type == CustomerType.Regular)
                    return amount * 0.95m;
                if (type == CustomerType.Premium)
                    return amount * 0.90m;
                if (type == CustomerType.Vip)
                    return amount * 0.80m;

                return amount;
            }
        }
    }

    namespace Good
    {
        public interface IDiscountStrategy
        {
            decimal Apply(decimal amount);
        }

        public class RegularDiscount : IDiscountStrategy
        {
            public decimal Apply(decimal amount) => amount * 0.95m;
        }

        public class PremiumDiscount : IDiscountStrategy
        {
            public decimal Apply(decimal amount) => amount * 0.90m;
        }

        public class VipDiscount : IDiscountStrategy
        {
            public decimal Apply(decimal amount) => amount * 0.80m;
        }

        public class BlackFridayDiscount : IDiscountStrategy
        {
            public decimal Apply(decimal amount) => amount * 0.50m;
        }

        public class DiscountCalculator
        {
            public decimal ApplyDiscount(decimal amount, IDiscountStrategy strategy)
                => strategy.Apply(amount);
        }
    }

    public static class OcpDemo
    {
        public static void Run()
        {
            Console.WriteLine("--- OCP: Bad (must edit class for every new type) ---");
            var badCalc = new Bad.DiscountCalculator();
            Console.WriteLine($"Regular: {badCalc.ApplyDiscount(100m, Bad.CustomerType.Regular):C}");
            Console.WriteLine($"VIP:     {badCalc.ApplyDiscount(100m, Bad.CustomerType.Vip):C}");

            Console.WriteLine();
            Console.WriteLine("--- OCP: Good (extend via new classes, no modification) ---");
            var calc = new Good.DiscountCalculator();
            Console.WriteLine($"Regular:      {calc.ApplyDiscount(100m, new Good.RegularDiscount()):C}");
            Console.WriteLine($"VIP:          {calc.ApplyDiscount(100m, new Good.VipDiscount()):C}");
            Console.WriteLine($"Black Friday: {calc.ApplyDiscount(100m, new Good.BlackFridayDiscount()):C}");
        }
    }
}
