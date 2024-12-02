using System.Text.RegularExpressions;
using SistemaCadastro.Domain.Contexts.Cadastro.Aggregates.ValueObjects;
using SistemaCadastro.Domain.Notifications;

namespace SistemaCadastro.Domain.Validations;

public partial class ContractValidations<T>
{
    public ContractValidations<T> EmailIsValid(Email email, string message, string propertyName)
    {
        if (string.IsNullOrEmpty(email.Valor) || !Regex.IsMatch(email.Valor, @"^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$"))
            AddNotification(new Notification(message, propertyName));

        return this;

    }
}
