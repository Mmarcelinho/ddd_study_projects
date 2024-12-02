using SistemaEstoque.Domain.Exceptions;

namespace SistemaEstoque.Domain.ValueObjects;

public sealed record EntradaId
{
    public Guid Valor { get; }

    private EntradaId(Guid valor) => Valor = valor;

    public static EntradaId Of(Guid valor)
    {
        if (valor == Guid.Empty)
            throw new DomainException("EntradaId não pode ser vazio.");

        return new(valor);
    }
}