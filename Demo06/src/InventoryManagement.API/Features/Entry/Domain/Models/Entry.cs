using InventoryManagement.API.Shared.Domain;

namespace InventoryManagement.API.Features.Entry.Domain.Models;

public sealed class Entry : Entity<int>
{
    private readonly List<EntryItem> _items = [];

    private Entry() { }

    private Entry(
        int carrierId,
        DateTime expectedDate,
        DateTime entryDate,
        double freight,
        int invoiceNumber,
        double tax)
    {
        CarrierId = carrierId;
        ExpectedDate = expectedDate;
        EntryDate = entryDate;
        Total = 0.0;
        Freight = freight;
        InvoiceNumber = invoiceNumber;
        Tax = tax;
    }

    public int CarrierId { get; private set; }
    public DateTime ExpectedDate { get; private set; }
    public DateTime EntryDate { get; private set; }
    public double Total { get; private set; }
    public double Freight { get; private set; }
    public int InvoiceNumber { get; private set; }
    public double Tax { get; private set; }

    public Carrier.Domain.Models.Carrier Carrier { get; private set; } = default!;

    public IReadOnlyList<EntryItem> Items => _items.AsReadOnly();

    public static Entry Create(
        int carrierId,
        DateTime expectedDate,
        DateTime entryDate,
        double freight,
        int invoiceNumber,
        double tax)
    {
        if (carrierId <= 0)
            throw new ArgumentException("O transportador informado é inválido. O identificador deve ser maior que zero.", nameof(carrierId));

        if (expectedDate == default)
            throw new ArgumentException("A data prevista de chegada é obrigatória.", nameof(expectedDate));

        if (entryDate == default)
            throw new ArgumentException("A data de entrada é obrigatória.", nameof(entryDate));

        if (entryDate < expectedDate)
            throw new ArgumentException("A data de entrada não pode ser anterior à data prevista de chegada.", nameof(entryDate));

        if (freight < 0)
            throw new ArgumentException("O valor do frete não pode ser negativo.", nameof(freight));

        if (invoiceNumber <= 0)
            throw new ArgumentException("O número da nota fiscal deve ser maior que zero.", nameof(invoiceNumber));

        if (tax < 0)
            throw new ArgumentException("O valor dos impostos não pode ser negativo.", nameof(tax));

        return new(
            carrierId,
            expectedDate,
            entryDate,
            freight,
            invoiceNumber,
            tax);
    }

    public void AddItem(int productId, string batch, int quantity, double value)
    {
        if (_items.Any(i => i.ProductId == productId && i.Batch == batch))
            throw new InvalidOperationException("Este produto e lote já foram adicionados à entrada.");

        if (quantity <= 0)
            throw new ArgumentException("A quantidade do item deve ser maior que zero.", nameof(quantity));

        if (value < 0)
            throw new ArgumentException("O valor unitário do item não pode ser negativo.", nameof(value));

        var item = EntryItem.Create(productId, Id, batch, quantity, value);

        _items.Add(item);
        RecalculateTotal();
    }

    public void RemoveItem(int itemEntryId)
    {
        var item = _items.FirstOrDefault(i => i.Id == itemEntryId)
            ?? throw new KeyNotFoundException("O item informado não pertence a esta entrada.");

        _items.Remove(item);
        RecalculateTotal();
    }

    private void RecalculateTotal()
        => Total = _items.Sum(i => i.Value * i.Quantity) + Freight + Tax;
}
