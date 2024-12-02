using SistemaCadastro.Domain.Contexts.Cadastro.Abstractions;
using SistemaCadastro.Domain.Contexts.Cadastro.Aggregates.ValueObjects;
using SistemaCadastro.Domain.Contexts.Cadastro.Errors;
using SistemaCadastro.Domain.Notifications;
using SistemaCadastro.Domain.Validations;
using SistemaCadastro.Domain.Validations.Interfaces;

namespace SistemaCadastro.Domain.Contexts.Cadastro.Aggregates.Entities;

public sealed class PessoaJuridica : Pessoa, IContract
{
    public PessoaJuridica(
        Nome nome,
        Cnpj cnpj,
        string razaoSocial,
        Email email,
        Telefone telefone) : base(nome, email, telefone)
    {
        Cnpj = cnpj;
        RazaoSocial = razaoSocial;
    }

    public Cnpj Cnpj { get; private set; }

    public string RazaoSocial { get; private set; }

    public static PessoaJuridica Criar(
        Nome nome,
        Cnpj cnpj,
        string razaoSocial,
        Email email,
        Telefone telefone
    ) =>
    new(nome, cnpj, razaoSocial, email, telefone);

    public override bool Validate()
    {
        var contracts = new ContractValidations<PessoaJuridica>()
        .CnpjIsValid(Cnpj, DomainErrors.PESSOA_JURIDICA_CNPJ, nameof(Cnpj))
        .RazaoSocialIsOk(RazaoSocial, 3, 30, DomainErrors.PESSOA_JURIDICA_RAZAOSOCIAL, nameof(RazaoSocial))
        .PrimeiroNomeIsOk(Nome, 3, 20, DomainErrors.NOME_INVALIDO, nameof(Nome.PrimeiroNome))
        .SobrenomeIsOk(Nome, 3, 30, DomainErrors.NOME_INVALIDO, nameof(Nome.Sobrenome))
        .NomeFantasiaIsOk(Nome, 3, 15, DomainErrors.NOME_INVALIDO, nameof(Nome.NomeFantasia))
        .EmailIsValid(Email, DomainErrors.EMAIL_INVALIDO, nameof(Email.Valor))
        .TelefoneIsValid(Telefone, 12, 13, DomainErrors.TELEFONE_INVALIDO, nameof(Telefone.Numero));

        SetNotificationList(contracts.Notifications as List<Notification>);
        return contracts.IsValid();
    }
}
