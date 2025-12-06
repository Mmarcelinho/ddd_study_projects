using InventoryManagement.API.Shared.Domain;

namespace InventoryManagement.API.Features.Store.Domain.Models;

public class Store : Entity<int>
{
    private Store() { }

    private Store(
        int cityId,
        string name,
        string address,
        int number,
        string neighborhood,
        string phone,
        string stateRegistration,
        string cnpj)
    {
        CityId = cityId;
        Name = name;
        Address = address;
        Number = number;
        Neighborhood = neighborhood;
        Phone = phone;
        StateRegistration = stateRegistration;
        Cnpj = cnpj;
    }

    public int CityId { get; private set; }          
    public string Name { get; private set; } = string.Empty;
    public string Address { get; private set; } = string.Empty;
    public int Number { get; private set; }      
    public string Neighborhood { get; private set; } = string.Empty; 
    public string Phone { get; private set; } = string.Empty;        
    public string StateRegistration { get; private set; } = string.Empty; 
    public string Cnpj { get; private set; } = string.Empty;

    public City.Domain.Models.City City { get; private set; } = default!;

    public static Store Create(
        int cityId,
        string name,
        string address,
        int number,
        string neighborhood,
        string phone,
        string stateRegistration,
        string cnpj)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Nome da loja é obrigatório.", nameof(name));
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
            phone,
            stateRegistration,
            cnpj);
    }

    public void UpdateAddress(string address, int number, string neighborhood)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException("Endereço é obrigatório.", nameof(address));
        if (number < 0)
            throw new ArgumentException("Número não pode ser negativo.", nameof(number));

        Address = address;
        Number = number;
        Neighborhood = neighborhood;
    }
 
    public void UpdateContact(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
            throw new ArgumentException("Telefone é obrigatório.", nameof(phone));

        Phone = phone;
    }
}

