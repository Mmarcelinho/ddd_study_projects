using SistemaEstoque.Domain.Exceptions;

namespace SistemaEstoque.Domain.ValueObjects;

public sealed record FornecedorId
{
    public Guid Valor { get; }

    private FornecedorId(Guid valor) => Valor = valor;

    public static FornecedorId Of(Guid valor)
    {
        if (valor == Guid.Empty)
            throw new DomainException("FornecedorId não pode ser vazio.");

        return new(valor);
    }
}
