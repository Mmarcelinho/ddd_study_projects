namespace SistemaCadastro.Domain.Contexts.Cadastro.Aggregates.ValueObjects;

public sealed record Cpf
{
    private Cpf(string valor)
    {
        Valor = valor;
    }

    public string Valor { get; init; }

    public static Cpf Criar(string valor) => new(valor);
}
