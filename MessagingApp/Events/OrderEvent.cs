namespace MessagingApp.Events;

public record OrderEvent
{
    public int OrderId { get; init; }
    public string CustomerName { get; init; }
    public string Message { get; init; }
}