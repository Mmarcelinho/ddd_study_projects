namespace SistemaReuniao.Domain.Contexts.Reuniao.Repositories;

public interface IReuniaoRepositorio
{
    Task<Aggregates.Entities.Reuniao?> RecuperarPorIdAsync(Guid id);

    Task<Aggregates.Entities.Reuniao?> RecuperarPorIdComCriadorAsync(Guid id);

    void Adicionar(Aggregates.Entities.Reuniao reuniao);
}
