using MassTransit;
using MessagingApp.Events;

namespace MessagingApp;

public static class MassTransitConfiguration
{
    public static IServiceCollection AddMasstransit(this IServiceCollection services)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<LogEventConsumer>();
            x.AddConsumer<OrderEventConsumer>();
            x.AddConsumer<NotificationEventConsumer>();
            
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("rabbitmq", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                
                cfg.ReceiveEndpoint("order-queue", e =>
                {
                    e.ConfigureConsumer<OrderEventConsumer>(context);
                });

                cfg.ReceiveEndpoint("notification-queue", e =>
                {
                    e.ConfigureConsumer<NotificationEventConsumer>(context);
                });
            });
            
            x.AddRider(rider =>
            {
                rider.AddProducer<LogEvent>("log-topic");
                rider.AddProducer<NotificationEvent>("notification-topic");
            
                rider.AddConsumer<LogEventConsumer>();
                rider.AddConsumer<NotificationEventConsumer>();
                
                rider.UsingKafka((context, k) =>
                {
                    k.Host("kafka:9092"); 
                    
                    k.TopicEndpoint<LogEvent>("log-topic", "log-group", e =>
                    {
                        e.ConfigureConsumer<LogEventConsumer>(context);
                    });
                    
                    k.TopicEndpoint<NotificationEvent>("notification-topic", "notification-group", e =>
                    {
                        e.ConfigureConsumer<NotificationEventConsumer>(context);
                    });
                });
            });
        });
        return services;
    }
}