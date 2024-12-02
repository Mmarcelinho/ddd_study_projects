namespace SistemaCadastro.Domain.Notifications;

public sealed record Notification
{
    public Notification(string message, string propertyName)
    {
        Message = message;
        PropertyName = propertyName;
    }

    public string Message { get; }
    
    public string PropertyName { get; }
}