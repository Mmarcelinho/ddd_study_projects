namespace SistemaReuniao.Domain.Contexts.Membro.Repositories;

public interface IMembroRepositorio
{
    Task<Membro.Aggregates.Entities.Membro> RecuperarPorIdAsync(Guid id);

    Task<Membro.Aggregates.Entities.Membro> RecuperarPorAsync(Func<Membro.Aggregates.Entities.Membro, bool> predicate);
}
