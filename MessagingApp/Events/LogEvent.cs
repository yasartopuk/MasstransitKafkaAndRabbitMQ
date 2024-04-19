namespace MessagingApp.Events;

public record LogEvent
{
    public string Message { get; init; }
    public DateTime Timestamp { get; init; }
}