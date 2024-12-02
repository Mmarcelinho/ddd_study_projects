using SistemaEstoque.Domain.Abstractions;
using SistemaEstoque.Domain.Errors;
using SistemaEstoque.Domain.Exceptions;
using SistemaEstoque.Domain.ValueObjects;

namespace SistemaEstoque.Domain.Entities;

public sealed class Loja : Aggregate<LojaId>
{
    private Loja(Nome nome, Endereco endereco, Telefone telefone, CNPJ cnpj, string inscricaoEstadual)
    {
        Nome = nome;
        Endereco = endereco;
        Telefone = telefone;
        CNPJ = cnpj;
        InscricaoEstadual = inscricaoEstadual;
    }

    public Nome Nome { get; private set; } = default!;
    public Endereco Endereco { get; private set; } = default!;
    public Telefone Telefone { get; private set; } = default!;
    public CNPJ CNPJ { get; private set; } = default!;
    public string InscricaoEstadual { get; private set; } = string.Empty;

    public static Loja Criar(Nome nome, Endereco endereco, Telefone telefone, CNPJ cnpj, string inscricaoEstadual)
    {
        if (string.IsNullOrWhiteSpace(inscricaoEstadual)) throw new DomainException(DomainErrors.INSCRICAO_ESTADUAL_VAZIA);

        return new(nome, endereco, telefone, cnpj, inscricaoEstadual);
    }
}
