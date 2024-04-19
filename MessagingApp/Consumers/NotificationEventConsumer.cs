using MassTransit;
using MessagingApp.Events;

public class NotificationEventConsumer : IConsumer<NotificationEvent>
{
    public async Task Consume(ConsumeContext<NotificationEvent> context)
    {
        await Task.Delay(TimeSpan.FromSeconds(1));
        var notification = context.Message;
        Console.WriteLine($"Notification Received: {notification.Message} with ID {notification.Id} - {DateTime.Now}");
        // Notification işlemlerini gerçekleştirin.
    }
}
