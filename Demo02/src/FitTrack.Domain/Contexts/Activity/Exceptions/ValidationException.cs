namespace FitTrack.Domain.Contexts.Activity.Exceptions;

public class ValidationException(string message) : DomainException(message) { }