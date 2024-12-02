using SistemaCadastro.Domain.Contexts.Cadastro.Aggregates.ValueObjects;
using SistemaCadastro.Domain.Notifications;

namespace SistemaCadastro.Domain.Validations;

public partial class ContractValidations<T>
{
    public ContractValidations<T> TelefoneIsValid(Telefone telefone, short minLength, short maxLength, string message, string propertyName)
    {
        if ((telefone.Numero < minLength) || (telefone.Numero > maxLength))
            AddNotification(new Notification(message, propertyName + " Telefone"));
        return this;
    }
}