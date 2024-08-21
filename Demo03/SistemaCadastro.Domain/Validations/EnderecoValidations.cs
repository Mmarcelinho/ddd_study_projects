using SistemaCadastro.Domain.Contexts.Cadastro.Aggregates.ValueObjects;
using SistemaCadastro.Domain.Notifications;

namespace SistemaCadastro.Domain.Validations;

public partial class ContractValidations<T>
{
    public ContractValidations<T> EnderecoIsValid(Endereco endereco, short minLength, short maxLength, string message, string propertyName)
    {
        if ((string.IsNullOrEmpty(endereco.Cep) && endereco.Cep.Length != 8) ||
        (string.IsNullOrEmpty(endereco.Bairro) && endereco.Bairro.Length < minLength && endereco.Bairro.Length > maxLength) ||
        (string.IsNullOrEmpty(endereco.Cidade) && endereco.Cidade.Length < minLength && endereco.Cidade.Length > maxLength) ||
        (string.IsNullOrEmpty(endereco.Uf) && endereco.Uf.Length != 2))
            AddNotification(new Notification(message, propertyName + " Endereco"));
        return this;
    }

}
