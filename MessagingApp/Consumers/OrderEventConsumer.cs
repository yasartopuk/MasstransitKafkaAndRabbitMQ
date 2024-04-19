using MassTransit;
using MessagingApp.Events;

public class OrderEventConsumer : IConsumer<OrderEvent>
{
    public async Task Consume(ConsumeContext<OrderEvent> context)
    {
        await Task.Delay(TimeSpan.FromSeconds(1));
        var message = context.Message;
        Console.WriteLine($"Processing Order: {message.OrderId}, Customer: {message.CustomerName}, Message: {message.Message} - {DateTime.Now}");
        // Burada gelen siparişle ilgili işlemleri gerçekleştirebilirsiniz.
    }
}