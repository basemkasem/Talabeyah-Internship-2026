namespace EShop.Console.Notifications;

public class EmailNotification : Notification
{
    public override void SendConfirmation()
    {
        System.Console.WriteLine("Email confirmation sent.");
    }
}
