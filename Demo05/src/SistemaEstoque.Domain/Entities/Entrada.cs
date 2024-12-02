using SistemaEstoque.Domain.Abstractions;
using SistemaEstoque.Domain.Errors;
using SistemaEstoque.Domain.Exceptions;
using SistemaEstoque.Domain.ValueObjects;

namespace SistemaEstoque.Domain.Entities;

public sealed class Entrada : Aggregate<EntradaId>
{
    private readonly List<ItemEntrada> _itensEntrada;

    private Entrada(DateTime dataPedido, DateTime dataEntrega, double total, Guid transportadoraId)
    {
        DataPedido = dataPedido;
        DataEntrega = dataEntrega;
        Total = total;
        TransportadoraId = transportadoraId;
        _itensEntrada = [];
    }

    public DateTime DataPedido { get; private set; }

    public DateTime DataEntrega { get; private set; }

    public double Total { get; private set; }

    public Guid TransportadoraId { get; private set; }

    public Transportadora Transportadora { get; private set; } = default!;

    public ICollection<ItemEntrada> Itens => _itensEntrada.AsReadOnly();

    public static Entrada Create(DateTime dataPedido, DateTime dataEntrega, double total, Guid transportadoraId)
    {
        if (dataEntrega < dataPedido)
            throw new DomainException(DomainErrors.DATA_ENTREGA_MENOR_PEDIDO);
        if (total < 0)
            throw new DomainException(DomainErrors.TOTAL_NEGATIVO);

        return new Entrada(dataPedido, dataEntrega, total, transportadoraId);
    }

    public void AdicionarItem(ItemEntrada item)
    {
        _itensEntrada.Add(item);
        Total += item.Quantidade * item.ValorUnitario;
    }
}
