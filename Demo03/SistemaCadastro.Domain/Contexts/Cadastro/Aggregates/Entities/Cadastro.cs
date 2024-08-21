using SistemaCadastro.Domain.Contexts.Cadastro.Abstractions;
using SistemaCadastro.Domain.Contexts.Cadastro.Aggregates.ValueObjects;
using SistemaCadastro.Domain.Contexts.Cadastro.Errors;
using SistemaCadastro.Domain.Notifications;
using SistemaCadastro.Domain.Validations;
using SistemaCadastro.Domain.Validations.Interfaces;

namespace SistemaCadastro.Domain.Contexts.Cadastro.Aggregates.Entities;

public sealed class Cadastro : Entidade, IContract
{
    private Cadastro(
        Email email,
        Nome nome,
        bool empresa,
        Telefone telefone,
        Inscrito inscrito,
        Credencial credencial,
        Parceiro parceiro,
        Documentacao documentacao,
        Identificacao identificacao,
        Endereco endereco,
        Pessoa pessoa)
    {
        Email = email;
        Nome = nome;
        Empresa = empresa;
        Telefone = telefone;
        Inscrito = inscrito;
        Credencial = credencial;
        Parceiro = parceiro;
        Documentacao = documentacao;
        Identificacao = identificacao;
        Endereco = endereco;
        Pessoa = pessoa;
    }


    public Email Email { get; private set; }

    public Nome Nome { get; private set; }

    public bool Empresa { get; private set; }

    public Telefone Telefone { get; private set; }

    public Inscrito Inscrito { get; private set; }

    public Credencial Credencial { get; private set; }

    public Parceiro Parceiro { get; private set; }

    public Documentacao Documentacao { get; private set; }

    public Identificacao Identificacao { get; private set; }

    public Endereco Endereco { get; private set; }

    public Pessoa Pessoa { get; private set; }

    public static Cadastro Criar(
        Email email,
        Nome nome,
        bool empresa,
        Telefone telefone,
        Inscrito inscrito,
        Credencial credencial,
        Parceiro parceiro,
        Documentacao documentacao,
        Identificacao identificacao,
        Endereco endereco,
        Pessoa pessoa) =>
        new(
        email,
        nome,
        empresa,
        telefone,
        inscrito,
        credencial,
        parceiro,
        documentacao,
        identificacao,
        endereco,
        pessoa);

    public override bool Validate()
    {
        var contracts = new ContractValidations<Cadastro>()
        .NomeFantasiaIsOk(Nome, 3, 15, DomainErrors.NOME_INVALIDO, nameof(Nome.NomeFantasia))
        .SobrenomeSocialIsOk(Nome, 3, 15, DomainErrors.NOME_INVALIDO, nameof(Nome.SobrenomeSocial))
        .EmailIsValid(Email, DomainErrors.EMAIL_INVALIDO, nameof(Email.Valor))
        .TelefoneIsValid(Telefone, 12, 13, DomainErrors.TELEFONE_INVALIDO, nameof(Telefone.Numero))
        .DocumentoIsValid(Documentacao, DomainErrors.DOCUMENTO_INVALIDO, nameof(Documentacao))
        .EnderecoIsValid(Endereco, 3, 20, DomainErrors.ENDERECO_INVALIDO, nameof(Endereco));

        SetNotificationList(contracts.Notifications as List<Notification>);
        return contracts.IsValid();
    }
}
