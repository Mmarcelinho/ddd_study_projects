using FluentAssertions;
using SistemaReuniao.Domain.Contexts.Membro.Aggregates.ValueObjects;
using SistemaReuniao.Domain.Contexts.Reuniao.Aggregates.Enums;

namespace Domain.Test.Reuniao;

public class ParticipanteTests
{
    [Fact]
    public void Criar_Participante_Deve_Retornar_Participante_Quando_Convite_For_Aceito()
    {
        // Arrange
        var nome = Nome.Criar("PrimeiroNome", "Sobrenome");
        var email = Email.Criar("criador@dominio.com");
        var criador = SistemaReuniao.Domain.Contexts.Membro.Aggregates.Entities.Membro.Criar(nome, email);

        var nomeReuniao = "Reunião Importante";
        var reuniao = SistemaReuniao.Domain.Contexts.Reuniao.Aggregates.Entities.Reuniao.Criar(
            Guid.NewGuid(),
            criador,
            ETipoReuniao.ComNumeroFixoDeParticipantes,
            DateTime.UtcNow.AddDays(1),
            nomeReuniao,
            "Sala 1",
            10,
            48);

        var nomeMembro = Nome.Criar("OutroNome", "Sobrenome");
        var emailMembro = Email.Criar("outro@dominio.com");
        var membro = SistemaReuniao.Domain.Contexts.Membro.Aggregates.Entities.Membro.Criar(nomeMembro, emailMembro);

        var convite = reuniao.EnviarConvite(membro);

        // Act
        var (participante, mensagem) = reuniao.AceitarConvite(convite);

        // Assert
        participante.Should().NotBeNull();
        participante!.MembroId.Should().Be(membro.Id);
        participante.ReuniaoId.Should().Be(reuniao.Id);
    }
}
