using SistemaReuniao.Domain.Contexts.Reuniao.Aggregates.Entities;

namespace SistemaReuniao.Domain.Contexts.Reuniao.Aggregates.ValueObjects;

public sealed record Participante
{
    internal Participante(Convite convite)
    {
        ReuniaoId = convite.ReuniaoId;
        MembroId = convite.MembroId;
        CriadoEmUtc = DateTime.UtcNow;
    }

    public Guid ReuniaoId { get; init; }
    public Guid MembroId { get; init; }
    public DateTime CriadoEmUtc { get; init; }
}
