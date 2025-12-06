using InventoryManagement.API.Shared.Domain;

namespace InventoryManagement.API.Features.Entry.Domain.Models;

public sealed class EntryItem : Entity<int>
{
    private EntryItem() { }

    private EntryItem(int productId, int entryId, string batch, int quantity, double value)
    {
        ProductId = productId;
        EntryId = entryId;
        Batch = batch;
        Quantity = quantity;
        Value = value;
    }

    public int ProductId { get; private set; }
    public int EntryId { get; private set; }
    public string Batch { get; private set; } = string.Empty;
    public int Quantity { get; private set; }
    public double Value { get; private set; }

    public Product.Domain.Models.Product Product { get; private set; } = default!;
    public Entry Entry { get; private set; } = default!;

    public static EntryItem Create(int productId, int entryId, string batch, int quantity, double value)
    {
        if (productId <= 0)
            throw new ArgumentException("O produto informado é inválido. O identificador deve ser maior que zero.", nameof(productId));

        if (entryId <= 0)
            throw new ArgumentException("A entrada associada é inválida. O identificador deve ser maior que zero.", nameof(entryId));

        if (string.IsNullOrWhiteSpace(batch))
            throw new ArgumentException("O lote do produto é obrigatório.", nameof(batch));

        if (quantity <= 0)
            throw new ArgumentException("A quantidade informada deve ser maior que zero.", nameof(quantity));

        if (value < 0)
            throw new ArgumentException("O valor unitário do item não pode ser negativo.", nameof(value));

        return new(productId, entryId, batch, quantity, value);
    }
}
