using SistemaReuniao.Domain.Contexts.Reuniao.Aggregates.Entities;

namespace SistemaReuniao.Domain.Contexts.Reuniao.Repositories;

    public interface IConviteRepositorio
    {
        Task<Convite?> RecuperarPorIdAsync(Guid id);

        void Adicionar(Convite convite);
    }
