using SistemaEstoque.Domain.Exceptions;

namespace SistemaEstoque.Domain.ValueObjects;

public sealed record SaidaId
{
    public Guid Valor { get; }

    private SaidaId(Guid valor) => Valor = valor;

    public static SaidaId Of(Guid valor)
    {
        if (valor == Guid.Empty)
            throw new DomainException("SaidaId não pode ser vazio.");

        return new(valor);
    }
}