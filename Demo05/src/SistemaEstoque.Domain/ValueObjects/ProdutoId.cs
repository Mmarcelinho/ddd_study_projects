using SistemaEstoque.Domain.Exceptions;

namespace SistemaEstoque.Domain.ValueObjects;

public sealed record ProdutoId
{
    public Guid Valor { get; }

    private ProdutoId(Guid valor) => Valor = valor;

    public static ProdutoId Of(Guid valor)
    {
        if (valor == Guid.Empty)
            throw new DomainException("ProdutoId não pode ser vazio.");

        return new(valor);
    }
}
