using MassTransit;

namespace MessagingApp;

public static class MassTransitConfiguration
{
    public static IServiceCollection AddMasstransit(this IServiceCollection services)
    {
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("rabbitmq", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
            });

            x.AddRider(rider =>
            {
                rider.AddProducer<LogEvent>("log-topic");
        
                rider.UsingKafka((context, k) =>
                {
                    k.Host("kafka:9092");
                });
            });
        });
        return services;
    }
}

public record LogEvent
{
    public string Message { get; init; }
    public DateTime Timestamp { get; init; }
}