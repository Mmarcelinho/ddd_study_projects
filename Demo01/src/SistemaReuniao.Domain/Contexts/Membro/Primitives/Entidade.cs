namespace SistemaReuniao.Domain.Contexts.Membro.Primitives;

public abstract class Entidade(Guid id) : IEquatable<Entidade>
{
    public Guid Id { get; private set; } = id;

    public static bool operator ==(Entidade? first, Entidade? second)
        => first is not null && second is not null && first.Equals(second);

    public static bool operator !=(Entidade? first, Entidade? second)
        => !(first == second);

    public bool Equals(Entidade? other)
    {
        if (other is null)
            return false;

        if (other.GetType() != GetType())
            return false;

        return other.Id == Id;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;

        if (obj is not Entidade entidade)
            return false;

        return entidade.Id == Id;
    }

    public override int GetHashCode()
        => Id.GetHashCode() * 41;
}
