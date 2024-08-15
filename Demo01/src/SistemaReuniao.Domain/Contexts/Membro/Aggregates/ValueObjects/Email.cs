using System.Text.RegularExpressions;
using SistemaReuniao.Domain.Contexts.Membro.Errors;
using SistemaReuniao.Domain.Contexts.Membro.Exceptions;

namespace SistemaReuniao.Domain.Contexts.Membro.Aggregates.ValueObjects;

    public sealed record Email
    {
    // O construtor privado garante que a criação de um Email passe por validações específicas,
    // centralizando a lógica de validação dentro do próprio Value Object.
    private Email(string valor)
    {
        // Verificação se o valor está em branco, uma regra de domínio que impede que um Email inválido seja criado.
        if(string.IsNullOrWhiteSpace(valor))
            throw new ValidacaoException(DomainErrors.MEMBRO_EMAIL_EMBRANCO);

        // Validação de formato utilizando Regex, garantindo que o Email atenda a um padrão válido.
        if (!Regex.IsMatch(valor, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            throw new ValidacaoException(DomainErrors.MEMBRO_EMAIL_INVALIDO);

        // Se todas as validações passarem, o valor é atribuído à propriedade Valor, que é imutável.
        Valor = valor;
    }

    // Propriedade imutável que armazena o valor do email validado.
    public string Valor { get; private set; }

    // Método de fábrica para criar uma instância de Email, garantindo que a instância sempre seja criada
    // passando pelo construtor privado e, consequentemente, pelas validações.
    public static Email Criar(string email) => new Email(email);
    }
