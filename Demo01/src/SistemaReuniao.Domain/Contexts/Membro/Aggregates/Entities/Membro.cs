using SistemaReuniao.Domain.Contexts.Membro.Aggregates.ValueObjects;
using SistemaReuniao.Domain.Contexts.Membro.Primitives;

namespace SistemaReuniao.Domain.Contexts.Membro.Aggregates.Entities;

public sealed class Membro : Entidade
{
    private Membro(Guid id, Nome nome, Email email) : base(id)
    {
        Nome = nome;
        Email = email;
    }

    public Nome Nome { get; private set; }

    public Email Email { get; private set; }

    public static Membro Criar(Nome nome, Email email) => new(Guid.NewGuid(), nome, email);
}
