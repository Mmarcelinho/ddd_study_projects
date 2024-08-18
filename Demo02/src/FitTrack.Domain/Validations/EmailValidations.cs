using System.Text.RegularExpressions;
using FitTrack.Domain.Contexts.User.Aggregates.ValueObjects;
using FitTrack.Domain.Notifications;

namespace FitTrack.Domain.Validations;

    public partial class ContractValidations<T> 
    {
        public ContractValidations<T> EmailIsValid(Email email, string message, string propertyName)
        {
            if(string.IsNullOrEmpty(email.Value) || !Regex.IsMatch(email.Value, @"^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$"))            
                AddNotification(new Notification(message, propertyName));
            
            return this;
            
        }
    }
