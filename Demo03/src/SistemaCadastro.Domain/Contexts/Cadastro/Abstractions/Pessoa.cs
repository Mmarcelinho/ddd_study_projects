using SistemaCadastro.Domain.Contexts.Cadastro.Aggregates.Entities;
using SistemaCadastro.Domain.Contexts.Cadastro.Aggregates.ValueObjects;

namespace SistemaCadastro.Domain.Contexts.Cadastro.Abstractions;

public abstract class Pessoa : Entidade
{
    protected Pessoa(Nome nome, Email email, Telefone telefone)
    {
        Nome = nome;
        Email = email;
        Telefone = telefone;
        Token = Guid.NewGuid();
        _domicilios = [];
    }

    private readonly List<Domicilio> _domicilios;

    public Nome Nome { get; private set; }

    public Email Email { get; private set; }

    public Telefone Telefone { get; private set; }

    public Guid Token { get; private set; }

    public IReadOnlyCollection<Domicilio> Domicilios => _domicilios.AsReadOnly();
}
