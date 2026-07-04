namespace Module1DesignPatterns.SOLID.DIP
{
    namespace Bad
    {
        public class EmailSender
        {
            public void SendEmail(string to, string message)
                => Console.WriteLine($"[Email] To: {to} | {message}");
        }

        public class NotificationService
        {
            private readonly EmailSender _emailSender = new(); 

            public void Notify(string recipient, string message)
            {
                _emailSender.SendEmail(recipient, message);
            }
        }
    }

    namespace Good
    {
        public interface INotificationChannel
        {
            void Send(string to, string message);
        }

        public class EmailSender : INotificationChannel
        {
            public void Send(string to, string message)
                => Console.WriteLine($"[Email] To: {to} | {message}");
        }

        public class SmsSender : INotificationChannel
        {
            public void Send(string to, string message)
                => Console.WriteLine($"[SMS] To: {to} | {message}");
        }

        public class NotificationService
        {
            private readonly INotificationChannel _channel;

            public NotificationService(INotificationChannel channel)
            {
                _channel = channel;
            }

            public void Notify(string recipient, string message)
            {
                _channel.Send(recipient, message);
            }
        }
    }

    public static class DipDemo
    {
        public static void Run()
        {
            Console.WriteLine("--- DIP: Bad (high-level class hard-coded to a concrete detail) ---");
            var badService = new Bad.NotificationService();
            badService.Notify("alice@example.com", "Your order shipped.");

            Console.WriteLine();
            Console.WriteLine("--- DIP: Good (depends on an abstraction; channel is injected) ---");
            var emailService = new Good.NotificationService(new Good.EmailSender());
            emailService.Notify("alice@example.com", "Your order shipped.");

            var smsService = new Good.NotificationService(new Good.SmsSender());
            smsService.Notify("+1-555-0100", "Your order shipped.");
        }
    }
}
