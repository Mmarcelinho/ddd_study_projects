namespace SistemaReuniao.Domain.Contexts.Membro.Repositories;

public interface IMembroRepositorio
{
    Task<Aggregates.Entities.Membro> RecuperarPorIdAsync(Guid id);

    Task<Aggregates.Entities.Membro> RecuperarPorAsync(Func<Aggregates.Entities.Membro, bool> predicate);
}
