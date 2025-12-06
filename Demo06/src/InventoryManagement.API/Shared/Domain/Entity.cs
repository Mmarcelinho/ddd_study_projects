using InventoryManagement.API.Shared.Domain.Interfaces;

namespace InventoryManagement.API.Shared.Domain;

public abstract class Entity<TId> : IEntity<TId>
{
    public TId Id { get; set; }
}
