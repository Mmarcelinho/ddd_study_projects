namespace FitTrack.Domain.Contexts.Activity.Exceptions;

public class ValidationException : DomainException
{
    public ValidationException(string message) : base(message) { }
}
