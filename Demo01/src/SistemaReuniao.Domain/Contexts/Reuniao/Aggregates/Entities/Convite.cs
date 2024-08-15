using SistemaReuniao.Domain.Contexts.Membro.Primitives;
using SistemaReuniao.Domain.Contexts.Reuniao.Aggregates.Enums;
using SistemaReuniao.Domain.Contexts.Reuniao.Aggregates.ValueObjects;

namespace SistemaReuniao.Domain.Contexts.Reuniao.Aggregates.Entities;

public sealed class Convite : Entidade
{
    // Construtor interno para garantir que o convite só possa ser criado pelo agregado raiz (Reuniao).
    internal Convite(Guid id, Membro.Aggregates.Entities.Membro membro, Reuniao reuniao) : base(id)
    {
        MembroId = membro.Id;
        ReuniaoId = reuniao.Id;
        Status = EStatusConvite.Pendente; // Status inicial do convite é "Pendente".
        CriadoEmUtc = DateTime.UtcNow; // Atribui a data/hora de criação do convite.
    }

    // Propriedades que definem o estado do convite e estão relacionadas ao agregado Reuniao.
    public Guid MembroId { get; private set; } // ID do membro convidado.
    public Guid ReuniaoId { get; private set; } // ID da reunião à qual o convite está associado.
    public EStatusConvite Status { get; private set; } // Status atual do convite.
    public DateTime CriadoEmUtc { get; private set; } // Data/hora de criação do convite.
    public DateTime? ModificadoEmUtc { get; private set; } // Data/hora da última modificação do convite.

    // Método interno para marcar o convite como expirado.
    internal void Expirado()
    {
        Status = EStatusConvite.Expirado; // Altera o status do convite para "Expirado".
        ModificadoEmUtc = DateTime.UtcNow; // Atualiza a data/hora de modificação.
    }

    // Método interno para aceitar o convite e criar um novo participante.
    internal Participante Aceito()
    {
        Status = EStatusConvite.Aceito; // Altera o status do convite para "Aceito".
        ModificadoEmUtc = DateTime.UtcNow; // Atualiza a data/hora de modificação.

        // Cria um novo participante associado a este convite.
        var participante = new Participante(this);

        return participante;
    }
}
