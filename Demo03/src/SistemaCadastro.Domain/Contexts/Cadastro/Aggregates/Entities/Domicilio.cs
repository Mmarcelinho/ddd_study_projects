using SistemaCadastro.Domain.Contexts.Cadastro.Abstractions;
using SistemaCadastro.Domain.Contexts.Cadastro.Aggregates.Enums;
using SistemaCadastro.Domain.Contexts.Cadastro.Aggregates.ValueObjects;
using SistemaCadastro.Domain.Contexts.Cadastro.Errors;
using SistemaCadastro.Domain.Notifications;
using SistemaCadastro.Domain.Validations;
using SistemaCadastro.Domain.Validations.Interfaces;

namespace SistemaCadastro.Domain.Contexts.Cadastro.Aggregates.Entities;

public sealed class Domicilio : Entidade, IContract
{
    private Domicilio(Endereco endereco, EDomicilioTipo tipo)
    {
        Endereco = endereco;
        Tipo = tipo;
    }

    public Endereco Endereco { get; private set; }

    public EDomicilioTipo Tipo { get; private set; }

    public static Domicilio Criar(Endereco endereco, EDomicilioTipo tipo) =>
    new(endereco, tipo);

    public override bool Validate()
    {
        var contracts = new ContractValidations<Cadastro>()
        .EnderecoIsValid(Endereco, 3, 20, DomainErrors.ENDERECO_INVALIDO, nameof(Endereco));

        SetNotificationList(contracts.Notifications as List<Notification>);
        return contracts.IsValid();
    }
}
