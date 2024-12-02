namespace FitTrack.Domain.Contexts.Workout.Exceptions;

public class ValidationException(string message) : DomainException(message) { }