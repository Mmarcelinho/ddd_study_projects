using SistemaCadastro.Domain.Contexts.Cadastro.Aggregates.ValueObjects;
using SistemaCadastro.Domain.Notifications;

namespace SistemaCadastro.Domain.Validations;

public partial class ContractValidations<T>
{
    public ContractValidations<T> PrimeiroNomeIsOk(Nome nome, short minLength, short maxLength, string message, string propertyName)
    {
        if (string.IsNullOrEmpty(nome.PrimeiroNome) || (nome.PrimeiroNome.Length < minLength) || (nome.PrimeiroNome.Length > maxLength))
            AddNotification(new Notification(message, propertyName));

        return this;
    }

    public ContractValidations<T> SobrenomeIsOk(Nome nome, short minLength, short maxLength, string message, string propertyName)
    {
        if (string.IsNullOrEmpty(nome.Sobrenome) || (nome.Sobrenome.Length < minLength) || (nome.Sobrenome.Length > maxLength))
            AddNotification(new Notification(message, propertyName));

        return this;
    }

    public ContractValidations<T> NomeFantasiaIsOk(Nome nome, short minLength, short maxLength, string message, string propertyName)
    {
        if (string.IsNullOrEmpty(nome.NomeFantasia) || (nome.NomeFantasia.Length < minLength) || (nome.NomeFantasia.Length > maxLength))
            AddNotification(new Notification(message, propertyName));

        return this;
    }

    public ContractValidations<T> SobrenomeSocialIsOk(Nome nome, short minLength, short maxLength, string message, string propertyName)
    {
        if (string.IsNullOrEmpty(nome.SobrenomeSocial) || (nome.SobrenomeSocial.Length < minLength) || (nome.SobrenomeSocial.Length > maxLength))
            AddNotification(new Notification(message, propertyName));

        return this;
    }

    public ContractValidations<T> RazaoSocialIsOk(string nome, short minLength, short maxLength, string message, string propertyName)
    {
        if (string.IsNullOrEmpty(nome) || (nome.Length < minLength) || (nome.Length > maxLength))
            AddNotification(new Notification(message, propertyName));

        return this;
    }
}
