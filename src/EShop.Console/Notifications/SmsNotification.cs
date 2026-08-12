namespace EShop.Console.Notifications;

public class SmsNotification : Notification
{
    public override void SendConfirmation()
    {
        System.Console.WriteLine("SMS confirmation sent.");
    }
}
