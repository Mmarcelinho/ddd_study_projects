using SistemaEstoque.Domain.Exceptions;

namespace SistemaEstoque.Domain.ValueObjects;

public sealed record CategoriaId
{
    public Guid Valor { get; }

    private CategoriaId(Guid valor) => Valor = valor;

    public static CategoriaId Of(Guid valor)
    {
        if (valor == Guid.Empty)
            throw new DomainException("CategoriaId não pode ser vazio.");

        return new(valor);
    }
}