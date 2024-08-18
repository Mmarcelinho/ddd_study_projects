namespace FitTrack.Domain.Notifications;

public record Notification
{
    public Notification(string message, string propertyName)
    {
        Message = message;
        PropertyName = propertyName;
    }

    public string Message { get; } 
    public string PropertyName { get; }
}