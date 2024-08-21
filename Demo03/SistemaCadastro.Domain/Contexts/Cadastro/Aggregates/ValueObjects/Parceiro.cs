namespace SistemaCadastro.Domain.Contexts.Cadastro.Aggregates.ValueObjects;

public record Parceiro
{
    private Parceiro(bool cliente, bool fornecedor, bool prestador, bool colaborador)
    {
        Cliente = cliente;
        Fornecedor = fornecedor;
        Prestador = prestador;
        Colaborador = colaborador;
    }

    public bool Cliente { get; init; }

    public bool Fornecedor { get; init; }

    public bool Prestador { get; init; }

    public bool Colaborador { get; init; }

    public static Parceiro Criar(bool cliente, bool fornecedor, bool prestador, bool colaborador)
    => new(cliente, fornecedor, prestador, colaborador);
}
