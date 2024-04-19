using MassTransit;
using MessagingApp.Events;

public class LogEventConsumer : IConsumer<LogEvent>
{
    public async Task Consume(ConsumeContext<LogEvent> context)
    {
        var log = context.Message;
        Console.WriteLine($"Log Received: {log.Message} at {log.Timestamp} - {DateTime.Now}");
        // Log mesajını işleyin veya log kaydını saklayın.
    }
}