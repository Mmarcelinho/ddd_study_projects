using FitTrack.Domain.Notifications;
using FitTrack.Domain.Validations.Interfaces;

namespace FitTrack.Domain.Validations;

public partial class ContractValidations<T> where T : IContract
{
    private readonly List<Notification> _notifications;

    public ContractValidations()
    {
        _notifications = [];
    }

    public IReadOnlyCollection<Notification> Notifications => _notifications.AsReadOnly();

    public void AddNotification(Notification notification) => _notifications.Add(notification);

    public bool IsValid() => _notifications.Count == 0;
}
