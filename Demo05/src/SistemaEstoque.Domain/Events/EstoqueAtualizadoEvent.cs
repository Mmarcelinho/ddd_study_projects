using SistemaEstoque.Domain.Abstractions;

namespace SistemaEstoque.Domain.Events;

public sealed record EstoqueAtualizadoEvent(Guid ProdutoId, int NovaQuantidade) : IDomainEvent
{
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}