using SistemaEstoque.Domain.Exceptions;

namespace SistemaEstoque.Domain.ValueObjects;

public sealed record ItemSaidaId
{
    public Guid Valor { get; }

    private ItemSaidaId(Guid valor) => Valor = valor;

    public static ItemSaidaId Of(Guid valor)
    {
        if (valor == Guid.Empty)
            throw new DomainException("ItemSaidaId não pode ser vazio.");

        return new(valor);
    }
}