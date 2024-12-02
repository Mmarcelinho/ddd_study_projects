using SistemaEstoque.Domain.Abstractions;
using SistemaEstoque.Domain.Errors;
using SistemaEstoque.Domain.Exceptions;
using SistemaEstoque.Domain.ValueObjects;

namespace SistemaEstoque.Domain.Entities;

public sealed class Saida : Aggregate<SaidaId>
{
    private readonly List<ItemSaida> _itensSaida = [];

    private Saida(DateTime dataSaida, double total, double frete, double imposto, Guid lojaId, Guid transportadoraId)
    {
        DataSaida = dataSaida;
        Total = total;
        Frete = frete;
        Imposto = imposto;
        LojaId = lojaId;
        TransportadoraId = transportadoraId;
    }

    public DateTime DataSaida { get; private set; }

    public double Total { get; private set; }

    public double Frete { get; private set; }

    public double Imposto { get; private set; }

    public Guid LojaId { get; private set; }

    public Loja Loja { get; private set; } = default!;

    public Guid TransportadoraId { get; private set; }

    public Transportadora Transportadora { get; private set; } = default!;

    public ICollection<ItemSaida> ItensSaida => _itensSaida.AsReadOnly();

    public static Saida Create(DateTime dataSaida, double total, double frete, double imposto, Guid lojaId, Guid transportadoraId)
    {
        if (total < 0) throw new DomainException(DomainErrors.TOTAL_NEGATIVO);
        if (frete < 0) throw new DomainException(DomainErrors.FRETE_NEGATIVO);
        if (imposto < 0) throw new DomainException(DomainErrors.IMPOSTO_NEGATIVO);

        return new Saida(dataSaida, total, frete, imposto, lojaId, transportadoraId);
    }

    public void AdicionarItem(ItemSaida item)
    {
        ItensSaida.Add(item);
        Total += item.Quantidade * item.ValorUnitario;
    }
}
