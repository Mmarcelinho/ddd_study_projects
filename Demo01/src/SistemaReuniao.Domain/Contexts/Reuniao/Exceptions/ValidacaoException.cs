namespace SistemaReuniao.Domain.Contexts.Reuniao.Exceptions;

public class ValidacaoException : DomainException
{
    public ValidacaoException(string mensagem) : base(mensagem) { }
}
