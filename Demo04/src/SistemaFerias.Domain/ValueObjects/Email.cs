namespace SistemaFerias.Domain.ValueObjects;

public sealed record Email
{
    public string Value { get; }

    private static readonly Regex EmailRegex = new(
        @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

    private Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException(DomainErrors.EMAIL_EMPTY);

        if (!EmailRegex.IsMatch(value))
            throw new DomainException(DomainErrors.EMAIL_INVALID);

        Value = value;
    }

    public static Email Create(string email) => new(email);

    public override string ToString() => Value;
}
