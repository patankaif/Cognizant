namespace Module4.Core.Interfaces;

public interface INotificationService
{
    void SendOrderConfirmation(string email, string orderId);
}
