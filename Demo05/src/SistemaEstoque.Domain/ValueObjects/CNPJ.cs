using SistemaEstoque.Domain.Errors;
using SistemaEstoque.Domain.Exceptions;
using SistemaEstoque.Domain.Validators;

namespace SistemaEstoque.Domain.ValueObjects;

public sealed record CNPJ
{
    private CNPJ(string valor)
    {
        if (!CNPJValidator.IsCnpj(valor))
            throw new DomainException(DomainErrors.CNPJ_INVALIDO);

        Valor = valor;
    }

    public string Valor { get; }

    public static CNPJ Criar(string valor) => new(valor);
}