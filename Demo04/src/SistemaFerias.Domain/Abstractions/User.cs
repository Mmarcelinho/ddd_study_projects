namespace SistemaFerias.Domain.Abstractions;

public abstract class User : Entity
{
    protected User() { }

    protected User(Email email, Name name, Password password, string role)
    {
        Email = email;
        Name = name;
        Password = password;
        Role = role;
        Active = true;
    }

    public Name Name { get; private set; } = default!;

    public Email Email { get; private set; } = default!;

    public Password Password { get; private set; } = default!;

    public string Role { get; private set; } = string.Empty;

    public bool Active { get; private set; }
}
