using SistemaEstoque.Domain.Abstractions;
using SistemaEstoque.Domain.Errors;
using SistemaEstoque.Domain.Exceptions;
using SistemaEstoque.Domain.ValueObjects;

namespace SistemaEstoque.Domain.Entities;

public sealed class Fornecedor : Aggregate<FornecedorId>
{
    private Fornecedor(Nome nome, Endereco endereco, string contato, Telefone telefone, CNPJ cnpj)
    {
        Nome = nome;
        Endereco = endereco;
        Contato = contato;
        Telefone = telefone;
        CNPJ = cnpj;
    }

    public Nome Nome { get; private set; }

    public Endereco Endereco { get; private set; }

    public string Contato { get; private set; }

    public Telefone Telefone { get; private set; }

    public CNPJ CNPJ { get; private set; }

    public static Fornecedor Criar(
        Nome nome,
        Endereco endereco,
        string contato,
        Telefone telefone,
        CNPJ cnpj)
    {
        if (string.IsNullOrWhiteSpace(contato)) throw new DomainException(DomainErrors.FORNECEDOR_CONTATO_VAZIO);

        return new(nome, endereco, contato, telefone, cnpj);
    }
}
