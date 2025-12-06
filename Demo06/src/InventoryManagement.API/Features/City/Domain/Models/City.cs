using InventoryManagement.API.Shared.Domain;

namespace InventoryManagement.API.Features.City.Domain.Models;

public sealed class City : Entity<int>
{
    public City() { }

    public string Name { get; } = string.Empty;
    public string State { get; } = string.Empty;
}
