namespace FitTrack.Domain.Contexts.User.Exceptions;

public class ValidationException(string message) : DomainException(message) { }
