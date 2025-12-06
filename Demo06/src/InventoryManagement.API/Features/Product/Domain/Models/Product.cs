using InventoryManagement.API.Shared.Domain;

namespace InventoryManagement.API.Features.Product.Domain.Models;

public sealed class Product : Entity<Guid>
{
    private Product() { }

    private Product(
        Guid id,
        int categoryId,
        int supplierId,
        string description,
        double weight,
        bool controlled,
        int minQuantity)
    {
        Id = id;
        CategoryId = categoryId;
        SupplierId = supplierId;
        Description = description;
        Weight = weight;
        Controlled = controlled;
        MinQuantity = minQuantity;
    }

    public int CategoryId { get; set; }
    public int SupplierId { get; set; }
    public string Description { get; set; } = string.Empty;
    public double Weight { get; set; }
    public bool Controlled { get; set; }
    public int MinQuantity { get; set; }

    public Category.Domain.Models.Category Category { get; private set; } = default!;
    public Supplier.Domain.Models.Supplier Supplier { get; private set; } = default!;

    public Product Create(
        int categoryId,
        int supplierId,
        string description,
        double weight,
        bool controlled,
        int minQuantity)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Descrição não pode ser vazia.", nameof(description));
        if (weight <= 0)
            throw new ArgumentException("Peso deve ser maior que zero.", nameof(weight));
        if (minQuantity < 0)
            throw new ArgumentException("Quantidade mínima não pode ser negativa.", nameof(minQuantity));

        return new(Guid.NewGuid(), categoryId, supplierId, description, weight, controlled, minQuantity);
    }
        

    public void UpdateDescription(string newDescription)
    {
        if (string.IsNullOrWhiteSpace(newDescription))
            throw new ArgumentException("Descrição não pode ser vazia.", nameof(newDescription));
        Description = newDescription;
    }

    public void UpdateMinQuantity(int minQuantity)
    {
        if (minQuantity < 0)
            throw new ArgumentException("Quantidade mínima não pode ser negativa.", nameof(minQuantity));
        MinQuantity = minQuantity;
    }
}
