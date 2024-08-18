using FitTrack.Domain.Notifications;
using FitTrack.Domain.Validations.Interfaces;

namespace FitTrack.Domain.Contexts.Workout.Abstractions;

public abstract class Entity : IValidate
{
    private List<Notification> _notifications;

    protected Entity()
    {
        Id = Guid.NewGuid();
        DateCreatedUtc = DateTime.UtcNow;
        _notifications = new List<Notification>();
    }

    public Guid Id { get; private set; }

    public DateTime DateCreatedUtc { get; private set; }

    public IReadOnlyCollection<Notification> Notifications => _notifications.AsReadOnly();

    public void SetNotificationList(List<Notification> notifications) => _notifications = notifications;

    public abstract bool Validate();
}
