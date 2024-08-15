using FluentAssertions;
using SistemaReuniao.Domain.Contexts.Reuniao.Aggregates.Enums;
using SistemaReuniao.Domain.Contexts.Reuniao.Errors;
using SistemaReuniao.Domain.Contexts.Membro.Exceptions;
using SistemaReuniao.Domain.Contexts.Membro.Aggregates.ValueObjects;

namespace Domain.Test.Reuniao;

public class ReuniaoTests
{
    [Fact]
    public void CriarReuniao_DeveRetornarReuniao_QuandoParametrosSaoValidos()
    {
        // Arrange
        var nome = Nome.Criar("PrimeiroNome", "Sobrenome");
        var email = Email.Criar("teste@dominio.com");
        var criador = SistemaReuniao.Domain.Contexts.Membro.Aggregates.Entities.Membro.Criar(nome, email);
        var nomeReuniao = "Reunião Importante";

        // Act
        var reuniao = SistemaReuniao.Domain.Contexts.Reuniao.Aggregates.Entities.Reuniao.Criar(
            Guid.NewGuid(),
            criador,
            ETipoReuniao.ComNumeroFixoDeParticipantes,
            DateTime.UtcNow.AddDays(1),
            nomeReuniao,
            "Sala 1",
            10,
            48);

        // Assert
        reuniao.Should().NotBeNull();
        reuniao.Nome.Should().Be(nomeReuniao);
        reuniao.NumeroMaximoDeParticipantes.Should().Be(10);
    }

    [Fact]
    public void CriarReuniao_DeveLancarExcecao_QuandoNumeroMaximoDeParticipantesNaoForDefinido()
    {
        // Arrange
        var nome = Nome.Criar("PrimeiroNome", "Sobrenome");
        var email = Email.Criar("teste@dominio.com");
        var criador = SistemaReuniao.Domain.Contexts.Membro.Aggregates.Entities.Membro.Criar(nome, email);

        // Act
        Action action = () => SistemaReuniao.Domain.Contexts.Reuniao.Aggregates.Entities.Reuniao.Criar(
            Guid.NewGuid(),
            criador,
            ETipoReuniao.ComNumeroFixoDeParticipantes,
            DateTime.UtcNow.AddDays(1),
            "Reunião",
            "Sala 1",
            null,
            48);

        // Assert
        action.Should().Throw<ValidacaoException>()
            .WithMessage(DomainErrors.REUNIAO_NUMERO_MAXIMO_PARTICIPANTES);
    }

    [Fact]
    public void Criar_Reuniao_Deve_Definir_Data_De_Expiracao_Quando_Validade_Dos_Convites_For_Informada()
    {
        // Arrange
        var nome = Nome.Criar("PrimeiroNome", "Sobrenome");
        var email = Email.Criar("teste@dominio.com");
        var criador = SistemaReuniao.Domain.Contexts.Membro.Aggregates.Entities.Membro.Criar(nome, email);

        var agendadoEmUtc = DateTime.UtcNow.AddDays(2);

        // Act
        var reuniao = SistemaReuniao.Domain.Contexts.Reuniao.Aggregates.Entities.Reuniao.Criar(
            Guid.NewGuid(),
            criador,
            ETipoReuniao.ComExpiracaoDeConvites,
            agendadoEmUtc,
            "Reunião",
            "Sala 2",
            null,
            24);

        // Assert
        reuniao.ConvitesExpiramEmUtc.Should().BeCloseTo(agendadoEmUtc.AddHours(-24), precision: TimeSpan.FromSeconds(1));
    }
}
