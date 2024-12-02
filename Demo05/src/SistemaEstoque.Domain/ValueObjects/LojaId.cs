using SistemaEstoque.Domain.Exceptions;

namespace SistemaEstoque.Domain.ValueObjects;

public sealed record LojaId
{
    public Guid Valor { get; }

    private LojaId(Guid valor) => Valor = valor;

    public static LojaId Of(Guid valor)
    {
        if (valor == Guid.Empty)
            throw new DomainException("LojaId não pode ser vazio.");

        return new(valor);
    }
}