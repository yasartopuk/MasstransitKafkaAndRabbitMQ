using MassTransit;
using MessagingApp.Events;

namespace MessagingApp.Configurations;

public static class EndpointsExtension
{
    public static void UseEndpoints(this WebApplication app)
    {
        app.MapPost("/publishRabbit", async (IPublishEndpoint publishEndpoint, OrderEvent order) =>
        {
            order ??= new OrderEvent()
            {
                OrderId = new Random().Next(1000, 9999),
                Message = "OrderEvent message"
            };
            await publishEndpoint.Publish(order);
            Console.WriteLine($"OrderEvent gönderildi: {order.OrderId} - {DateTime.Now}");
            return Results.Ok($"RabbitMQ: Order with ID {order.OrderId} published.");
        });

        app.MapPost("/publishKafka", async (ITopicProducer<LogEvent> logProducer, LogEvent log) =>
        {
            log ??= new LogEvent()
            {
                Timestamp = new DateTime(),
                Message = "LogEvent message"
            };
            await logProducer.Produce(log);
            Console.WriteLine($"LogEvent gönderildi: {log.Timestamp} - {DateTime.Now}");

            return Results.Ok($"Kafka: Log event at {log.Timestamp} published.");
        });

        app.MapPost("/publishNotification", async (IPublishEndpoint publishEndpoint,
            ITopicProducer<NotificationEvent> kafkaProducer, NotificationEvent notification) =>
        {
            notification = new NotificationEvent()
            {
                Id = new Random().Next(1000, 9999),
                Message = "RabbitMQ message"
            };

            // RabbitMQ'ya mesaj gönder
            await publishEndpoint.Publish(notification);
            
            var notification2 = new NotificationEvent()
            {
                Id = new Random().Next(1000, 9999),
                Message = "Kafka message"
            };
            
            // Kafka'ya mesaj gönder
            await kafkaProducer.Produce(notification2);
            Console.WriteLine($"NotificationEvent gönderildi: {notification.Id} - {DateTime.Now}");

            return Results.Ok($"Notification with ID {notification.Id} sent to both RabbitMQ and Kafka.");
        });
    }
}