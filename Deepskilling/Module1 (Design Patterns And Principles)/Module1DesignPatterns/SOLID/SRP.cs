namespace Module1DesignPatterns.SOLID.SRP
{
    
    namespace Bad
    {
        public class Invoice
        {
            public string CustomerName { get; set; } = string.Empty;
            public List<(string Item, decimal Price)> LineItems { get; } = new();

            public decimal CalculateTotal() => LineItems.Sum(li => li.Price);

            public void SaveToDatabase()
            {
                Console.WriteLine($"[DB] Saving invoice for {CustomerName} to database...");
            }

            public void PrintInvoice()
            {
                Console.WriteLine($"Invoice for {CustomerName}");
                foreach (var (item, price) in LineItems)
                    Console.WriteLine($" - {item}: {price:C}");
                Console.WriteLine($"Total: {CalculateTotal():C}");
            }
        }
    }

    
    namespace Good
    {
        public class Invoice
        {
            public string CustomerName { get; set; } = string.Empty;
            public List<(string Item, decimal Price)> LineItems { get; } = new();

            public decimal CalculateTotal() => LineItems.Sum(li => li.Price);
        }

        public class InvoiceRepository
        {
            public void Save(Invoice invoice)
            {
                Console.WriteLine($"[DB] Saving invoice for {invoice.CustomerName} to database...");
            }
        }

        public class InvoicePrinter
        {
            public void Print(Invoice invoice)
            {
                Console.WriteLine($"Invoice for {invoice.CustomerName}");
                foreach (var (item, price) in invoice.LineItems)
                    Console.WriteLine($" - {item}: {price:C}");
                Console.WriteLine($"Total: {invoice.CalculateTotal():C}");
            }
        }
    }

    public static class SrpDemo
    {
        public static void Run()
        {
            Console.WriteLine("--- SRP: Bad (mixed responsibilities) ---");
            var badInvoice = new Bad.Invoice { CustomerName = "Acme Corp" };
            badInvoice.LineItems.Add(("Widget", 9.99m));
            badInvoice.PrintInvoice();
            badInvoice.SaveToDatabase();

            Console.WriteLine();
            Console.WriteLine("--- SRP: Good (separated responsibilities) ---");
            var invoice = new Good.Invoice { CustomerName = "Acme Corp" };
            invoice.LineItems.Add(("Widget", 9.99m));

            var printer = new Good.InvoicePrinter();
            var repository = new Good.InvoiceRepository();

            printer.Print(invoice);
            repository.Save(invoice);
        }
    }
}
