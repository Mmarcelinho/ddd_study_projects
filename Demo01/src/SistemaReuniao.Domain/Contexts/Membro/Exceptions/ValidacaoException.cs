namespace SistemaReuniao.Domain.Contexts.Membro.Exceptions;

public class ValidacaoException : DomainException
{
    public ValidacaoException(string mensagem) : base(mensagem) { }
}
