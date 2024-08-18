using FitTrack.Domain.Notifications;

namespace FitTrack.Domain.Validations;

public partial class ContractValidations<T>
{
    public ContractValidations<T> ContentIsOk(string content, short minLength, short maxLength, string message, string propertyName)
    {
        if (string.IsNullOrEmpty(content) || (content.Length < minLength) || (content.Length > maxLength))
            AddNotification(new Notification(message, propertyName));

        return this;
    }

}