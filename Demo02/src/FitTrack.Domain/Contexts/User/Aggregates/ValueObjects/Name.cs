namespace FitTrack.Domain.Contexts.User.Aggregates.ValueObjects;

public sealed record Name
{
    private Name(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public string FirstName { get; }

    public string LastName { get; }

    public static Name Create(string firstName, string lastName) => new(firstName, lastName);
}
