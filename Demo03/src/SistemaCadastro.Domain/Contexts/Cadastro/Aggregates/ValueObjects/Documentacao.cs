namespace SistemaCadastro.Domain.Contexts.Cadastro.Aggregates.ValueObjects;

public sealed record Documentacao
{
    private Documentacao(string numero, string orgaoEmissor, string estadoEmissor, DateTime validade)
    {
        Numero = numero;
        OrgaoEmissor = orgaoEmissor;
        EstadoEmissor = estadoEmissor;
        Validade = validade;
    }

    public string Numero { get; init; }

    public string OrgaoEmissor { get; init; }

    public string EstadoEmissor { get; init; }

    public DateTime Validade { get; init; }

    public static Documentacao Criar(
        string numero,
        string orgaoEmissor,
        string estadoEmissor,
        DateTime validade) =>
        new(numero, orgaoEmissor, estadoEmissor, validade);
}
