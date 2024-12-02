using SistemaCadastro.Domain.Contexts.Cadastro.Abstractions;
using SistemaCadastro.Domain.Contexts.Cadastro.Aggregates.ValueObjects;
using SistemaCadastro.Domain.Contexts.Cadastro.Errors;
using SistemaCadastro.Domain.Notifications;
using SistemaCadastro.Domain.Validations;
using SistemaCadastro.Domain.Validations.Interfaces;

namespace SistemaCadastro.Domain.Contexts.Cadastro.Aggregates.Entities;

public sealed class PessoaFisica : Pessoa, IContract
{
    private PessoaFisica(
        Nome nome,
        Cpf cpf,
        DateTime nascimento,
        Email email,
        Telefone telefone) : base(nome, email, telefone)
    {
        Cpf = cpf;
        Nascimento = nascimento;
    }

    public Cpf Cpf { get; private set; }

    public DateTime Nascimento { get; private set; }

    public static PessoaFisica Criar(
        Nome nome,
        Cpf cpf,
        DateTime nascimento,
        Email email,
        Telefone telefone) =>
        new(nome, cpf, nascimento, email, telefone);

    public override bool Validate()
    {
        var contracts = new ContractValidations<PessoaFisica>()
        .CpfIsValid(Cpf, DomainErrors.PESSOA_FISICA_CPF, nameof(Cpf))
        .PrimeiroNomeIsOk(Nome, 3, 20, DomainErrors.NOME_INVALIDO, nameof(Nome.PrimeiroNome))
        .SobrenomeIsOk(Nome, 3, 30, DomainErrors.NOME_INVALIDO, nameof(Nome.Sobrenome))
        .EmailIsValid(Email, DomainErrors.EMAIL_INVALIDO, nameof(Email.Valor))
        .TelefoneIsValid(Telefone, 12, 13, DomainErrors.TELEFONE_INVALIDO, nameof(Telefone.Numero));

        SetNotificationList(contracts.Notifications as List<Notification>);
        return contracts.IsValid();
    }
}
