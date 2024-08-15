using SistemaReuniao.Domain.Contexts.Reuniao.Aggregates.ValueObjects;

namespace SistemaReuniao.Domain.Contexts.Reuniao.Repositories;

public interface IParticipanteRepositorio
{
    void Adicionar(Participante participante);
}
