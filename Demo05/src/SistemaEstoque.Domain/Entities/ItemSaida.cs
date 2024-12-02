using SistemaEstoque.Domain.Abstractions;
using SistemaEstoque.Domain.Errors;
using SistemaEstoque.Domain.Exceptions;
using SistemaEstoque.Domain.ValueObjects;

namespace SistemaEstoque.Domain.Entities;

public sealed class ItemSaida : Entity<ItemSaidaId>
{
    public ItemSaida(Guid produtoId, int quantidade, double valorUnitario, string lote)
    {
        ProdutoId = produtoId;
        Quantidade = quantidade;
        ValorUnitario = valorUnitario;
        Lote = lote;
    }

    public Guid ProdutoId { get; private set; }

    public Produto Produto { get; private set; } = default!;

    public int Quantidade { get; private set; }

    public double ValorUnitario { get; private set; }

    public string Lote { get; private set; } = string.Empty;

    public static ItemSaida Create(Guid produtoId, int quantidade, double valorUnitario, string lote)
    {
        if (quantidade <= 0) throw new DomainException(DomainErrors.QUANTIDADE_NEGATIVO);
        if (valorUnitario <= 0) throw new DomainException(DomainErrors.VALOR_UNITARIO_NEGATIVO);
        if (string.IsNullOrWhiteSpace(lote)) throw new DomainException(DomainErrors.LOTE_VAZIO);

        return new(produtoId, quantidade, valorUnitario, lote);
    }
}
