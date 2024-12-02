using SistemaEstoque.Domain.Abstractions;
using SistemaEstoque.Domain.Errors;
using SistemaEstoque.Domain.Events;
using SistemaEstoque.Domain.Exceptions;
using SistemaEstoque.Domain.ValueObjects;

namespace SistemaEstoque.Domain.Entities;

public sealed class Produto : Aggregate<ProdutoId>
{
    public Produto(
        Nome nome,
        string descricao,
        double peso,
        bool controlado,
        int quantidade,
        Guid categoriaId,
        Guid fornecedorId)
    {
        Nome = nome;
        Descricao = descricao;
        Peso = peso;
        Controlado = controlado;
        Quantidade = quantidade;
        CategoriaId = categoriaId;
        FornecedorId = fornecedorId;
    }

    public Nome Nome { get; private set; }

    public string Descricao { get; private set; }

    public double Peso { get; private set; }

    public bool Controlado { get; private set; }

    public int Quantidade { get; private set; }

    public Guid CategoriaId { get; private set; }

    public Categoria Categoria { get; private set; } = default!;

    public Guid FornecedorId { get; private set; }

    public Fornecedor Fornecedor { get; private set; } = default!;

    public static Produto Criar(
        Nome nome,
        string descricao,
        double peso,
        bool controlado,
        int quantidade,
        Guid categoriaId,
        Guid fornecedorId)
    {
        if (quantidade < 0) throw new DomainException(DomainErrors.QUANTIDADE_NEGATIVO);
        if (peso < 0) throw new DomainException(DomainErrors.PESO_PRODUTO_NEGATIVO);

        return new(nome, descricao, peso, controlado, quantidade, categoriaId, fornecedorId);
    }

    public void AtualizarEstoque(int quantidade)
    {
        if (quantidade < 0 && Math.Abs(quantidade) > Quantidade)
            throw new DomainException(DomainErrors.QUANTIDADE_ESTOQUE_INSUFICIENTE);

        Quantidade += quantidade;
        AddDomainEvent(new EstoqueAtualizadoEvent(Id.Valor, Quantidade));
    }
}
