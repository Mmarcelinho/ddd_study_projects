namespace SistemaReuniao.Domain.Contexts.Membro.Exceptions;

public class DomainException : Exception
{
    public DomainException(string mensagem) : base(mensagem) { }
}
