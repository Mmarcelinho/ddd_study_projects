using SistemaEstoque.Domain.Errors;
using SistemaEstoque.Domain.Exceptions;

namespace SistemaEstoque.Domain.ValueObjects;

public sealed record Nome
{
    public string Valor { get; }

    private Nome(string valor)
    {
        if (string.IsNullOrWhiteSpace(valor))
            throw new DomainException(DomainErrors.NOME_PRODUTO_VAZIO);

        Valor = valor;
    }

    public static Nome Criar(string valor) => new(valor);
}

