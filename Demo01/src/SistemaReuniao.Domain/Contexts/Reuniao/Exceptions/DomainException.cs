namespace SistemaReuniao.Domain.Contexts.Reuniao.Exceptions;

public class DomainException : Exception
{
    public DomainException(string mensagem) : base(mensagem) { }
}
