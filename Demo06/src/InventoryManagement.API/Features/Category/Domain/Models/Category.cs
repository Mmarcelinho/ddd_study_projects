using InventoryManagement.API.Shared.Domain;

namespace InventoryManagement.API.Features.Category.Domain.Models;

public sealed class Category : Entity<int>
{
    private Category() { }

    private Category(string name)
    {
        Name = name;
    }
    public string Name { get; private set; }

    public Category Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Nome da categoria é obrigatório.", nameof(name));

        return new(name);
    }

    public void UpdateName(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new ArgumentException("Nome da categoria é obrigatório.", nameof(newName));

        Name = newName;
    }
}
