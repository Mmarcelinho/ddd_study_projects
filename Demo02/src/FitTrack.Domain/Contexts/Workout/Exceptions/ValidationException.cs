namespace FitTrack.Domain.Contexts.Workout.Exceptions;

public class ValidationException : DomainException
{
    public ValidationException(string message) : base(message) { }
}
