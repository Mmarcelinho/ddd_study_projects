using InventoryManagement.API.Shared.Domain;

namespace InventoryManagement.API.Features.Output.Domain.Models;

public sealed class Output : Entity<int>
{
    private readonly List<OutputItem> _items = [];

    private Output() { }

    private Output(int storeId, int carrierId, double freight, double tax)
    {
        StoreId = storeId;
        CarrierId = carrierId;
        Total = 0.0;
        Freight = freight;
        Tax = tax;
    }

    public int StoreId { get; private set; }
    public int CarrierId { get; private set; }
    public double Total { get; private set; }
    public double Freight { get; private set; }
    public double Tax { get; private set; }

    public Store.Domain.Models.Store Store { get; private set; } = default!;
    public Carrier.Domain.Models.Carrier Carrier { get; private set; } = default!;

    public IReadOnlyCollection<OutputItem> Items => _items.AsReadOnly();

    public static Output Create(int storeId, int carrierId, double freight, double tax)
    {
        if (storeId <= 0)
            throw new ArgumentException("A loja informada é inválida. O identificador deve ser maior que zero.", nameof(storeId));

        if (carrierId <= 0)
            throw new ArgumentException("O transportador informado é inválido. O identificador deve ser maior que zero.", nameof(carrierId));

        if (freight < 0)
            throw new ArgumentException("O valor do frete não pode ser negativo.", nameof(freight));

        if (tax < 0)
            throw new ArgumentException("O valor dos impostos não pode ser negativo.", nameof(tax));

        return new(storeId, carrierId, freight, tax);
    }

    public void AddItem(int productId, string batch, int quantity, double value)
    {
        if (_items.Any(i => i.ProductId == productId && i.Batch == batch))
            throw new InvalidOperationException("Este produto e lote já foram adicionados a esta saída.");

        if (quantity <= 0)
            throw new ArgumentException("A quantidade informada deve ser maior que zero.", nameof(quantity));

        if (string.IsNullOrWhiteSpace(batch))
            throw new ArgumentException("O lote do produto é obrigatório.", nameof(batch));

        if (value < 0)
            throw new ArgumentException("O valor unitário do item não pode ser negativo.", nameof(value));

        var item = OutputItem.Create(productId, Id, batch, quantity, value);

        _items.Add(item);
        RecalculateTotal();
    }

    public void RemoveItem(int itemOutputId)
    {
        var item = _items.FirstOrDefault(i => i.Id == itemOutputId)
            ?? throw new KeyNotFoundException("O item informado não pertence a esta saída.");

        _items.Remove(item);
        RecalculateTotal();
    }

    private void RecalculateTotal()
        => Total = _items.Sum(i => i.Value * i.Quantity) + Freight + Tax;
}
