using SistemaEstoque.Domain.Exceptions;

namespace SistemaEstoque.Domain.ValueObjects;

public sealed record TransportadoraId
{
    public Guid Valor { get; }

    private TransportadoraId(Guid valor) => Valor = valor;

    public static TransportadoraId Of(Guid valor)
    {
        if (valor == Guid.Empty)
            throw new DomainException("TransportadoraId não pode ser vazio.");

        return new(valor);
    }
}