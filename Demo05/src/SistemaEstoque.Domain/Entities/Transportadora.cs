using SistemaEstoque.Domain.Abstractions;
using SistemaEstoque.Domain.ValueObjects;

namespace SistemaEstoque.Domain.Entities;

public sealed class Transportadora : Aggregate<TransportadoraId>
{
    private Transportadora(Nome nome, Endereco endereco, Telefone telefone, CNPJ cnpj)
    {
        Nome = nome;
        Endereco = endereco;
        Telefone = telefone;
        CNPJ = cnpj;
    }

    public Nome Nome { get; private set; }
    public Endereco Endereco { get; private set; }
    public Telefone Telefone { get; private set; }
    public CNPJ CNPJ { get; private set; }

    public static Transportadora Create(Nome nome, Endereco endereco, Telefone telefone, CNPJ cnpj)
    => new(nome, endereco, telefone, cnpj);
}
