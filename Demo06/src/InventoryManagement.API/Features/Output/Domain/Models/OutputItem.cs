using InventoryManagement.API.Shared.Domain;

namespace InventoryManagement.API.Features.Output.Domain.Models;

public sealed class OutputItem : Entity<int>
{
    private OutputItem() { }

    private OutputItem(int productId, int outputId, string batch, int quantity, double value)
    {
        ProductId = productId;
        OutputId = outputId;
        Batch = batch;
        Quantity = quantity;
        Value = value;
    }

    public int ProductId { get; private set; }
    public int OutputId { get; private set; }
    public string Batch { get; private set; } = string.Empty;
    public int Quantity { get; private set; }
    public double Value { get; private set; }

    public Product.Domain.Models.Product Product { get; private set; } = default!;
    public Output Output { get; private set; } = default!;

    public static OutputItem Create(int productId, int outputId, string batch, int quantity, double value)
    {
        if (quantity <= 0)
            throw new ArgumentNullException("Quantidade deve ser positiva.");
        if (string.IsNullOrWhiteSpace(batch))
            throw new ArgumentNullException("Lote é obrigatório.");
        if (value < 0)
            throw new ArgumentException("Valor não pode ser negativo.");

        return new(productId, outputId, batch, quantity, value);
    }
}
