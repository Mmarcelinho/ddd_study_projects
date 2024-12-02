using SistemaEstoque.Domain.Exceptions;

namespace SistemaEstoque.Domain.ValueObjects;

public sealed record ItemEntradaId
{
    public Guid Valor { get; }

    private ItemEntradaId(Guid valor) => Valor = valor;

    public static ItemEntradaId Of(Guid valor)
    {
        if (valor == Guid.Empty)
            throw new DomainException("ItemEntradaId não pode ser vazio.");

        return new(valor);
    }
}
