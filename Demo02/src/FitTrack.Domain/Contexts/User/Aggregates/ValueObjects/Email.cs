namespace FitTrack.Domain.Contexts.User.Aggregates.ValueObjects;

public sealed record Email
{
    private Email(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Email Create(string email) => new(email);
}
