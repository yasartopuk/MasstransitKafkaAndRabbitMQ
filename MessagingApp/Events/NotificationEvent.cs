namespace MessagingApp.Events;

public record NotificationEvent
{
    public int Id { get; init; }
    public string Message { get; init; }
}


public record KafkaNotificationEvent
{
    public int Id { get; init; }
    public string Message { get; init; }
}