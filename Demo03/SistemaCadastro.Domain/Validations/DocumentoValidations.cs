using SistemaCadastro.Domain.Contexts.Cadastro.Aggregates.ValueObjects;
using SistemaCadastro.Domain.Notifications;

namespace SistemaCadastro.Domain.Validations;

public partial class ContractValidations<T>
{
    public ContractValidations<T> DocumentoIsValid(Documentacao documentacao, string message, string propertyName)
    {
        if (
            string.IsNullOrEmpty(documentacao.OrgaoEmissor) &&
            documentacao.OrgaoEmissor.Length != 3 ||
            string.IsNullOrEmpty(documentacao.EstadoEmissor) && documentacao.OrgaoEmissor.Length != 2 ||
            documentacao.Numero.Length != 9)
            AddNotification(new Notification(message, propertyName));

        return this;
    }
}
