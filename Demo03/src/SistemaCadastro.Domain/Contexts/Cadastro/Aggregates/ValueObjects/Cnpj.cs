namespace SistemaCadastro.Domain.Contexts.Cadastro.Aggregates.ValueObjects;

public sealed record Cnpj
{
    private Cnpj(string valor)
    {
        Valor = valor;
    }

    public string Valor { get; init; }

    public static Cnpj Criar(string valor) => new(valor);
}