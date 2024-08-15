namespace SistemaReuniao.Domain.Contexts.Reuniao.Repositories;

public interface IReuniaoRepositorio
{
    Task<Reuniao.Aggregates.Entities.Reuniao?> RecuperarPorIdAsync(Guid id);

    Task<Reuniao.Aggregates.Entities.Reuniao?> RecuperarPorIdComCriadorAsync(Guid id);

    void Adicionar(Reuniao.Aggregates.Entities.Reuniao reuniao);
}
