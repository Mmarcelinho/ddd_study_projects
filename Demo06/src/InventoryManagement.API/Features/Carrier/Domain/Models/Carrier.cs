using InventoryManagement.API.Shared.Domain;

namespace InventoryManagement.API.Features.Carrier.Domain.Models;

public class Carrier : Entity<int>
{
    protected Carrier() { }

    private Carrier(
        int cityId,
        string name,
        string address,
        int number,
        string neighborhood,
        string zipCode,
        string cnpj,
        string stateRegistration,
        string contact,
        string phone)
    {
        CityId = cityId;
        Name = name;
        Address = address;
        Number = number;
        Neighborhood = neighborhood;
        ZipCode = zipCode;
        Cnpj = cnpj;
        StateRegistration = stateRegistration;
        Contact = contact;
        Phone = phone;
    }

    public int CityId { get; private set; }        
    public string Name { get; private set; } = string.Empty;
    public string Address { get; private set; } = string.Empty;
    public int Number { get; private set; }
    public string Neighborhood { get; private set; } = string.Empty;   
    public string ZipCode { get; private set; } = string.Empty;      
    public string Cnpj { get; private set; } = string.Empty;
    public string StateRegistration { get; private set; } = string.Empty; 
    public string Contact { get; private set; } = string.Empty;
    public string Phone { get; private set; } = string.Empty;          

    public City.Domain.Models.City City { get; private set; } = default!;

    public static Carrier Create(
        int cityId,
        string name,
        string address,
        int number,
        string neighborhood,
        string zipCode,
        string cnpj,
        string stateRegistration,
        string contact,
        string phone)
    {
        if (cityId <= 0)
            throw new ArgumentException("CityId deve ser maior que zero.", nameof(cityId));
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Nome é obrigatório.", nameof(name));
        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException("Endereço é obrigatório.", nameof(address));
        if (number < 0)
            throw new ArgumentException("Número não pode ser negativo.", nameof(number));
        if (string.IsNullOrWhiteSpace(cnpj))
            throw new ArgumentException("CNPJ é obrigatório.", nameof(cnpj));
        if (string.IsNullOrWhiteSpace(phone))
            throw new ArgumentException("Telefone é obrigatório.", nameof(phone));

        return new(
            cityId,
            name,
            address,
            number,
            neighborhood,
            zipCode,
            cnpj,
            stateRegistration,
            contact,
            phone);
    }

    public void UpdateAddress(string address, int number, string neighborhood, string zipCode)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException("Endereço é obrigatório.", nameof(address));
        if (number < 0)
            throw new ArgumentException("Número não pode ser negativo.", nameof(number));

        Address = address;
        Number = number;
        Neighborhood = neighborhood;
        ZipCode = zipCode;
    }

    public void UpdateContact(string contact, string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
            throw new ArgumentException("Telefone é obrigatório.", nameof(phone));
        Contact = contact;
        Phone = phone;
    }
}
