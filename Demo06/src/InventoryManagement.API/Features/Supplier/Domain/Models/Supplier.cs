using InventoryManagement.API.Shared.Domain;

namespace InventoryManagement.API.Features.Supplier.Domain.Models;

public sealed class Supplier : Entity<Guid>
{
    private Supplier() { }

    private Supplier(
        int cityId,
        string name,
        string address,
        int number,
        string neighborhood,
        string zipCode,
        string contact,
        string cnpj,
        string stateRegistration,
        string phone)
    {
        Id = Guid.NewGuid();
        CityId = cityId;
        Name = name;
        Address = address;
        Number = number;
        Neighborhood = neighborhood;
        ZipCode = zipCode;
        Contact = contact;
        CNPJ = cnpj;
        StateRegistration = stateRegistration;
        Phone = phone;
    }

    public int CodFornecedor { get; private set; }
    public int CityId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Address { get; private set; } = string.Empty;
    public int Number { get; private set; }
    public string Neighborhood { get; private set; } = string.Empty;
    public string ZipCode { get; private set; } = string.Empty;
    public string Contact { get; private set; } = string.Empty;
    public string CNPJ { get; private set; } = string.Empty;
    public string StateRegistration { get; private set; } = string.Empty;
    public string Phone { get; private set; } = string.Empty;

    public City.Domain.Models.City City { get; private set; } = default!;

    public static Supplier Create(
        int cityId,
        string name,
        string address,
        int number,
        string neighborhood,
        string zipCode,
        string contact,
        string cnpj,
        string stateRegistration,
        string phone)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Nome não pode ser vazio.", nameof(name));
        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException("Endereço não pode ser vazio.", nameof(address));
        if (number <= 0)
            throw new ArgumentException("Número deve ser maior que zero.", nameof(number));
        if (string.IsNullOrWhiteSpace(neighborhood))
            throw new ArgumentException("Bairro não pode ser vazio.", nameof(neighborhood));
        if (string.IsNullOrWhiteSpace(zipCode))
            throw new ArgumentException("CEP não pode ser vazio.", nameof(zipCode));
        if (string.IsNullOrWhiteSpace(contact))
            throw new ArgumentException("Contato não pode ser vazio.", nameof(contact));
        if (string.IsNullOrWhiteSpace(cnpj))
            throw new ArgumentException("CNPJ não pode ser vazio.", nameof(cnpj));
        if (string.IsNullOrWhiteSpace(stateRegistration))
            throw new ArgumentException("Inscrição Estadual não pode ser vazia.", nameof(stateRegistration));
        if (string.IsNullOrWhiteSpace(phone))
            throw new ArgumentException("Telefone não pode ser vazio.", nameof(phone));

        return new(cityId, name, address, number, neighborhood, zipCode, contact, cnpj, stateRegistration, phone);
    }

    public void UpdateAddress(string newAddress, int newNumber, string newNeighborhood, string newZipCode)
    {
        if (string.IsNullOrWhiteSpace(newAddress))
            throw new ArgumentException("Endereço não pode ser vazio.", nameof(newAddress));
        if (newNumber <= 0)
            throw new ArgumentException("Número deve ser maior que zero.", nameof(newNumber));
        if (string.IsNullOrWhiteSpace(newNeighborhood))
            throw new ArgumentException("Bairro não pode ser vazio.", nameof(newNeighborhood));
        if (string.IsNullOrWhiteSpace(newZipCode))
            throw new ArgumentException("CEP não pode ser vazio.", nameof(newZipCode));

        Address = newAddress;
        Number = newNumber;
        Neighborhood = newNeighborhood;
        ZipCode = newZipCode;
    }

    public void UpdateContact(string newContact, string newPhone)
    {
        if (string.IsNullOrWhiteSpace(newContact))
            throw new ArgumentException("Contato não pode ser vazio.", nameof(newContact));
        if (string.IsNullOrWhiteSpace(newPhone))
            throw new ArgumentException("Telefone não pode ser vazio.", nameof(newPhone));

        Contact = newContact;
        Phone = newPhone;
    }
}
