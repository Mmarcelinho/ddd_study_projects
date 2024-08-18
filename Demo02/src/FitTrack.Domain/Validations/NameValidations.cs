using FitTrack.Domain.Contexts.User.Aggregates.ValueObjects;
using FitTrack.Domain.Notifications;

namespace FitTrack.Domain.Validations;

public partial class ContractValidations<T>
{
    public ContractValidations<T> NameIsOk(string name, short minLength, short maxLength, string message, string propertyName)
    {
        if (string.IsNullOrEmpty(name) || (name.Length < minLength) || (name.Length > maxLength))
            AddNotification(new Notification(message, propertyName));

        return this;
    }

    public ContractValidations<T> FirstNameIsOk(Name name, short minLength, short maxLength, string message, string propertyName)
    {
        if (string.IsNullOrEmpty(name.FirstName) || (name.FirstName.Length < minLength) || (name.FirstName.Length > maxLength))
            AddNotification(new Notification(message, propertyName));

        return this;
    }

    public ContractValidations<T> LastNameIsOk(Name name, short minLength, short maxLength, string message, string propertyName)
    {
        if (string.IsNullOrEmpty(name.FirstName) || (name.LastName.Length < minLength) || (name.LastName.Length > maxLength))
            AddNotification(new Notification(message, propertyName));

        return this;
    }
}
