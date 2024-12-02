using SistemaReuniao.Domain.Contexts.Membro.Primitives;
using SistemaReuniao.Domain.Contexts.Reuniao.Aggregates.Enums;
using SistemaReuniao.Domain.Contexts.Reuniao.Aggregates.ValueObjects;

namespace SistemaReuniao.Domain.Contexts.Reuniao.Aggregates.Entities;

public sealed class Convite : Entidade
{
    internal Convite(Guid id, Membro.Aggregates.Entities.Membro membro, Reuniao reuniao) : base(id)
    {
        MembroId = membro.Id;
        ReuniaoId = reuniao.Id;
        Status = EStatusConvite.Pendente;
        CriadoEmUtc = DateTime.UtcNow;
    }

    public Guid MembroId { get; private set; }
    public Guid ReuniaoId { get; private set; }
    public EStatusConvite Status { get; private set; }
    public DateTime CriadoEmUtc { get; private set; }
    public DateTime? ModificadoEmUtc { get; private set; }

    internal void Expirado()
    {
        Status = EStatusConvite.Expirado;
        ModificadoEmUtc = DateTime.UtcNow;
    }

    internal Participante Aceito()
    {
        Status = EStatusConvite.Aceito;
        ModificadoEmUtc = DateTime.UtcNow;

        var participante = new Participante(this);

        return participante;
    }
}
