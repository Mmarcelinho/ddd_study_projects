using SistemaEstoque.Domain.Abstractions;
using SistemaEstoque.Domain.Errors;
using SistemaEstoque.Domain.Exceptions;
using SistemaEstoque.Domain.ValueObjects;

namespace SistemaEstoque.Domain.Entities;

public sealed class Categoria : Entity<CategoriaId>
{
    public string Nome { get; private set; }

    private Categoria(string nome)
    {
        Nome = nome;
    }

    public static Categoria Criar(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome)) throw new DomainException(DomainErrors.NOME_CATEGORIA_VAZIO);
        return new(nome);
    }
}
