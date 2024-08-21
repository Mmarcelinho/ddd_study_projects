namespace SistemaCadastro.Domain.Contexts.Cadastro.Aggregates.ValueObjects;

public record Inscrito
{
    public Inscrito(bool assinante, bool associado, bool afiliado)
    {
        Assinante = assinante;
        Associado = associado;
        Afiliado = afiliado;
    }

    public bool Assinante { get; init; }

    public bool Associado { get; init; }

    public bool Afiliado { get; init; }

    public static Inscrito Criar(bool assinante, bool associado, bool afiliado) => new(assinante, associado, afiliado);
}
