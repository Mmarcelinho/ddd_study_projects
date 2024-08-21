namespace SistemaCadastro.Domain.Contexts.Cadastro.Aggregates.ValueObjects;

public record Email
{
    public Email(string valor)
    {
        Valor = valor;
    }

    public string Valor { get; init; }

    public static Email Criar(string valor) => new(valor);
}