using System.Text.RegularExpressions;
using SistemaReuniao.Domain.Contexts.Membro.Errors;
using SistemaReuniao.Domain.Contexts.Membro.Exceptions;

namespace SistemaReuniao.Domain.Contexts.Membro.Aggregates.ValueObjects;

public sealed record Email
{
    private Email(string valor)
    {

        if (string.IsNullOrWhiteSpace(valor))
            throw new ValidacaoException(DomainErrors.MEMBRO_EMAIL_EMBRANCO);

        if (!Regex.IsMatch(valor, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            throw new ValidacaoException(DomainErrors.MEMBRO_EMAIL_INVALIDO);

        Valor = valor;
    }

    public string Valor { get; private set; }

    public static Email Criar(string email) => new Email(email);
}
