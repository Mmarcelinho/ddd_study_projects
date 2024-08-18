namespace FitTrack.Domain.Contexts.User.Exceptions;

public class ValidationException : DomainException
{
    public ValidationException(string message) : base(message) { }
}
