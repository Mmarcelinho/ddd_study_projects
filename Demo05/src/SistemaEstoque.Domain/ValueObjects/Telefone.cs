using SistemaEstoque.Domain.Errors;
using SistemaEstoque.Domain.Exceptions;

namespace SistemaEstoque.Domain.ValueObjects;

public sealed record Telefone
{
    public string Numero { get; }

    public Telefone(string numero)
    {
        if (string.IsNullOrWhiteSpace(numero))
            throw new DomainException(DomainErrors.TELEFONE_VAZIO);

        if (!ValidarFormato(numero))
            throw new DomainException(DomainErrors.TELEFONE_FORMATO_INVALIDO);

        Numero = numero;
    }

    private static bool ValidarFormato(string numero)
    {
        var regex = new System.Text.RegularExpressions.Regex(@"^\(\d{2}\) \d{4,5}-\d{4}$");
        return regex.IsMatch(numero);
    }

    public string ObterDDD() => Numero.Substring(1, 2);

    public string Normalizar() => Numero.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "");
}
