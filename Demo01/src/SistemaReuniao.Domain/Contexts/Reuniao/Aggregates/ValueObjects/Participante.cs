using SistemaReuniao.Domain.Contexts.Reuniao.Aggregates.Entities;

namespace SistemaReuniao.Domain.Contexts.Reuniao.Aggregates.ValueObjects;

public sealed record Participante
{
    // Construtor interno que assegura que um Participante só pode ser criado através de um Convite aceito.
    internal Participante(Convite convite)
    {
        ReuniaoId = convite.ReuniaoId; // Associando o participante à reunião correspondente.
        MembroId = convite.MembroId; // Associando o participante ao membro que aceitou o convite.
        CriadoEmUtc = DateTime.UtcNow; // Registrando a data/hora de criação do participante.
    }

    // Propriedades que descrevem as associações e o estado do participante.
    public Guid ReuniaoId { get; init; } // ID da reunião à qual o participante está associado.
    public Guid MembroId { get; init; } // ID do membro associado a este participante.
    public DateTime CriadoEmUtc { get; init; } // Data/hora de criação do participante.
}
